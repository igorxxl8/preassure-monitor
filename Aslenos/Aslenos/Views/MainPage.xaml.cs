using Aslenos.Services;
using Xamarin.Forms;

namespace Aslenos.Views
{
    public partial class MainPage : TabbedPage
    {
        private Bluetooth Bluetooth { get; }
        private Calculation Calculation { get; }

        public MainPage()
        {
            InitializeComponent();

            Bluetooth = DependencyService.Get<Bluetooth>();
            Calculation = DependencyService.Get<Calculation>();

            Bluetooth.MonitorBluetoothStatus(MonitorBluetoothState);
            Bluetooth.MonitorConnectedStatus(MonitorConnectedStatus);
            Bluetooth.MonitorConnectedLostStatus(MonitorConnectedLostStatus);

            if (Bluetooth.IsBluetoothOn())
            {
                BluetoothStatusToolbarItem.Text = "Bluetooth status: ON";
            }

            if (Bluetooth.IsDeviceConnect())
            {
                ConnectionStatusToolbarItem.Text = "Connection status: ON";
            }
        }

        private async void MonitorBluetoothState(bool status)
        {
            if (status)
            {
                BluetoothStatusToolbarItem.Text = "Bluetooth status: ON";
            }
            else
            {
                BluetoothStatusToolbarItem.Text = "Bluetooth status: OFF";
                Calculation.StopCalculation();

                await DisplayAlert("Error", "Bluetooth was turned off.", "OK");
            }
        }

        private void MonitorConnectedStatus()
        {
            ConnectionStatusToolbarItem.Text = "Connection status: ON";
        }

        private async void MonitorConnectedLostStatus()
        {
            ConnectionStatusToolbarItem.Text = "Connection status: OFF";
            Calculation.StopCalculation();

            await DisplayAlert("Error", "Device was turned off.", "OK");
        }
    }
}