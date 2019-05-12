using Aslenos.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aslenos.Services
{
    class Calculation
    {
        private int[,] adcData = new int[2, Constants.MAX_BUFF_SIZE];
        private int[] adcDataSize = new int[2];
        private int bufferNumber = 0;

        private int countPackage = 0;

        private TimerCallback timerCallback;
        private Timer timer;

        public Calculation()
        {
            timerCallback = new TimerCallback(ChangeBuffer);
        }

        public void AdcDataSplit(byte[] data)
        {
            countPackage++;

            if (data.Length % 2 == 0)
            {
                for (int i = 0; i < data.Length; i += 2)
                {
                    adcData[bufferNumber, adcDataSize[bufferNumber]] = (data[i] << 8) + data[i + 1];
                    adcDataSize[bufferNumber]++;

                    if (adcDataSize[bufferNumber] > Constants.MAX_BUFF_SIZE)
                    {
                        adcDataSize[bufferNumber] = 0;
                    }
                }
            }
        }

        private void ChangeBuffer(object obj)
        {
            bufferNumber ^= 1;

            if (countPackage > 0)
            {

            }
        }

        public void StartTimer()
        {
            timer = new Timer(timerCallback, null, 0, Constants.UPDATE_INTERVAL);
        }

        public void StopTimer()
        {
            timer.Dispose();
        }

    }
}
