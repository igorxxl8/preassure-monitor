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
    public partial class OpenTrendGraphicPage : ContentPage
    {
        private readonly DevicDataViewModel _vm;

        public OpenTrendGraphicPage()
        {
            InitializeComponent();
            _vm = new DevicDataViewModel();
            BindingContext = _vm;
            Setup();
        }

        private void Setup()
        {
            ((ObservableCollection<RealTimeDeviceData>) FirstChanelSeries.ItemsSource).Add(DeviceDataProvider.GetProvider.Data);

        
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
                var findRealTime = (LineSeries)SecondChanelChart.Series[indicex];
                ((ObservableCollection<RealTimeDeviceData>)findRealTime.ItemsSource).Add(DeviceDataProvider.GetProvider.Data);
            }
        }

        private void DataButton_OnClicked(object sender, EventArgs e)
        {
            Ch1Data.IsVisible = !Ch1Data.IsVisible;
            Ch2Data.IsVisible = !Ch2Data.IsVisible;
        }
    }
}