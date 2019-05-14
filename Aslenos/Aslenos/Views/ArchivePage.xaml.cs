using System;
using System.Collections.Generic;
using Aslenos.Interfaces;
using Aslenos.Models;
using Aslenos.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArchivePage : ContentPage
    {
        private readonly IFileWorker _fileWorker;
        private DeviceDataViewModel _deviceData;

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
            _fileWorker = DependencyService.Get<IFileWorker>();
        }

        private void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Option)OptionsList.SelectedItem).Command.Execute(null);
        }

        private void OnOpenButtonClicked(object sender, EventArgs e)
        {
            var files = _fileWorker.GetFilesAsync();
            foreach (var file in files.Result)
            {
                DisplayAlert(file, "", "OK");
            }

            _deviceData = new DeviceDataViewModel();
        }
    }
}