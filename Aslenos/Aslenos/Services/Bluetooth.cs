using Aslenos.Helpers;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aslenos.Services
{
    class Bluetooth
    {
        private IBluetoothLE BluetoothLE { get; }
        private IAdapter Adapter { get; }
        private IDevice Device { get; set; }
        private IList<IDevice> DevicesList { get; }

        private ICharacteristic CharacteristicRX { get; set; }
        private ICharacteristic CharacteristicTX { get; set; }

        private bool AdcStatus { get; set; }

        private Calculation Calculation { get; }

        public Bluetooth()
        {
            BluetoothLE = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;

            Calculation = DependencyService.Get<Calculation>();

            DevicesList = new ObservableCollection<IDevice>();
        }

        public IList<IDevice> GetDevices()
        {
            return DevicesList;
        }

        public (Guid Id, string Name) GetDevice()
        {
            return (Device.Id, Device.Name);
        }

        public bool IsBluetoothOn()
        {
            return BluetoothLE.State == BluetoothState.On;
        }

        public bool IsDeviceConnect()
        {
            return Device?.State == Plugin.BLE.Abstractions.DeviceState.Connected;
        }

        public async void CheckScanningStatus()
        {
            if (Adapter.IsScanning)
            {
                await Adapter.StopScanningForDevicesAsync();
            }
        }

        public async void SearchDevices()
        {
            CheckScanningStatus();
            DevicesList.Clear();

            Adapter.ScanTimeout = Constants.SCAN_TIMEOUT;
            Adapter.ScanMode = ScanMode.Balanced;

            Adapter.DeviceDiscovered += (s, a) =>
            {
                var device = a.Device;

                if (!DevicesList.Contains(device) && device.Name != null)
                {
                    DevicesList.Add(device);
                }
            };

            await Adapter.StartScanningForDevicesAsync();
        }

        public async Task<bool> TryConnectToDevice(object device)
        {
            Device = device as IDevice;

            await Adapter.StopScanningForDevicesAsync();

            try
            {
                await Adapter.ConnectToDeviceAsync(Device);
                AddListenerForDevice();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> TryConnectToDeviceWithGuid(Guid guid)
        {
            try
            {
                await Adapter.ConnectToKnownDeviceAsync(guid);
                AddListenerForDevice();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void StartAdc()
        {
            AdcStatus = true;
            Calculation.StartCalculation();
        }

        public void StopAdc()
        {
            AdcStatus = false;
            Calculation.StopCalculation();
        }

        public async void AddListenerForDevice()
        {
            var service = await Device.GetServiceAsync(Guids.UART_SERVICE);
            CharacteristicRX = await service.GetCharacteristicAsync(Guids.RX_DATA_CHARACTERISTIC);
            CharacteristicTX = await service.GetCharacteristicAsync(Guids.TX_DATA_CHARACTERISTIC);

            CharacteristicRX.ValueUpdated += (o, args) =>
            {
                byte[] bytes = args.Characteristic.Value;

                if (AdcStatus)
                {
                    Calculation.AdcDataSplit(bytes);
                }
            };

            await CharacteristicRX.StartUpdatesAsync();
        }

        public async void SendCommand(string command)
        {
            var bytes = Encoding.ASCII.GetBytes(command);

            if (command == Commands.START)
            {
                StartAdc();
            }

            if (command == Commands.STOP)
            {
                StopAdc();
            }

            await CharacteristicTX.WriteAsync(bytes);

        }

        public void MonitorBluetoothStatus(Action<bool> action)
        {
            BluetoothLE.StateChanged += (s, e) => action.Invoke(BluetoothLE.State == BluetoothState.On);
        }

        public void MonitorConnectedStatus(Action action)
        {
            Adapter.DeviceConnected += (s, e) => action.Invoke();
        }

        public void MonitorConnectedLostStatus(Action action)
        {
            Adapter.DeviceConnectionLost += (s, e) => action.Invoke();
            Adapter.DeviceDisconnected += (s, e) => action.Invoke();
        }
    }
}
