using System;
using System.Collections.Generic;
using System.Linq;
using Aslenos.Models;
using Aslenos.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenTrendGraphicPage : ContentPage
    {

        public OpenTrendGraphicPage(DeviceDataViewModel deviceData)
        {
            RealTimeDeviceData Comp(RealTimeDeviceData i1, RealTimeDeviceData i2) => i1.Fluctuation > i2.Fluctuation ? i1 : i2;

            InitializeComponent();

            var data = deviceData ?? new DeviceDataViewModel();
            BindingContext = data;


            Ch1Data.BindingContext = data.FirstChanelSeriesData.Aggregate(Comp);
            Ch2Data.BindingContext = data.SecondChanelSeriesData.Aggregate(Comp);
        }

        private void DataButton_OnClicked(object sender, EventArgs e)
        {
            Ch1Data.IsVisible = !Ch1Data.IsVisible;
            Ch2Data.IsVisible = !Ch2Data.IsVisible;
        }
    }
}