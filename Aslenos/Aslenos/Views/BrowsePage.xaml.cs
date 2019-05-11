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
    public partial class BrowsePage : ContentPage
    {
        private readonly JsonDataKeeper<IList<Device>> _dataKeeper;

        public BrowsePage()
        {
            InitializeComponent();
            _dataKeeper = new JsonDataKeeper<IList<Device>>("browse.json");
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            var selectedDevice = DevicesList.SelectedItem;
            if (selectedDevice == null) return;
            await DisplayAlert("Selected", ((Device)selectedDevice).Text, "OK");
            DevicesList.SelectedItem = null;
        }

        private async void OnLoadButtonClicked(object sender, EventArgs e)
        {
            DevicesList.ItemsSource = await _dataKeeper.Browse();
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new FindDevicePage(_dataKeeper)));
        }
    }
}