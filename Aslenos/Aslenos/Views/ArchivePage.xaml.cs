using System;
using System.Collections.Generic;
using System.Linq;
using Aslenos.Interfaces;
using Aslenos.Models;
using Aslenos.Services;
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
        private readonly JsonDataKeeper<DeviceDataViewModel> _jsonKeeper;

        public ArchivePage()
        {
            InitializeComponent();
            OptionsList.ItemsSource = new List<Option>
            {
                new Option
                {
                    Name = "Open trend",
                    Command = new Command(() => Navigation.PushModalAsync(new OpenTrendPage(_deviceData)))
                },
                new Option
                {
                    Name = "Open trend graphic",
                    Command = new Command(() => Navigation.PushModalAsync(new NavigationPage(new OpenTrendGraphicPage(_deviceData))))
                }
            };

            OptionsList.ItemSelected += OnOptionSelected;
            _fileWorker = DependencyService.Get<IFileWorker>();
            _jsonKeeper = new JsonDataKeeper<DeviceDataViewModel>();
        }

        private void OnOptionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((Option)OptionsList.SelectedItem).Command.Execute(null);
        }

        private async void OnOpenButtonClicked(object sender, EventArgs e)
        {
            var files = _fileWorker.GetFilesAsync().Result.ToArray();
            var archive = await DisplayActionSheet("Choose archive", "Cancel", null, files);

            if (archive == "Cancel") return;
            _jsonKeeper.Filename = archive;
            _deviceData = await _jsonKeeper.Browse();
        }
    }
}