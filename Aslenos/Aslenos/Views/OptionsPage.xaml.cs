using System.Collections.Generic;
using Aslenos.Models;
using Aslenos.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        private Bluetooth Bluetooth { get; }

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
                    Name = "Manometer",
                    Command = new Command(() => Navigation.PushModalAsync(new ManometerPage()))
                },
                new Option
                {
                    Name = "Open trend",
                    Command = new Command(() => Navigation.PushModalAsync(new OpenTrendPage()))
                },
                new Option
                {
                    Name = "Open trend graphic",
                    Command = new Command(() => Navigation.PushModalAsync(new OpenTrendGraphicPage()))
                }
            };

            OptionsList.ItemSelected += OnOptionSelected;
        }

        private void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Option)OptionsList.SelectedItem).Command.Execute(null);
        }

        protected override void OnAppearing()
        {
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