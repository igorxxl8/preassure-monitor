using System.Linq;
using Aslenos.Models;
using Aslenos.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenTrendPage : ContentPage
    {
        public OpenTrendPage(DeviceDataViewModel deviceData)
        {
            RealTimeDeviceData Comp(RealTimeDeviceData i1, RealTimeDeviceData i2) => i1.Fluctuation > i2.Fluctuation ? i1 : i2;

            InitializeComponent();

            var data = deviceData ?? new DeviceDataViewModel();
            BindingContext = data;


            Ch1Table.BindingContext = data.FirstChanelSeriesData.Aggregate(Comp);
            Ch2Table.BindingContext = data.SecondChanelSeriesData.Aggregate(Comp);
        }
    }
}