using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Code_2016
{
    public static class Day7
    {
        public static string Greeting = "--- Day 7: Internet Protocol Version 7 ---";
        public static void Run()
        {
            Puzzle.Greet(Greeting);

            var inputs = File.ReadAllLines(Environment.CurrentDirectory + "/inputs/day7.txt");

             var ls = inputs.Select(input =>
             {
                 return new
                 {
                     ABBA = Regex.IsMatch(input, @"^(?=.*(.)(?!\1)(.)\2\1)(?!.*\[[^]]*(.)(?!\3)(.)\4\3)"),
                     ABA = Regex.Split(input, @"\[[^\]]*\]").FirstOrDefault(x => { return FindAba(x).Any(match => IsBab(input, match)); }) != null
                 };
             }).ToList();
            

            Puzzle.Print($"ABBA: {ls.Count(x => x.ABBA)}", $"ABA/BAB: {ls.Count(x => x.ABA)}");
        }

        private static IEnumerable<Match> FindAba(string x)
        {
            return Regex.Matches(x, @"(?=((.)(?!\2).\2))").Cast<Match>();
        }

        private static bool IsBab(string input, Match match)
        {
            return Regex.Matches(input, @"\[(\w*)\]")
                .Cast<Match>().Any(m => m.Value.Contains(ToBab(match)));
        }

        private static string ToBab(Match match)
        {
            return string.Concat(match.Groups[1].Value[1], match.Groups[1].Value[0], match.Groups[1].Value[1]);
        }
    }
}
