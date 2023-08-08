using System;
using System.Collections.Generic;
using System.Linq;

namespace Rivals;

public class RivalsTask
{
	public static IEnumerable<OwnedLocation> AssignOwners(Map map)
	{
		var visited = new HashSet<Point>(map.Players.Select(point => new Point(point.X,point.Y)));
		var queue = new Queue<OwnedLocation>(map.Players.Select((point,index) => 
			new OwnedLocation(index, new Point(point.X, point.Y), 0))) ;

		while (queue.Count > 0)
		{
			var point = queue.Dequeue();

			foreach (Point newPoint in GetAllPoint(point.Location, map))
            {
                if (visited.Contains(newPoint)) continue;
                visited.Add(newPoint);
                queue.Enqueue(new OwnedLocation(point.Owner, newPoint, point.Distance + 1));
            }

            yield return point;
		}

		yield break;
	}

    private static IEnumerable<Point> GetAllPoint(Point point, Map map)
    {
        for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
            {
                var newX = point.X + x;
                var newY = point.Y + y;
                var cond1 = newX >= 0 && newX < map.Maze.GetLength(0);
                var cond2 = newY >= 0 && newY < map.Maze.GetLength(1);
                var cond3 = Math.Abs(x + y) == 1;

                if (cond1 && cond2 && cond3 && map.Maze[newX, newY] != MapCell.Wall)
                    yield return new Point(newX, newY);
            }
    }
}