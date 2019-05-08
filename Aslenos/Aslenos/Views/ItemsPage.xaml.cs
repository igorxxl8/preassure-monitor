using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;

namespace Aslenos.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();

            BluetoothLE = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;

            DevicesList = new ObservableCollection<IDevice>();
            DevicesListView.ItemsSource = DevicesList;
        }

        private IAdapter Adapter { get; }
        private IBluetoothLE BluetoothLE { get; }
        private ICollection<IDevice> DevicesList { get; }
        private IDevice Device { get; set; }

        private async void DevicesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Device = DevicesListView.SelectedItem as IDevice;

            var result = await DisplayAlert("AVISO", "Deseja se conectar a esse dispositivo?", "Conectar", "Cancelar");

            if (!result)
                return;

            //Stop Scanner
            await Adapter.StopScanningForDevicesAsync();

            try
            {
                await Adapter.ConnectToDeviceAsync(Device);

                await DisplayAlert("Conectado", "Status:" + Device.State, "OK");
            }
            catch (DeviceConnectionException ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void SearchDevice(object sender, EventArgs e)
        {
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void CheckStatusButton_Clicked(object sender, EventArgs e)
        {
            if (BluetoothLE.State == BluetoothState.Off)
            {
                await DisplayAlert("Atenção", "Bluetooth desabilitado.", "OK");
            }
            else
            {
                DevicesList.Clear();

                Adapter.ScanTimeout = 10000;
                Adapter.ScanMode = ScanMode.Balanced;


                Adapter.DeviceDiscovered += (obj, a) =>
                {
                    if (!DevicesList.Contains(a.Device))
                        DevicesList.Add(a.Device);
                };

                await Adapter.StartScanningForDevicesAsync();
            }
        }
    }
}