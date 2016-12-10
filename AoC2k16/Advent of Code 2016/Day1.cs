using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2016
{
    public static class Day1
    {
        public static string Greeting = "--- Day 1: No Time for a Taxicab ---";

        public static void Run()
        {
            Puzzle.Greet(Greeting);

            var inputs = File.ReadAllLines(Environment.CurrentDirectory + "/inputs/day1.txt")[0];

            var coords = new Coords {Facing = Facing.North, Positions = ""};

            var p1 = Regex.Split(inputs,", ").Select(x => CalculateDistance(x,coords)).Last();

            Puzzle.Print($"Distance to Bunny HQ: {p1}", $"Real Distance to Bunny HQ: {coords.RealDistance}");
        }

        private static int CalculateDistance(string instruction, Coords pos)
        {

            var dir = instruction[0];
            var step = int.Parse(instruction.Substring(1));

            var direction = (int)pos.Facing;

            if (dir == 'R')
            {
                direction++;
            }
            else
            {
                direction--;
            }
            if (direction < 0)
            {
                direction += 4;
            }

            direction %= 4;

            if (direction >= 2)
            {
                step *= -1;
            }

            pos.Facing = (Facing) direction;

            var len = Math.Abs(step);
            while (len > 0)
            {

                switch (direction)
                {
                    case (int)Facing.North:
                        pos.Y += 1;
                        break;
                    case (int)Facing.East:
                        pos.X += 1;
                        break;
                    case (int)Facing.West:
                        pos.X -= 1;
                        break;
                    case (int)Facing.South:
                        pos.Y -= 1;
                        break;
                }

                var point = $"[{pos.X},{pos.Y}]";
                pos.Distance = Math.Abs(pos.X) + Math.Abs(pos.Y);

                if (pos.Positions.Contains(point) && pos.RealDistance == 0)
                {
                    pos.RealDistance = pos.Distance;
                }
                else
                { 
                    pos.Positions += point;
                }

                len -= 1;
            }


            return pos.Distance;
        }

        private class Coords
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Facing Facing { get; set; }
            public string Positions { get; set; }
            public int Distance { get; set; }
            public int RealDistance { get; set; }
        }

        private enum Facing
        {
            North = 0,East,South,West
        }
    }
}
