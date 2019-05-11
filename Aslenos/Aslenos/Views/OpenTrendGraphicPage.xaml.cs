using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Aslenos.Models;
using Aslenos.ViewModel;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Xamarin.Forms.Device;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenTrendGraphicPage : ContentPage
    {
        private readonly Random _random;
        private readonly RealTimeViewModel _vm;

        public OpenTrendGraphicPage()
        {
            InitializeComponent();
            _vm = new RealTimeViewModel();
            _random = new Random();
            BindingContext = _vm;
            Device.StartTimer(TimeSpan.FromSeconds(1), TimerTick);

        }

        private bool TimerTick()
        {
            var indice = FirstChanelChart.Series.IndexOf(FirstChanelSeries);
            if (indice < 0)
            {
                FirstChanelSeries.ItemsSource = _vm.FirstChanelSeriesData; 
                FirstChanelSeries.XBindingPath = "Speed";
                FirstChanelSeries.YBindingPath = "Rate";
                FirstChanelChart.Series.Add(FirstChanelSeries);
            }
            else
            {
                var findRealTime = (LineSeries)FirstChanelChart.Series[indice];
                int index = ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Count - 1;
                if (index < 0)
                {
                    ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource)?.Add(new RealTimeDeviceData() { Speed = 1, Rate = _random.Next(0, 10000) });
                }
                else
                {
                    var rtd = ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Last();
                    ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Add(new RealTimeDeviceData() { Speed = rtd.Speed + 1, Rate = _random.Next(0, 10000) });
                }
            }

            var indicex = SecondChanelChart.Series.IndexOf(SecondChanelSeries);
            if (indicex < 0)
            {
                SecondChanelSeries.ItemsSource = _vm.SecondChanelSeriesData;
                SecondChanelSeries.XBindingPath = "Speed";
                SecondChanelSeries.YBindingPath = "Rate";
                SecondChanelChart.Series.Add(SecondChanelSeries);
            }
            else
            {
                var findRealTime = (LineSeries)SecondChanelChart.Series[indicex];
                int index = ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Count - 1;
                if (index < 0)
                {
                    ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Add(new RealTimeDeviceData() { Speed = 1, Rate = _random.Next(0, 10000) });
                }
                else
                {
                    var rtd = ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Last();
                    ((ObservableCollection<RealTimeDeviceData>) findRealTime.ItemsSource).Add(new RealTimeDeviceData() { Speed = rtd.Speed + 1, Rate = _random.Next(0, 10000) });
                }
            }

            return true;
        }
    }
}