using System;
using System.Collections.ObjectModel;
using System.Linq;
using Aslenos.Helpers;
using Aslenos.Models;
using Aslenos.Services;
using Aslenos.ViewModel;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenTrendGraphicPage : ContentPage
    {
        private readonly RealTimeViewModel _vm;
        private readonly StoppableTimer _timer;

        public OpenTrendGraphicPage()
        {
            InitializeComponent();
            _vm = new RealTimeViewModel();
            _timer = new StoppableTimer(TimeSpan.FromMilliseconds(Constants.UPDATE_INTERVAL), TimerTick);
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
                var findRealTime = (LineSeries)FirstChanelChart.Series[indice];
                ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Add(DeviceDataProvider.GetProvider.Data);

            }

            var indicex = SecondChanelChart.Series.IndexOf(SecondChanelSeries);
            if (indicex < 0)
            {
                SecondChanelSeries.ItemsSource = _vm.SecondChanelSeriesData;
                SecondChanelSeries.XBindingPath = "AxesX";
                SecondChanelSeries.YBindingPath = "AxesY";
                SecondChanelChart.Series.Add(SecondChanelSeries);
            }
            else
            {
                var findRealTime = (LineSeries)SecondChanelChart.Series[indice];
                ((ObservableCollection<RealTimeDeviceData>)findRealTime.ItemsSource).Add(DeviceDataProvider.GetProvider.Data);
            }
        }

        private void DataButton_OnClicked(object sender, EventArgs e)
        {
            Ch1Data.IsVisible = !Ch1Data.IsVisible;
            Ch2Data.IsVisible = !Ch2Data.IsVisible;
        }

        private void StopButton_OnClicked(object sender, EventArgs e)
        {
            _timer.Stop();
        }

        private void StartButton_OnClicked(object sender, EventArgs e)
        {
            _timer.Start();
        }
    }
}