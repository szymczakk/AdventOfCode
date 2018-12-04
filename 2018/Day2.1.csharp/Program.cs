using System;
using System.Linq;

namespace Day2._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");

            int twice = 0, tripple = 0;
            foreach (var line in lines)
            {
                var dict = line.GroupBy(c => c).ToDictionary(c => c.Key, c=> c.ToList().Count);
                var doubleInLine = dict.Where(pair => pair.Value == 2);
                var trippleInLine = dict.Where(pair => pair.Value == 3);

                if (doubleInLine.Any())
                {
                    twice++;
                }

                if (trippleInLine.Any())
                {
                    tripple++;
                }
            }

            var result = twice * tripple;
            Console.WriteLine(result);
        }
    }
}
