using Aslenos.Services;
using Xamarin.Forms;

namespace Aslenos.Views
{
    public partial class MainPage : TabbedPage
    {
        private Bluetooth Bluetooth { get; }

        public MainPage()
        {
            InitializeComponent();

            Bluetooth = DependencyService.Get<Bluetooth>();

            if (Bluetooth.IsBluetoothOn())
            {
                BluetoothStatusToolbarItem.Text = "Bluetooth status: ON";
            }

            if (Bluetooth.IsDeviceConnect())
            {
                ConnectionStatusToolbarItem.Text = "Connection status: OFF";
            }
        }
    }
}