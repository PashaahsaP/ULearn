using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
    public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
    {
        var queue = new Queue<double>();
        double average = 0;

        foreach (var point in data)
        {
            if (queue.Count == 0)
            {
                average = point.OriginalY;
                queue.Enqueue(average);
                yield return point.WithAvgSmoothedY(point.OriginalY);
            }
            else
            {
                CalculateMovingAverage(point.OriginalY, queue,ref average, windowWidth);
                yield return point.WithAvgSmoothedY(average);
            }
        }
    }

    static void CalculateMovingAverage(double yValue, Queue<double> queue,ref double average, int windowWidth)
    {
        if (queue.Count == windowWidth)
        {
            average = ((queue.Count * average) - queue.Dequeue() + yValue) / (1 + queue.Count);
            queue.Enqueue(yValue);
        }
        else
        {
            average = ((queue.Count * average) + yValue) / (1 + queue.Count);
            queue.Enqueue(yValue);
        }
    }
}