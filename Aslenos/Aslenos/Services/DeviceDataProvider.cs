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
            Data = new RealTimeDeviceData();
        }

        public static DeviceDataProvider GetProvider { get; } = new DeviceDataProvider();
        public RealTimeDeviceData Data { get; }
    }
}
