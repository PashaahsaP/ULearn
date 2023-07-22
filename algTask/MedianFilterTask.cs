using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        public static double[,] MedianFilter(double[,] original)
        {
            int xLength = original.GetLength(0);
            int yLength = original.GetLength(1);
            var result = new double[xLength, yLength];

            for (int i = 0; i <xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    result[i, j] = AppendFilteredPixel(original, i, j);
                }
            }

            return result;
        }

        private static double AppendFilteredPixel(double[,] original, int i, int j)
        {
            List<double> validCells = GetValidCells(original, i, j);

            if (validCells.Count % 2 == 0 && validCells.Count != 0)
                return (validCells[validCells.Count / 2] + validCells[validCells.Count / 2 - 1]) / 2;
            else if (validCells.Count % 2 == 1)
                return validCells[validCells.Count / 2];
            else
                return original[i, j];
        }

        private static List<double> GetValidCells(double[,] original, int i, int j)
        {
            var result = new List<double>(9);
            int xLength = original.GetLength(0);
            int yLength = original.GetLength(1);

            for (int k = i - 1, q = 0; q < 3; q++, k++)
            {
                for (int l = j - 1, w = 0; w < 3; w++, l++)
                {
                    if (k >= 0 && k < xLength && l >= 0 && l < yLength)
                        result.Add(original[k, l]);
                }
            }

            result.Sort();
            return result;
        }
    }
}