using Aslenos.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Aslenos.Models;

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

        static bool receptionDataFlag = false;
        static bool adcGraphUpdateFlag = false;
        static bool[] manometerUpdateFlag = new bool[2];
        static bool[] manometerUpdateFlag_t = new bool[2];

        static bool[] series_fluctuations_flag = { true, true };
        static bool[] series_phase_e_flag = { false, false };
        static bool[] series_phase_f_flag = { false, false };
        static bool[] series_phase_b_flag = { false, false };
        static bool[] graph_array_flag = { true, true };

        static double[,] graphX_params = new double[2, 9];
        static double[,] graphX_params_scrin = new double[2, 9];

        static double[] fluctuations_time = new double[2];
        static double[] series_max_vacuum_buf = new double[2];
        static double[] total_volume = new double[2];

        static int[,] series_x_s = new int[2, 4];
        static int[,] series_x_s_scrin = new int[2, 4];

        static IList<double>[] graph_array_preBuf = new List<double>[] { new List<double>(), new List<double>() };
        static IList<double>[] graph_array_buf_t = new List<double>[] { new List<double>(), new List<double>() };
        int array_preBuf_max_length = 50;

        static IList<double>[] graph_array = new List<double>[] { new List<double>(), new List<double>() };
        static IList<double>[] graph_array_scrin = new List<double>[] { new List<double>(), new List<double>() };


        static int[] series_filter = new int[2];
        static int num_filter = 3;

        static bool fluctuation_analys_flag = true;

        public Calculation()
        {
            TimerCallback = ChangeBuffer;
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

            var realtimeData = DeviceDataProvider.GetProvider.Data;
            for (int i = 0; i < adcDataSize[bufferNumber ^ 1]; i++)
            {
                if (i % 2 == 0)
                {
                    var point = (adcData[bufferNumber ^ 1, i] - 289) / 5.85;
                    FindFluctuations(point, 0);
                    // TODO add to graphic = x: time(or just a number of point) y: point
                    realtimeData.AxesX++;
                    realtimeData.AxesY = point;

                }
                else
                {
                    var point = (adcData[bufferNumber ^ 1, i] - 289) / 5.85;
                    FindFluctuations(point, 1);
                    realtimeData.AxesX++;
                    realtimeData.AxesY = point;
                    // TODO add to graphic = x: time(or just a number of point) y: point

                }
            }
        }

        private void FindFluctuations(double ch, int s_ID)
        {
            if (ch < graphX_params[s_ID, GraphParams.MIN_VACUUM])
            {
                graphX_params[s_ID, GraphParams.MIN_VACUUM] = ch;
            }
            else if (ch > series_max_vacuum_buf[s_ID]) series_max_vacuum_buf[s_ID] = ch;
            if (fluctuation_analys_flag)
            {
                fluctuations_time[s_ID] += samplingTime;

                if (graph_array_flag[s_ID])
                {
                    if (graph_array_preBuf[s_ID].Count > array_preBuf_max_length)
                    {
                        graph_array_preBuf[s_ID].RemoveAt(0);
                        graph_array_preBuf[s_ID].Add(ch);
                    }
                    else graph_array_preBuf[s_ID].Add(ch);
                }

                if (series_fluctuations_flag[s_ID] && ch > 3.5)
                {
                    if (SeriesFilter(s_ID))
                    {
                        graphX_params[s_ID, GraphParams.PHASE_F] = fluctuations_time[s_ID];
                        graphX_params[s_ID, GraphParams.FLUCTUATION] = graphX_params[s_ID, GraphParams.PHASE_F] + graphX_params[s_ID, GraphParams.PHASE_E];
                        fluctuations_time[s_ID] = 0;

                        graphX_params[s_ID, GraphParams.MAX_VACUUM] = series_max_vacuum_buf[s_ID];
                        series_max_vacuum_buf[s_ID] = 0;

                        save_trend_volume(s_ID);
                        for (int i = 0; i < graph_array_preBuf[s_ID].Count; i++)
                        {
                            graph_array_buf_t[s_ID].Add(graph_array_preBuf[s_ID][i]);
                        }
                        series_x_s[s_ID, GraphParams.FLUCTUATION] = graph_array_buf_t[s_ID].Count;

                        series_fluctuations_flag[s_ID] = false;
                        graph_array_flag[s_ID] = false;
                        series_phase_e_flag[s_ID] = true;
                        series_phase_b_flag[s_ID] = true;
                        // Это для надписей в графика
                        // BluetoothBase.HandlerMessageSet(Constants.SERIES0_EVT[s_ID]);
                    }
                }

                if (graph_array_buf_t[s_ID] != null)
                {
                    if (graph_array_buf_t[s_ID].Count > 3000)
                    {
                        graph_array_buf_t[s_ID].RemoveAt(0);
                        graph_array_buf_t[s_ID].Add(ch);
                    }
                    else graph_array_buf_t[s_ID].Add(ch);
                }

                if (series_phase_e_flag[s_ID] && ch < (series_max_vacuum_buf[s_ID] - 4))
                {
                    graphX_params[s_ID, GraphParams.PHASE_E] = fluctuations_time[s_ID];
                    series_x_s[s_ID, GraphParams.PHASE_B] = graph_array_buf_t[s_ID].Count;
                    find_phase_a(s_ID);
                    fluctuations_time[s_ID] = 0;
                    series_phase_e_flag[s_ID] = false;
                    series_phase_f_flag[s_ID] = true;
                }

                if (series_phase_f_flag[s_ID] && ch < 3.5)
                {
                    if (SeriesFilter(s_ID))
                    {
                        graphX_params[s_ID, GraphParams.PHASE_C] = fluctuations_time[s_ID];
                        series_x_s[s_ID, GraphParams.PHASE_C] = graph_array_buf_t[s_ID].Count;

                        series_fluctuations_flag[s_ID] = true;
                        series_phase_f_flag[s_ID] = false;
                        graph_array_flag[s_ID] = true;
                        graph_array_preBuf[s_ID].Clear();
                        graphX_params[s_ID, GraphParams.MIN_VACUUM] = 50.0;
                    }
                }
            }
            else
            {
                if (!manometerUpdateFlag[s_ID]) manometerUpdateFlag[s_ID] = true;
                total_volume[s_ID] = ch;
                if (manometerUpdateFlag_t[s_ID])
                {
                    manometerUpdateFlag_t[s_ID] = false;
                    // Это для надписей в графика
                    // BluetoothBase.HandlerMessageSet(Constants.SERIES0_EVT[s_ID]);
                }
            }
        }

        private bool SeriesFilter(int s_ID)
        {
            series_filter[s_ID]++;
            if (series_filter[s_ID] > num_filter)
            {
                series_filter[s_ID] = 0;
                return true;
            }
            else return false;
        }

        private void find_phase_a(int ID)
        {
            int max_size = graph_array_buf_t[ID].Count;
            int null_point = graph_array_preBuf[ID].Count;
            int phase_e_length = max_size - null_point;
            double step_time = graphX_params[ID, GraphParams.PHASE_E] / phase_e_length;
            double phase_a_time = 0.0;

            for (int i = null_point; i < max_size; i++)
            {
                phase_a_time += step_time;
                if (graph_array_buf_t[ID][i] > (series_max_vacuum_buf[ID] - 4))
                {
                    graphX_params[ID, GraphParams.PHASE_A] = phase_a_time;
                    series_x_s[ID, GraphParams.PHASE_A] = i;
                    return;
                }
            }

        }

        private void save_trend_volume(int s_ID)
        {
            graph_array[s_ID].Clear();
            for (int i = 0; i < graph_array_buf_t[s_ID].Count; i++)
            {
                graph_array[s_ID].Add(graph_array_buf_t[s_ID][i]);
            }
            graph_array_buf_t[s_ID].Clear();
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
