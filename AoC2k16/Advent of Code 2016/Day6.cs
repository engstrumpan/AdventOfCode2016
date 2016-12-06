using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2016
{
    public static class Day6
    {
        public static string Greeting = "--- Day 6: Signals and Noise ---";
        public static void Run()
        {
            Puzzle.Greet(Greeting);

            var inputs = File.ReadAllLines(Environment.CurrentDirectory + "/inputs/day6.txt");
            var len = inputs[0].Length;

            var part1 = inputs
                .SelectMany(x => x)
                .Select((c, i) => new {Key = c, Index = i%len})
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .ThenBy(x => x.Key.Index)
                .Select(x => x).ToList();

            var part2 = part1
                .OrderBy(x => x.Count())
                .ThenBy(x => x.Key.Index);

            var p1 = string.Concat(part1.Take(len).Select(x => x.Key.Key));
            var p2 = string.Concat(part2.Take(len).Select(x => x.Key.Key));

            Puzzle.Print(p1,p2);

        }
    }
}
