using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Aslenos.Models;

namespace Aslenos.ViewModel
{
    public class DeviceDataViewModel
    {
        public ObservableCollection<RealTimeDeviceData> FirstChanelSeriesData { get; set; }
        public ObservableCollection<RealTimeDeviceData> SecondChanelSeriesData { get; set; }

        public DeviceDataViewModel()
        {
        }

        public DeviceDataViewModel(IEnumerable<RealTimeDeviceData> first=default, IEnumerable<RealTimeDeviceData> second=default)
        {
            FirstChanelSeriesData = new ObservableCollection<RealTimeDeviceData>(first ?? new List<RealTimeDeviceData>());
            SecondChanelSeriesData = new ObservableCollection<RealTimeDeviceData>(second ?? new List<RealTimeDeviceData>());
        }
    }
}
