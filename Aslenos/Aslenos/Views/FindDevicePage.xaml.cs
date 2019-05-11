using Aslenos.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Aslenos.Models.Device;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindDevicePage : ContentPage
    {
        private Bluetooth Bluetooth { get; }
        private readonly JsonDataKeeper<IList<Device>, Device> _dataKeeper;

        public FindDevicePage()
        {
            InitializeComponent();

            Bluetooth = DependencyService.Get<Bluetooth>();
            _dataKeeper = DependencyService.Get<JsonDataKeeper<IList<Models.Device>,Device>>();
            _dataKeeper.Filename = "browse.json";
            DevicesList.ItemsSource = Bluetooth.GetDevices();

            Bluetooth.SearchDevices();
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            var selectedDevice = DevicesList.SelectedItem;

            if (selectedDevice == null)
            {
                return;
            }

            var result = await Bluetooth.TryConnectToDevice(selectedDevice);

            if (result)
            {
                await DisplayAlert("Connection status:", "The device is successfully connected.", "OK");
                _dataKeeper.Save(new Device(Bluetooth.GetDevice()));
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Connection status:", "Сonnection error.", "Try again");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}