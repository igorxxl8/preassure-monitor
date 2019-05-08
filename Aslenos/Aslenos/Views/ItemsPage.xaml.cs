using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Aslenos.Models;
using Aslenos.Views;
using Aslenos.ViewModels;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using Plugin.BLE.Abstractions.Exceptions;

namespace Aslenos.Views
{
    public partial class ItemsPage : ContentPage
    {
        IAdapter Adapter { get; }
        IBluetoothLE BluetoothLE { get; }
        ICollection<IDevice> DevicesList { get; }
        IDevice Device { get; set; }

        public ItemsPage()
        {
            InitializeComponent();

            BluetoothLE = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;

            DevicesList = new ObservableCollection<IDevice>();
            DevicesListView.ItemsSource = DevicesList;
        }

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

        async void AddItem_Clicked(object sender, EventArgs e)
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