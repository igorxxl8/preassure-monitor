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
                    Command = new Command(() => DisplayAlert("1", "", "OK"))
                },
                new Option
                {
                    Name = "Manometer",
                    Command = new Command(() => DisplayAlert("2", "", "OK"))
                },
                new Option
                {
                    Name = "Open trend",
                    Command = new Command(() => DisplayAlert("3", "", "OK"))
                },
                new Option
                {
                    Name = "Open trend graphic",
                    Command = new Command(() => DisplayAlert("4", "", "OK"))
                },
                new Option
                {
                    Name = "Open vacuum",
                    Command = new Command(() => DisplayAlert("5", "", "OK"))
                },
            };

            OptionsList.ItemSelected += OnOptionSelected; ;
        }

        private void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Option)OptionsList.SelectedItem).Command.Execute(null);
        }
    }
}