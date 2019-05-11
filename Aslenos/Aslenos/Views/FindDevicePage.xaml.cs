using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindDevicePage : ContentPage
    {
        public FindDevicePage()
        {
            InitializeComponent();
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            var selectedDevice = DevicesList.SelectedItem;
            if (selectedDevice == null) return;
            //await DisplayAlert("Selected", ((Device)selectedDevice).Text, "OK");
            DevicesList.SelectedItem = null;
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}