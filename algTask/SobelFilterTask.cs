using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            int demensions = sx.GetLength(0) / 2;

            for (int y = demensions; y <= height - demensions-1; y++)
                for (int x = demensions; x <= width - demensions-1; x++)
                {
                    (double baseMatrix,double transpodedMatrix) sumSX = GetSumMatrixs(sx, g, y-demensions, x-demensions);
                    result[x, y] = Math.Sqrt(sumSX.baseMatrix * sumSX.baseMatrix  + sumSX.transpodedMatrix * sumSX.transpodedMatrix);
                }

            return result;
        }

        private static (double,double) GetSumMatrixs(double[,] sx, double[,] origin, int i, int k)
        {
            var width = sx.GetLength(0);
            var height = sx.GetLength(1);
            double matrixSum = 0;
            double matrixTranspodedSum = 0;
            int y = i;

            for (int j = 0, e = 0; j < height; e++, j++, y++)
            {
                int x = k;
                for (int l = 0, u = 0; l < width; u++, l++, x++)
                {
                    matrixSum += sx[l, j] * origin[x, y];
                    matrixTranspodedSum += sx[e, u] * origin[x, y];
                }
            }

            return (matrixSum, matrixTranspodedSum);
        }
    }
}