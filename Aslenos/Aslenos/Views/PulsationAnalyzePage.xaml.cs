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
        private readonly RealTimeViewModel _vm;
        private readonly StoppableTimer _timer;

        public PulsationAnalyzePage()
        {
            InitializeComponent();
            _vm = new RealTimeViewModel();
            _timer = new StoppableTimer(TimeSpan.FromSeconds(1), TimerTick);
            BindingContext = _vm;
        }

        private void TimerTick()
        {
            var indice = FirstChanelChart.Series.IndexOf(FirstChanelSeries);
            if (indice < 0)
            {
                FirstChanelSeries.ItemsSource = _vm.FirstChanelSeriesData;
                FirstChanelSeries.XBindingPath = "AxesX";
                FirstChanelSeries.YBindingPath = "AxesY";
                FirstChanelChart.Series.Add(FirstChanelSeries);
            }
            else
            {
                var findRealTime = (LineSeries) FirstChanelChart.Series[indice];
                ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Add(DeviceDataProvider.GetProvider
                    .Data);

            }
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