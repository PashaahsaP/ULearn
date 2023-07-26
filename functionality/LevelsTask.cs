using System;
using System.Collections.Generic;
namespace func_rocket
{
    public class LevelsTask
    {
        static readonly Physics standardPhysics = new Physics();
        public static IEnumerable<Level> CreateLevels()
        {
            yield return CreateLevel((size, v) => Vector.Zero);
            yield return CreateLevel((size, v) => new Vector(0, 0.9),"Heavy");
            yield return CreateLevel((size, v) => new Vector(0, -300 / (size.Y - v.Y + 300)),new Vector(700,500),name:"Up");
            yield return CreateLevel((size, v) =>
                {
                    var toWhiteHole = new Vector(v.X - 600, v.Y - 200);
                    return toWhiteHole.Normalize() * 140 * toWhiteHole.Length
                           / (toWhiteHole.Length * toWhiteHole.Length + 1);
                }, "WhiteHole");
            yield return CreateLevel((size, v) =>
                {
                    var blackHolePosition = new Vector((600 + 200) / 2, (500 + 200) / 2);
                    var toBlackHole = blackHolePosition - v;
                    return new Vector(blackHolePosition.X - v.X, blackHolePosition.Y - v.Y).Normalize() *
                        300 * toBlackHole.Length / (toBlackHole.Length * toBlackHole.Length + 1);
                }, "BlackHole");
            yield return CreateLevel((size, v) =>
                {
                    var toWhiteHole = new Vector(v.X - 600, v.Y - 200);
                    var blackHolePosition = new Vector((600 + 200) / 2, (500 + 200) / 2);
                    var toBlackHole = blackHolePosition - v;
                    return (toWhiteHole.Normalize() * 140 * toWhiteHole.Length  / (toWhiteHole.Length * toWhiteHole.Length + 1) 
                            + new Vector(blackHolePosition.X - v.X, blackHolePosition.Y - v.Y).Normalize() 
                            * 300 * toBlackHole.Length / (toBlackHole.Length * toBlackHole.Length + 1)) / 2;
                }, "BlackAndWhite");
        }
          static Level CreateLevel(Gravity gravity,Vector target, string name = "Zero")
        {
                return new Level(name, new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),  
                    target, gravity,  standardPhysics);
        }
          static Level CreateLevel(Gravity gravity, string name = "Zero")
         {
             return new Level(name, new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                 new Vector(600, 200), gravity, standardPhysics);
         }
    }
}