using System;
using System.Collections.Generic;
using System.Text;

namespace Aslenos.Services
{
    public class MockImpulseInvoker
    {
        private readonly StoppableTimer _timer;

        public MockImpulseInvoker()
        {
            _timer = new StoppableTimer(TimeSpan.FromSeconds(1), Callback);
        }

        private void Callback()
        {
            DeviceDataProvider.GetProvider.Data.AxesX++;
            DeviceDataProvider.GetProvider.Data.AxesY++;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
