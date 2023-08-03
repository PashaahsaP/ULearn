using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
	{
		var visited = new HashSet<Point>();
        var mapChest = chests.ToHashSet();
		var linkedList = new SinglyLinkedList<Point>(start);
		var queue = new Queue<SinglyLinkedList<Point>>();
		queue.Enqueue(linkedList);
        visited.Add(start);

        while (queue.Count != 0)
        {
			var initPoint = queue.Dequeue();
            foreach (var point in GetAllPoint(initPoint.Value, map))
            {
                if (visited.Contains(point)) continue;
                visited.Add(point);
                var temp = new SinglyLinkedList<Point>(point, initPoint);
                if (mapChest.Contains(point)) yield return temp;
                queue.Enqueue(temp);
            }
        }
    }

    private static IEnumerable<Point> GetAllPoint(Point point,Map map)
    {
        for (int x = -1; x < 2; x++)
        for (int y = -1; y < 2; y++)
        {
            var newX = point.X + x;
            var newY = point.Y + y;
            var cond1 = newX >= 0 && newX < map.Dungeon.GetLength(0);
            var cond2 = newY >= 0 && newY < map.Dungeon.GetLength(1);
            var cond3 = Math.Abs(x + y) == 1;
       
            if(cond1 && cond2 && cond3 && map.Dungeon[newX, newY] != MapCell.Wall)
                yield return new Point(newX, newY);
        }
    }
}