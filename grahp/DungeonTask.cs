using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Avalonia.Remote.Protocol.Viewport;

namespace Dungeon;

public class DungeonTask
{
	public static MoveDirection[] FindShortestPath(Map map)
    {
        //Stopwatch st = Stopwatch.StartNew();

        var pathsToChests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
        IEnumerable<MoveDirection> shortestPath = new List<MoveDirection>();
        GetPath(ref shortestPath,map, pathsToChests);


        if (!shortestPath.Any()) return new MoveDirection[0];
        //st.Stop();
        //using (StreamWriter sw = new StreamWriter(@"C:\Users\Павел\Desktop\Текстовый документ.txt",true))
        //{
        //    sw.WriteLine(st.ElapsedMilliseconds);
        //}
        return shortestPath
            .ToArray();
    }

    private static void GetPath(ref IEnumerable<MoveDirection> shortestPath, Map map, IEnumerable<SinglyLinkedList<Point>> pathsToChests)
    {
        int minPath = int.MaxValue;
        if (!pathsToChests.Any())
            pathsToChests= new List<SinglyLinkedList<Point>>(){ new SinglyLinkedList<Point>(map.InitialPosition)};

        foreach (var path in pathsToChests)
        {
            if (path.Length>minPath) continue;
            var prevPath = GetDirections(path);
            var temp = BfsTask.FindPaths(map, path.Value, new Point[] { map.Exit });
            if (temp.Any())
            {
                var pathFromChest = temp.FirstOrDefault();
                if ((path.Length + pathFromChest.Length)>minPath) continue;
                var lastPath = GetDirections(pathFromChest);
                shortestPath = lastPath.Concat(prevPath).Reverse();
                minPath = Math.Min(path.Length + pathFromChest.Length, minPath);
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

        return 0;
    }




    
}