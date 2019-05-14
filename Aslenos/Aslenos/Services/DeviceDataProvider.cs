using Aslenos.Models;

namespace Aslenos.Services
{
    public sealed class DeviceDataProvider
    {

        static DeviceDataProvider()
        {

        }

        private DeviceDataProvider()
        {
            FirstChanel = new RealTimeDeviceData();
            SecondChanel = new RealTimeDeviceData();
        }

        public static DeviceDataProvider GetProvider { get; } = new DeviceDataProvider();
        public RealTimeDeviceData FirstChanel { get; set; }
        public RealTimeDeviceData SecondChanel { get; set; }
    }
}
