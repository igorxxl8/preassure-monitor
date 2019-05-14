using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aslenos.Interfaces;
using Aslenos.Models;
using Aslenos.Services;
using Aslenos.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        private Bluetooth Bluetooth { get; }
        private DeviceDataViewModel _lastData;

        public OptionsPage()
        {
            InitializeComponent();

            Bluetooth = DependencyService.Get<Bluetooth>();

            OptionsList.ItemsSource = new List<Option>
            {
                new Option
                {
                    Name = "Pulsation analyze",
                    Command = new Command(() => Navigation.PushModalAsync(new PulsationAnalyzePage()))
                },
                new Option
                {
                    Name = "Open trend",
                    Command = new Command(() => Navigation.PushModalAsync(new OpenTrendPage()))
                },
                new Option
                {
                    Name = "Open trend graphic",
                    Command = new Command(() => Navigation.PushModalAsync(new NavigationPage(new OpenTrendGraphicPage(_lastData))))
                }
            };

            OptionsList.ItemSelected += OnOptionSelected;
        }

        private async void LoadLastDataModel()
        {
            var _fileWorker = DependencyService.Get<IFileWorker>();
            var _jsonKeeper = new JsonDataKeeper<DeviceDataViewModel>();
            var files = _fileWorker.GetFilesAsync().Result.ToArray();

            if (files.Length == 0)
            {
                _lastData = new DeviceDataViewModel();
            }
            else
            {
                _jsonKeeper.Filename = files[files.Length - 1];
                _lastData = await _jsonKeeper.Browse();
            }
        }

        private void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Option)OptionsList.SelectedItem).Command.Execute(null);
        }

        protected override void OnAppearing()
        {
            LoadLastDataModel();

            if (Bluetooth.IsDeviceConnect())
            {
                OptionsList.IsEnabled = true;
            }
            else
            {
                //OptionsList.IsEnabled = false;
                //RequestToConnectToDevice();
            }

            base.OnAppearing();
        }

        private async void RequestToConnectToDevice()
        {
            await DisplayAlert("Error", "You cannot use the options when not connected to the device.\nConnect to the device and try it again.", "OK");
        }
    }
}