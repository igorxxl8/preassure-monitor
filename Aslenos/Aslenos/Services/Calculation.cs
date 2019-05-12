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

        private int packageCount = 0;
        private IList<int> packageCountList = new List<int>();

        private int packageAverageValue = 0;
        private int samplingTime = 0;

        private TimerCallback TimerCallback { get; }
        private Timer Timer { get; set; }

        public Calculation()
        {
            TimerCallback = new TimerCallback(ChangeBuffer);
        }

        public void AdcDataSplit(byte[] data)
        {
            packageCount++;

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

            if (packageCount > 0)
            {
                if (packageCountList.Count <= Constants.BUFFER_COUNT)
                {
                    packageCountList.Add(packageCount);
                    packageAverageValue += packageCount;
                }
                else
                {
                    packageAverageValue -= packageCountList[0];
                    packageCountList.RemoveAt(0);
                    packageCountList.Add(packageCount);
                    packageAverageValue += packageCount;
                }

                samplingTime = Constants.UPDATE_INTERVAL * 2 / packageAverageValue;
                packageCount = 1;
            }

            for (int i = 0; i < adcDataSize[bufferNumber ^ 1]; i++)
            {
                if (i % 2 == 0)
                {
                    var point = (adcData[bufferNumber ^ 1, i] - 289) / 5.85;
                    // TODO add to graphic = x: time(or just a number of point) y: point

                }
                else
                {
                    var point = (adcData[bufferNumber ^ 1, i] - 289) / 5.85;
                    // TODO add to graphic = x: time(or just a number of point) y: point

                }
            }
        }

        private void FindFluctuations(double point, int id)
        {

        }

        public void StartTimer()
        {
            Timer = new Timer(TimerCallback, null, 0, Constants.UPDATE_INTERVAL);
        }

        public void StopTimer()
        {
            Timer.Dispose();
        }

    }
}
