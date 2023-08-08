using System;
using System.Collections.Generic;
using System.Linq;
namespace Dungeon;
public class DungeonTask
{
	public static MoveDirection[] FindShortestPath(Map map)
    {
        var pathToExit = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();// get shortest path to exit
        //Default check
        if (pathToExit == null) return new MoveDirection[0];//return default if not path
        if (map.Chests.Any(chest => pathToExit.ToList().Contains(chest)))// check if path have chests and return if have
            return GetDirections(pathToExit).Reverse().ToArray();
        //Iterate all variants
        var pathsToChests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);// else find paths to chestes
        if (!pathsToChests.Any()) return GetDirections(pathToExit).ToArray();
        IEnumerable<MoveDirection> shortestPath = new List<MoveDirection>();// shortest path for optimize method
        GetPath(ref shortestPath,map, pathsToChests);

        return shortestPath.ToArray();
    }

    private static void GetPath(ref IEnumerable<MoveDirection> shortestPath, Map map, IEnumerable<SinglyLinkedList<Point>> pathsToChests)
    {
        int minPath = int.MaxValue; // for optimize search path, if current length of path more than minPath then execute continue of cycle
       
        foreach (var path in pathsToChests)
        {
            if (path.Length>minPath) continue;
            var prevPathDir = GetDirections(path);// get directions from start to chest
            var pathFromChest = BfsTask.FindPaths(map, path.Value, new Point[] { map.Exit }).FirstOrDefault();//find path from chest to exit point
            if (pathFromChest != null)
            {
                if ((path.Length + pathFromChest.Length)>minPath) continue; // discarding  useless calculation
                var pathFromChestDir = GetDirections(pathFromChest); //get directions from chest to finish
                shortestPath = pathFromChestDir.Concat(prevPathDir).Reverse();
                minPath = path.Length + pathFromChest.Length; // set new min path length
            }
        }
    }

    private static IEnumerable<MoveDirection> GetDirections(SinglyLinkedList<Point> singlyLinkedList)
    {
        while (singlyLinkedList.Previous!=null)
        {
            yield return CalculteDirection(singlyLinkedList.Previous.Value,singlyLinkedList.Value);
            singlyLinkedList=singlyLinkedList.Previous;
        }
    }

    private static MoveDirection CalculteDirection(Point from, Point to)
    {
        var shifting = to - from;

        if (shifting.X == 1) return MoveDirection.Right;
        if (shifting.X == -1) return MoveDirection.Left;
        if (shifting.Y== -1) return MoveDirection.Up;
        if (shifting.Y == 1) return MoveDirection.Down;

        throw new NotImplementedException();
    }
}