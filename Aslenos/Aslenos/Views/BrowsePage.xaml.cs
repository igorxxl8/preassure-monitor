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
        public BrowsePage()
        {
            InitializeComponent();

            // bluetooth items insert here
            DevicesList.ItemsSource = new List<Device>
            {
                new Device {Text="1"},
                new Device {Text="2" },
                new Device {Text="3"},
                new Device {Text="4" },
                new Device {Text="5"},
                new Device {Text="6" },
                new Device {Text="7"},
                new Device {Text="8" },
                new Device {Text="9"},
                new Device {Text="10" },
                new Device {Text="11"},
                new Device {Text="12" },
            };
        }

        private async void OnConnectClicked(object sender, EventArgs e)
        {
            var selectedDevice = DevicesList.SelectedItem;
            if (selectedDevice == null) return;
            DisplayAlert("Selected", ((Device)selectedDevice).Text, "OK");
            DevicesList.SelectedItem = null;

            var filename = "test.txt";
            if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
            {
                // запрашиваем разрешение на перезапись
                bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                if (isRewrited == false) return;
            }
            // перезаписываем файл
            await DependencyService.Get<IFileWorker>().SaveTextAsync(filename, "test text");
        }
    }
}