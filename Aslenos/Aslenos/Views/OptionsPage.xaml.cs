using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aslenos.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        public OptionsPage()
        {
            InitializeComponent();
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
                },
                new Option
                {
                    Name = "Open vacuum",
                    Command = new Command(() => Navigation.PushModalAsync(new OpenVacuumPage()))
                },
            };

            OptionsList.ItemSelected += OnOptionSelected;
        }

        private void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Option)OptionsList.SelectedItem).Command.Execute(null);
        }
    }
}