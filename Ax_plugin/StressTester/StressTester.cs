using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NickStrupat;
using AxPlugin;

namespace Perfomance
{
    /// <summary>
    /// Класс нагрузочного тестирования.
    /// </summary>
    public class StressTester
    {
        private AxParameters InitializeParameters()
        {
            var parameters = new AxParameters();

            parameters.SetParameter(ParamType.LengthBlade, new Parameter(100, 300, 200));
            parameters.SetParameter(ParamType.LengthHandle, new Parameter(300, 900, 600));
            parameters.SetParameter(ParamType.WidthHandle, new Parameter(20, 60, 40));
            parameters.SetParameter(ParamType.LengthButt, new Parameter(80, 270, 150));
            parameters.SetParameter(ParamType.ThicknessButt, new Parameter(24, 72, 48));
            parameters.SetParameter(ParamType.WidthButt, new Parameter(80, 150, 115));

            return parameters;
        }
        /// <summary>
        /// Метод для нагрузочного тестирования.
        /// </summary>
        public void StressTesting()
        {
            var builder = new Builder();
            var stopWatch = new Stopwatch();
            var parameters = InitializeParameters();

            Process currentProcess = Process.GetCurrentProcess();
            var count = 0;
            var streamWriter = new StreamWriter("log.txt");
            const double gigabyteInByte = 0.000000000931322574615478515625;

            while (true)
            {
                stopWatch.Start();
                builder.BuildAx(parameters);
                stopWatch.Stop();

                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory
                                  - computerInfo.AvailablePhysicalMemory)
                                 * gigabyteInByte;

                streamWriter.WriteLine($"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory:F2}");
                streamWriter.Flush();
                stopWatch.Reset();
            }
        }

        
        
    }
}
