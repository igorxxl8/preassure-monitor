using System.Collections.ObjectModel;
using Aslenos.Models;

namespace Aslenos.ViewModel
{
    public class DevicDataViewModel
    {
        public ObservableCollection<RealTimeDeviceData> FirstChanelSeriesData { get; set; }
        public ObservableCollection<RealTimeDeviceData> SecondChanelSeriesData { get; set; }

        public DevicDataViewModel()
        {
            FirstChanelSeriesData = new ObservableCollection<RealTimeDeviceData>();
            SecondChanelSeriesData = new ObservableCollection<RealTimeDeviceData>();
        }
    }
}
