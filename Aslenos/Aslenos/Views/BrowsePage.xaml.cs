using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aslenos.Helpers;
using Aslenos.Interfaces;
using Aslenos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Aslenos.Models.Device;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowsePage : ContentPage
    {
        private Bluetooth Bluetooth { get; }

        public BrowsePage()
        {
            InitializeComponent();

            Bluetooth = DependencyService.Get<Bluetooth>();
            DevicesList.ItemsSource = Bluetooth.GetDevices();

            Bluetooth.SearchDevices();
        }

        private async void DevicesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedDevice = DevicesList.SelectedItem;

            if (selectedDevice == null)
            {
                return;
            }

            var answer = await DisplayAlert("Info", "Do you want to connect to this device", "YES", "NO");

            if (!answer)
            {
                return;
            }

            var result = await Bluetooth.TryConnectToDevice(selectedDevice);

            if (result)
            {
                await DisplayAlert("Connection status:", "The device is successfully connected.", "OK");
            }
            else
            {
                await DisplayAlert("Connection status:", "Сonnection error.", "Try again");
            }
        }
    }
}