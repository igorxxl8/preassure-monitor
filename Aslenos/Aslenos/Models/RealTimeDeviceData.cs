namespace Aslenos.Models
{
    public class RealTimeDeviceData
    {
        public int AxesX { get; set; }
        public double AxesY { get; set; }

        public RealTimeDeviceData(int x, double y)
        {
            AxesX = x;
            AxesY = y;
        }

        public RealTimeDeviceData()
        {
        }
    }
}