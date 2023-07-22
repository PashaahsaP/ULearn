using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
            var cells = GetCells(original);

            cells = cells.OrderByDescending(x => x.value).ToList();
            SetPixel(cells, original, whitePixelsFraction);
            
            return original;
		}

        private static void SetPixel(List<(double value, (int x, int y) position)> cells, 
                                     double[,] original,double whitePixelsFraction)
        {
            double temp = double.MaxValue;
            int xLength = original.GetLength(0);
            int yLength = original.GetLength(1);
            int count = (int)(xLength * yLength * whitePixelsFraction);

            foreach (var value in cells)
            {
                if (count > 0)
                {
                    if (count == 1)
                        temp = value.value;
                    count--;
                    original[value.position.x, value.position.y] = 1.0;
                }
                else
                {
                    if (temp != double.MaxValue && original[value.position.x, value.position.y] == temp)
                        original[value.position.x, value.position.y] = 1.0;
                    else
                        original[value.position.x, value.position.y] = 0.0;
                }
            }
        }

        private static List<(double value, (int x, int y) position)> GetCells(double[,] original)
        {
            int xLength = original.GetLength(0);
            int yLength = original.GetLength(1);
            var cells = new List<(double value, (int x, int y) position)>();

            for (int i = 0; i < xLength; i++)
                for (int j = 0; j < yLength; j++)
                    cells.Add((original[i, j], (i, j)));

            return cells;
        }
    }
}