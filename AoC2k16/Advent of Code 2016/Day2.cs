using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2016
{
    public static class Day2
    {
        public static string Greeting = "--- Day 2: Bathroom Security ---";

        public static void Run()
        {
            Puzzle.Greet(Greeting);

            var inputs = File.ReadAllLines(Environment.CurrentDirectory + "/inputs/day2.txt").ToList();

            var numpad = new Numpad() { X = 1, Y = 1 };
            var numpad2 = new AdvancedNumpad { X = 0, Y = 2 };

            var combination1 = "";
            var combination2 = "";

            inputs.ForEach(x =>
            {
                x.ToCharArray().ToList().ForEach(c => Move(numpad, numpad2, c));

                combination1 += numpad.Digit();
                combination2 += numpad2.Digit();
            });

            Puzzle.Print($"The combination is: {combination1}", $"The combination is: {combination2}");
        }

        private static void Move(Numpad numpad, AdvancedNumpad numpad2, char move)
        {
            switch (move)
            {
                case 'U':
                    numpad.Up();
                    numpad2.Up();
                    break;
                case 'D':
                    numpad.Down();
                    numpad2.Down();
                    break;
                case 'L':
                    numpad.Left();
                    numpad2.Left();
                    break;
                case 'R':
                    numpad.Right();
                    numpad2.Right();
                    break;
            }
        }
    }

    public class Numpad
    {
        public int X { get; set; }
        public int Y { get; set; }


        private readonly int[,] _pad = {
            {7, 8, 9},
            {4, 5, 6},
            {1, 2, 3}
        };


        public void Up()
        {
            if (X < 2) X++;
        }
        public void Down()
        {
            if (X > 0) X--;
        }
        public void Left()
        {
            if (Y > 0) Y--;
        }
        public void Right()
        {
            if (Y < 2) Y++;
        }

        public int Digit()
        {
            return _pad[X, Y];
        }
    }
    public class AdvancedNumpad
    {
        public int X { get; set; }
        public int Y { get; set; }


        private readonly string[,] _pad = {
            {null, null, "1", null, null},
            {null, "2", "3", "4", null},
            {"5", "6", "7", "8", "9"},
            {null, "A", "B", "c", null},
            {null, null, "D", null, null},
        };


        public void Up()
        {
            var row = Y - 1;
            if (row > 0 && row < _pad.GetLength(0) && !string.IsNullOrEmpty(_pad[row, X])) Y--;
        }
        public void Down()
        {
            var row = Y + 1;
            if (row > 0 && row < _pad.GetLength(0) && !string.IsNullOrEmpty(_pad[row, X])) Y++;
        }
        public void Left()
        {
            var col = X - 1;
            if (col > 0 && col < _pad.GetLength(1) && !string.IsNullOrEmpty(_pad[Y, col])) X--;
        }
        public void Right()
        {
            var col = X + 1;
            if (col > 0 && col < _pad.GetLength(1) && !string.IsNullOrEmpty(_pad[Y, col])) X++;
        }

        public string Digit()
        {
            return _pad[Y, X];
        }
    }
}
