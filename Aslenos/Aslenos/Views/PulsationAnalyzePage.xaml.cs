using System;
using System.Collections.ObjectModel;
using Aslenos.Interfaces;
using Aslenos.Models;
using Aslenos.Services;
using Aslenos.ViewModel;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PulsationAnalyzePage : ContentPage
    {
        private readonly DeviceDataViewModel _vm;
        private readonly StoppableTimer _timer;
        private readonly DeviceDataProvider _ddp;
        private readonly JsonDataKeeper<DeviceDataViewModel> _jsonKeeper;

        public PulsationAnalyzePage()
        {
            InitializeComponent();
            _vm = new DeviceDataViewModel();
            _ddp = DeviceDataProvider.GetProvider;
            _timer = new StoppableTimer(TimeSpan.FromSeconds(1), TimerTick);
            FirstChanelSeries.ItemsSource = _vm.FirstChanelSeriesData;
            SecondChanelSeries.ItemsSource = _vm.SecondChanelSeriesData;
            _jsonKeeper = new JsonDataKeeper<DeviceDataViewModel>();
        }

        private void TimerTick()
        {
            _vm.FirstChanelSeriesData.Add(_ddp.FirstChanel);
            _vm.SecondChanelSeriesData.Add(_ddp.SecondChanel);
        }

        private void DataButton_OnClicked(object sender, EventArgs e)
        {
            Ch1Data.IsVisible = !Ch1Data.IsVisible;
        }

        private void StopButton_OnClicked(object sender, EventArgs e)
        {
            _timer.Stop();
        }

        private void StartButton_OnClicked(object sender, EventArgs e)
        {
            _timer.Start();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "preventLandScape");
        }

        private void SaveButton_OnClicked(object sender, EventArgs e)
        {
            _jsonKeeper.Filename = $"{DateTime.Now:MM_dd_yyyy_HH_mm_ss}.arc";
            _jsonKeeper.Save(_vm);
        }
    }
}