using Parcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PARCSMatrixMultiply
{
    public class MatrixMult : IModule
    {
        private int[] Multiply(int height, int width, List<int[]> matr2, int[] row)
        {
            var ans = new int[width];
            for(var i = 0; i < width; i++)
            {
                var elem = 0;
                Console.WriteLine(i);
                for (var j = 0; j < height; j++)
                {
                    elem += row[j] * matr2.ElementAt(j)[i];
                }
                ans.SetValue(elem, i);
            }
            return ans;
        }
        public void Run(ModuleInfo info, CancellationToken token = default)
        {
            var matr1 = (Matrix)info.Parent.ReadObject(typeof(Matrix));
            var matr2 = (Matrix)info.Parent.ReadObject(typeof(Matrix));
            var rowInPoint = info.Parent.ReadObject<List<int>>();

            var result = new List<int[]>();
            foreach (int item in rowInPoint)
            {
                result.Add(Multiply(matr2.Height, matr2.Width, matr2.Data, matr1.Data.ElementAt(item)));
            }
            Console.WriteLine(result.Count);
            info.Parent.WriteObject(result);
        }
    }
}
