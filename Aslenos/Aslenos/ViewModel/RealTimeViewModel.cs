using System;
using System.Collections;
using System.Collections.ObjectModel;
using Aslenos.Models;

namespace Aslenos.ViewModel
{
    public class RealTimeViewModel
    {
        public ObservableCollection<RealTimeDeviceData> FirstChanelSeriesData { get; set; }
        public ObservableCollection<RealTimeDeviceData> SecondChanelSeriesData { get; set; }

        public RealTimeViewModel()
        {
            FirstChanelSeriesData = new ObservableCollection<RealTimeDeviceData>();
            SecondChanelSeriesData = new ObservableCollection<RealTimeDeviceData>();
        }
    }
}
