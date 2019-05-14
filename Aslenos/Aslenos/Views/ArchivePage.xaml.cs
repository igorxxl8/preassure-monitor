using System;
using System.Collections.Generic;
using Aslenos.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArchivePage : ContentPage
    {
        public ArchivePage()
        {
            InitializeComponent();
            OptionsList.ItemsSource = new List<Option>
            {
                new Option
                {
                    Name = "Open trend",
                    Command = new Command(() => Navigation.PushModalAsync(new OpenTrendPage()))
                },
                new Option
                {
                    Name = "Open trend graphic",
                    Command = new Command(() => Navigation.PushModalAsync(new NavigationPage(new OpenTrendGraphicPage())))
                }
            };

            OptionsList.ItemSelected += OnOptionSelected;
        }

        private void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Option)OptionsList.SelectedItem).Command.Execute(null);
        }

        private void OnOpenButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}