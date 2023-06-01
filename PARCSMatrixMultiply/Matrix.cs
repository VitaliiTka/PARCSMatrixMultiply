using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PARCSMatrixMultiply
{
    [Serializable]
    public class Matrix
    {
        public List<int[]> Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Matrix(List<int[]> data, int height, int width)
        {
            Data = data;
            Height = height;
            Width = width;
        }

        public void Write()
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    Console.Write(Data.ElementAt(i)[j].ToString().PadLeft(4));
                }
                Console.WriteLine();
            }
        }
    }
}
