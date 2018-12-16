using System;

namespace Day15.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");

            var w = new W();
            var result = w.GetBattleOutcome(input);
            Console.Write(result);
            Console.ReadLine();
        }
    }

    public class W
    {
        public int GetBattleOutcome(string[] input)
        {
            return 0;
        }
    }
}
