using System;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Threading;
using Parcs;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace PARCSMatrixMultiply
{
    internal class Program : IModule
    {
        static void Main()
        {
            var job = new Job();
            if (!File.Exists(Assembly.GetExecutingAssembly().Location))
            {
                Console.WriteLine("File doesn't exist");
                return;
            }
            job.AddFile(Assembly.GetExecutingAssembly().Location);
            (new Program()).Run(new ModuleInfo(job, null));
            Console.ReadKey();
        }

        public void Run(ModuleInfo info, CancellationToken token = default)
        {
            var sw = new Stopwatch();
            var readingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataFiles";

            var firstMatrix = MatrixService.ReadMatrix(readingPath + @"\FirstMatrix.txt");
            var secondMatrix = MatrixService.ReadMatrix(readingPath + @"\SecondMatrix.txt");

            sw.Start();
            const int pointsNum = 1;
            var points = new IPoint[pointsNum];
            var channels = new IChannel[pointsNum];
            for (var i = 0; i < pointsNum; i++)
            {
                points[i] = info.CreatePoint();
                channels[i] = points[i].CreateChannel();
                points[i].ExecuteClass("PARCSMatrixMultiply.MatrixMult");
            }

            var rowInPoint = MatrixService.DivideRowsByNumber(firstMatrix.Height, pointsNum);

            for (var i = 0; i < pointsNum; i++)
            {
                channels[i].WriteObject(firstMatrix);
                channels[i].WriteObject(secondMatrix);
                channels[i].WriteObject(rowInPoint[i]);
            }

            var result = new List<int[]>();
            for (var i = 0; i < pointsNum; i++)
            {
                result.AddRange((List<int[]>)channels[i].ReadObject(typeof(List<int[]>)));
            }
            sw.Stop();

            MatrixService.WriteMatrixToFile(result, readingPath + @"\Output.txt");

            Console.WriteLine($"Total time {sw.ElapsedMilliseconds} ms");
        }
    }
}
