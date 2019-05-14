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

        public OpenTrendGraphicPage(DeviceDataViewModel deviceData)
        {
            InitializeComponent();

            BindingContext = deviceData ?? new DeviceDataViewModel();
        }

        private void DataButton_OnClicked(object sender, EventArgs e)
        {
            Ch1Data.IsVisible = !Ch1Data.IsVisible;
            Ch2Data.IsVisible = !Ch2Data.IsVisible;
        }
    }
}