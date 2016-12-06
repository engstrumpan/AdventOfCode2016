using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2016
{
    public static class Day4
    {
        public static string Greeting = "--- Day 4: Security Through Obscurity ---";
        private static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static void Run()
        {
            Puzzle.Greet(Greeting);

            var inputs = File.ReadAllLines(Environment.CurrentDirectory + "/inputs/day4.txt");
            var sum = 0;

            var northpole = 0;

            foreach (var input in inputs)
            {
                var matches = Regex.Matches(input, "(?<name>[a-z-]+)-(?<id>\\d+)\\[(?<checksum>[a-z]+)\\]");
                var name = matches[0].Groups["name"].Value;
                var id = int.Parse(matches[0].Groups["id"].Value);
                var checksum = matches[0].Groups["checksum"].Value;


                var a = string.Join("",
                     name.Replace("-", string.Empty).ToCharArray()
                         .GroupBy(c => c)
                         .Select(x => new { x.Key, Count = x.Count() })
                         .OrderByDescending(x => x.Count)
                         .ThenBy(x => x.Key)
                         .Take(5)
                         .Select(x => x.Key)).Equals(checksum)
                     ? id
                     : 0;

                sum += a;

                if (a <= 0 || northpole != 0) continue;
                {
                    var decrypted = new string(name.Select(c => c.Equals('-') ? ' ' : RotateLetter(c, id)).ToArray());

                    if (decrypted.Contains("northpole"))
                    {
                        northpole = id;
                    }
                }
            }

            Puzzle.Print($"Real rooms: {sum}", "Sector ID:" + northpole);

        }

        private static char RotateLetter(char c, int id)
        {
            var i = Alphabet.IndexOf(c);
            for (var j = 0; j < id; j++)
            {
                i++;
                if (i == Alphabet.Length)
                {
                    i = 0;
                }
            }

            return Alphabet[i];
        }
    }

   
}
