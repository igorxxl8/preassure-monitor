﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aslenos.Services
{
    public class MockImpulseInvoker
    {
        private readonly StoppableTimer _timer;
        private readonly DeviceDataProvider _dataProvider;

        public MockImpulseInvoker()
        {
            _dataProvider = DeviceDataProvider.GetProvider;
            _timer = new StoppableTimer(TimeSpan.FromSeconds(1), Callback);
        }

        private void Callback()
        {
            _dataProvider.FirstChanel.AxesX++;
            _dataProvider.FirstChanel.AxesY++;
            _dataProvider.SecondChanel.AxesX+=2;
            _dataProvider.SecondChanel.AxesY+=2;
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
