using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARCSMatrixMultiply
{
    public static class MatrixService
    {
        public static Matrix ReadMatrix(string filePath)
        {

            var lines = File.ReadLines(filePath).ToList();
            var height = int.Parse(lines[0]);
            var width = int.Parse(lines[1]);
            lines.Remove(lines[0]);
            lines.Remove(lines[0]);
            var result = new List<int[]>();
            foreach (string line in lines)
            {
                var array = line.Split(' ').Select(s => int.Parse(s)).ToArray();
                result.Add(array);
            }
            return new Matrix(result, height, width);
        }

        public static void WriteMatrixToFile(List<int[]> matrix, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (int[] row in matrix)
                {
                    for (var i = 0; i < row.Length; i++)
                    {
                        writer.Write(row[i]);

                        if (i < row.Length - 1)
                        {
                            writer.Write(" ");
                        }
                    }

                    writer.WriteLine();
                }
            }
        }

        public static List<List<int>> DivideRowsByNumber(int matrixHeight, int number)
        {
            var rowNumbers = new List<List<int>>();

            var rowsPerNumber = matrixHeight / number;
            var remainingRows = matrixHeight % number;

            var currentRow = 0;
            for (var i = 0; i < number; i++)
            {
                var rows = new List<int>();

                int rowsToAdd = rowsPerNumber;
                if (i == number - 1)
                {
                    rowsToAdd += remainingRows;
                }

                for (var j = 0; j < rowsToAdd; j++)
                {
                    rows.Add(currentRow);
                    currentRow++;
                }

                rowNumbers.Add(rows);
            }

            return rowNumbers;
        }
    }
}
