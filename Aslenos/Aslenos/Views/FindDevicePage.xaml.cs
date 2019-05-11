using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aslenos.Interfaces;
using Aslenos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Aslenos.Models.Device;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindDevicePage : ContentPage
    {
        private readonly JsonDataKeeper<IList<Device>> _dataKeeper;

        public FindDevicePage()
        {
            InitializeComponent();

            // bluetooth devices find fill listview here
            DevicesList.ItemsSource = new List<Device>
            {
                new Device {Text = "1"},
                new Device {Text = "2"},
                new Device {Text = "3"},
                new Device {Text = "4"}
            };
        }

        public FindDevicePage(JsonDataKeeper<IList<Device>> dataKeeper):this()
        {
            _dataKeeper = dataKeeper;
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            var selectedDevice = DevicesList.SelectedItem;
            if (selectedDevice == null) return;
            var devices = (List<Device>)(await _dataKeeper.Browse());
            devices.Add((Device)selectedDevice);
            _dataKeeper.Save(devices);
            OnCancelClicked(this, e);
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}