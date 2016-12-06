using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2016
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var player = new System.Media.SoundPlayer(Environment.CurrentDirectory + "/inputs/SleighRide.wav");

            player.PlayLooping();

            AoC();

            player.Stop();
        }




        private static void AoC()
        {
            var tree = @"
                            |                         _...._
                         \  _  /                    .::o:::::.
                          (\o/)                    .:::'''':o:.
                      ---  / \  ---                :o:_    _:::
                           >*<                     `:}_>()<_{:'
                          >0<@<                 @    `'//\\'`    @ 
                         >>>@<<*              @ #     //  \\     # @
                        >@>*<0<<<           __#_#____/'____'\____#_#__
                       >*>>@<<<@<<         [__________________________]
                      >@>>0<<<*<<@<         |=_- .-/\ /\ /\ /\--. =_-|
                     >*>>0<<@<<<@<<<        |-_= | \ \\ \\ \\ \ |-_=-|
                    >@>>*<<@<>*<<0<*<       |_=-=| / // // // / |_=-_|
      \*/          >0>>*<<@<>0><<*<@<<      |=_- |`-'`-'`-'`-'  |=_=-|
  ___\\U//___     >*>>@><0<<*>>@><*<0<<     | =_-| o          o |_==_| 
  |\\ | | \\|    >@>>0<*<<0>>@<<0<<<*<@<    |=_- | !     (    ! |=-_=|
  | \\| | _(UU)_ >((*))_>0><*<0><@<<<0<*<  _|-,-=| !    ).    ! |-_-=|_
  |\ \| || / //||.*.*.*.|>>@<<*<<@>><0<<@</=-((=_| ! __(:')__ ! |=_==_-\
  |\\_|_|&&_// ||*.*.*.*|_\\db//__     (\_/)-=))-|/^\=^=^^=^=/^\| _=-_-_\
  """"|'.'.'.|~~|.*.*.*|     ____|_   =('.')=//   ,------------.      
  jgs |'.'.'.|   ^^^^^^|____|>>>>>>|  ( ~~~ )/   (((((((())))))))   
      ~~~~~~~~         '""""`------'  `w---w`     `------------'
            ";

            var menuItems = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsClass && x.IsAbstract && x.IsSealed && Regex.IsMatch(x.Name, "^Day\\d+$"))
                .Select(x => x)
                .OrderBy(x => x.Name)
                .ToArray();

            ConsoleKeyInfo input;

            do
            {
                Console.WriteLine(tree);
                PrintMenu(menuItems);

                input = Console.ReadKey();

                int command;
                if (int.TryParse(input.KeyChar.ToString(), out command) && (command > 0 && command <= menuItems.Length + 1))
                {
                    Console.Clear();
                    menuItems[command - 1].GetMethod("Run").Invoke(null, null);
                }

                if (input.Key == ConsoleKey.Escape) continue;

                Console.WriteLine(Environment.NewLine + "Continue? [y/n]");
                input = Console.ReadKey();
                if (input.Key == ConsoleKey.N) break;

            } while (input.Key != ConsoleKey.Escape);
        }

        private static void PrintMenu(IReadOnlyList<Type> menuItems)
        {
            for (var i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {menuItems[i].GetField("Greeting", BindingFlags.Public | BindingFlags.Static)?.GetValue(null)}");
            }
            Console.WriteLine("ESC - exit");
        }
    }
}
