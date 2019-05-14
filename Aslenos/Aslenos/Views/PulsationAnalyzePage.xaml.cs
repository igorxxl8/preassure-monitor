using System;
using System.Collections.ObjectModel;
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

        public PulsationAnalyzePage()
        {
            InitializeComponent();
            _vm = new DeviceDataViewModel();
            _ddp = DeviceDataProvider.GetProvider;
            _timer = new StoppableTimer(TimeSpan.FromSeconds(1), TimerTick);
            FirstChanelSeries.ItemsSource = _vm.FirstChanelSeriesData;
            SecondChanelSeries.ItemsSource = _vm.SecondChanelSeriesData;
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
    }
}