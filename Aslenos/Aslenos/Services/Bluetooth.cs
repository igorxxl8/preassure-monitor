using Aslenos.Helpers;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

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

        public Bluetooth()
        {
            BluetoothLE = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;

            DevicesList = new ObservableCollection<IDevice>();
        }

        public IList<IDevice> GetDevices()
        {
            return DevicesList ;
        }

        public bool IsBluetoothOn()
        {
            return BluetoothLE.State == BluetoothState.On;
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

                if (!DevicesList.Contains(device))
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

        public async void AddListenerForDevice()
        {
            var service = await Device.GetServiceAsync(Guids.UART_SERVICE);
            CharacteristicRX = await service.GetCharacteristicAsync(Guids.RX_DATA_CHARACTERISTIC);
            CharacteristicTX = await service.GetCharacteristicAsync(Guids.TX_DATA_CHARACTERISTIC);

            CharacteristicRX.ValueUpdated += (o, args) =>
            {
                byte[] bytes = args.Characteristic.Value;
            };

            await CharacteristicRX.StartUpdatesAsync();
        }

        public async void SendCommand(string command)
        {
            var bytes = Encoding.ASCII.GetBytes(command);
            await CharacteristicTX.WriteAsync(bytes);
        }
    }
}
