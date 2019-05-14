using System;
using System.Collections.Generic;
using Aslenos.Models;
using Aslenos.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenTrendGraphicPage : ContentPage
    {

        public OpenTrendGraphicPage()
        {
            InitializeComponent();

            // load data in graphic here
            BindingContext = new DeviceDataViewModel(
                new List<RealTimeDeviceData>
                {
                    new RealTimeDeviceData
                    {
                        AxesX = 1,
                        AxesY = 2
                    },
                    new RealTimeDeviceData
                    {
                        AxesX = 3,
                        AxesY = 9
                    },
                    new RealTimeDeviceData
                    {
                        AxesX = 7,
                        AxesY = 20
                    },
                    new RealTimeDeviceData
                    {
                        AxesX = 4,
                        AxesY = 5
                    }
                },
                new List<RealTimeDeviceData>
                {
                    new RealTimeDeviceData
                    {
                        AxesX = 9,
                        AxesY = 8
                    },
                    new RealTimeDeviceData
                    {
                        AxesX = 7,
                        AxesY = 4
                    },
                    new RealTimeDeviceData
                    {
                        AxesX = 75,
                        AxesY = 2
                    },
                    new RealTimeDeviceData
                    {
                        AxesX = 42,
                        AxesY = 0
                    }
                }
            );
        }

        private void DataButton_OnClicked(object sender, EventArgs e)
        {
            Ch1Data.IsVisible = !Ch1Data.IsVisible;
            Ch2Data.IsVisible = !Ch2Data.IsVisible;
        }
    }
}