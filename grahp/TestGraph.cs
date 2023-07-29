using System.ComponentModel.DataAnnotations;
using System.Drawing;

enum State
{
    Empty,
    Wall,
    Visited
};

public class TestGraph
{
    static void VisitInRecursionManner(State[,] map, int x, int y)
    {
        if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) return;
        if (map[x, y] != State.Empty) return;
        map[x, y] = State.Visited;
        Print(map);

        for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
                if (dx != 0 && dy != 0) continue;
                else VisitInRecursionManner(map, x + dx, y + dy);
    }

    static void VisitViaStack(State[,] map)
    {
        var stack = new Stack<Point>();
        stack.Push(new Point(0, 0));
        while (stack.Count!=0)
        {
            var point = stack.Pop();
            if (point.X < 0 || point.X >= map.GetLength(0) || point.Y < 0 || point.Y >= map.GetLength(1)) continue;
            if (map[point.X, point.Y] != State.Empty) continue;
            map[point.X, point.Y] = State.Visited;
            Print(map);

            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
                if (dx != 0 && dy != 0) continue;
                else stack.Push( new Point(point.X + dx, point.Y + dy));
        }
    }

    static void VisitViaQueue(State[,] map)
    {
        var queue = new Queue<Point>();
        queue.Enqueue(new Point(0, 0));
        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            if (point.X < 0 || point.X >= map.GetLength(0) || point.Y < 0 || point.Y >= map.GetLength(1)) continue;
            if (map[point.X, point.Y] != State.Empty) continue;
            map[point.X, point.Y] = State.Visited;
            Print(map);

            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
                if (dx != 0 && dy != 0) continue;
                else queue.Enqueue(new Point(point.X + dx, point.Y + dy));
        }
    }
    static void Main()
    {
        Console.CursorVisible = false;
        var generatedMap =GenerateLabyrinth(40,20);
        Print(generatedMap);
        VisitViaQueue(generatedMap);
        
    }

   

    static void Print(State[,] map)
    {

        Console.SetCursorPosition(0, 0);
        for (int x = 0; x < map.GetLength(0) + 2; x++)
            Console.Write("█");
        Console.WriteLine();
        for (int y = 0; y < map.GetLength(1); y++)
        {
            Console.Write("█");
            for (int x = 0; x < map.GetLength(0); x++)
                switch (map[x, y])
                {
                    case State.Wall: Console.Write("█"); break;
                    case State.Empty: Console.Write(" "); break;
                    case State.Visited: Console.Write("."); break;
                }
            Console.WriteLine("█");
        }
        for (int x = 0; x < map.GetLength(0) + 2; x++)
            Console.Write("█");
        Thread.Sleep(100);
    }
  
     static State[,] GenerateLabyrinth(int xSize,int ySize)
    {
        while (true)
        {
            
            //Generate
            var map = new State[xSize, ySize];
            var rnd = new Random();
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    if(x == 0 && y == 0) map[x,y]= State.Empty;
                    else
                        map[x, y] = rnd.Next(2) >0 ? State.Empty : State.Wall;

            //Validate
            if (IsValidLabyrinth(map)) 
                return ToDefault(map);
        }
    }

     private static bool IsValidLabyrinth(State[,] states)
     {
         bool check;
         check = false;
         Validate(states, 0, 0, ref check);
         if (check)
             return true;
         return false;
     }

     private static void Validate(State[,] map, int x, int y, ref bool check)
     {

         if (x == map.GetLength(0) - 1 && y == map.GetLength(1) - 1 && map[x, y] == State.Empty) check = true;
         if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) return;
         if (map[x, y] != State.Empty) return;
         map[x, y] = State.Visited;

         for (var dy = -1; dy <= 1; dy++)
         for (var dx = -1; dx <= 1; dx++)
             if (dx != 0 && dy != 0) continue;
             else Validate(map, x + dx, y + dy,ref check);
     }

     static State[,] ToDefault(State[,] map)
     {
         for (int x = 0; x < map.GetLength(0); x++)
         for (int y = 0; y < map.GetLength(1); y++)
             if (map[x, y] == State.Visited)
                 map[x, y] = State.Empty;
         return map;
     }
}
