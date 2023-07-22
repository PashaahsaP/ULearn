using System.Collections.Generic;
namespace yield;
public static class MovingMaxTask
{
    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
    {
        var queue = new Queue<double>();
        var list = new LinkedList<double>();

        foreach (var point in data)
        {
            if (queue.Count == windowWidth)
            {
                if (list.First.Value == queue.Dequeue())
                    list.RemoveFirst();
                AppendValue(ref list, point, queue);
                yield return point.WithMaxY(list.First.Value);
            }
            else
            {
                AppendValue(ref list, point, queue);
                yield return point.WithMaxY(list.First.Value);
            }
        }
    }

    static void AppendValue(ref LinkedList<double> list, DataPoint point, Queue<double> queue)
    {
        if (list.Last != null)
        {
            if (list.First.Value > point.OriginalY)
                RemoveLesserValue(list, point);
            else if (list.First.Value < point.OriginalY)
            {
                list = new LinkedList<double>();
                list.AddLast(new LinkedListNode<double>(point.OriginalY));
            }
        }
        else
            list.AddLast(new LinkedListNode<double>(point.OriginalY));

        queue.Enqueue(point.OriginalY);
    }

    static void RemoveLesserValue(LinkedList<double> list,DataPoint point)
    {
        while (true)
        {
            if (list.Last.Value > point.OriginalY)
            {
                list.AddLast(new LinkedListNode<double>(point.OriginalY));
                break;
            }
            else
                list.RemoveLast();
        }
    }
}
