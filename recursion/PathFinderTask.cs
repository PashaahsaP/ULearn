using System;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static Point[] CheckPoints { get; set; }
        public static int[] ShortestPath { get; set; }
        public static double ShortestDistance { get; set; } = double.MaxValue;

        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            CheckPoints = checkpoints;
            var shortestPath = new int[checkpoints.Length];
            MakeTrivialPermutation(new int[checkpoints.Length], 1, 0);
            ShortestDistance = double.MaxValue;

            return ShortestPath;
        }

        private static void MakeTrivialPermutation(int[] permutation, int position, double currDistance)
        {
            if (position >= 2)
                currDistance += DistanceBetweenPoints(CheckPoints[permutation[position - 2]], CheckPoints[permutation[position - 1]]);
            if (currDistance > ShortestDistance)
                return;
            if (position == permutation.Length)
            {
                if (currDistance < ShortestDistance)
                {
                    ShortestDistance = currDistance;
                    ShortestPath = permutation.ToArray();
                }
                return;
            }

            for (int i = 1; i < permutation.Length; i++)
            {
                int index = Array.IndexOf(permutation, i, 0, position);
                if (index == -1)
                {
                    permutation[position] = i;
                    MakeTrivialPermutation(permutation, position + 1, currDistance);
                }
            }
        }

        private static double DistanceBetweenPoints(Point one, Point second)
        {
            int xDif = (one.X - second.X) * (one.X - second.X);
            int yDif = (one.Y - second.Y) * (one.Y - second.Y);
            return Math.Sqrt(xDif + yDif);
        }
    }
}