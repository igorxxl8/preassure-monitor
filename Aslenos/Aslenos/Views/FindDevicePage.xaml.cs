using Aslenos.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindDevicePage : ContentPage
    {
        private Bluetooth Bluetooth { get; }

        public FindDevicePage()
        {
            InitializeComponent();

            Bluetooth = DependencyService.Get<Bluetooth>();
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