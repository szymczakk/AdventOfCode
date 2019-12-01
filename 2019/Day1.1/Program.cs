using System;
using System.IO;
using System.Linq;

namespace Day1._1
{
    class Program
    {
        private static decimal[] wrongAnswers2 = new[] {85060109M, 1642797M};
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("testData.txt");

            decimal result1 = 0;
            decimal result2 = 0;

            foreach (var s in input)
            {
                var fuelForModule = Day11.Count(int.Parse(s));
                result1 += fuelForModule;
                var fuelForFuel = Day12.Count(fuelForModule);
                result2 += fuelForModule;
                result2 += fuelForFuel;
            }

            Console.WriteLine($"Result1: {result1}");

            if (wrongAnswers2.Any(wa => wa == result2))
            {
                Console.WriteLine($"WRONG RESULT2 {result2}");
            }
            else
            {
                Console.WriteLine($"Result2: {result2}");
            }
            Console.ReadLine();
        }
    }

    public class Day11
    {
        public static decimal Count(decimal mass)
        {
            return Math.Floor((mass / 3)) - 2;
        }
    }

    public class Day12
    {
        public static decimal Count(decimal mass)
        {
            var massLeft = mass;
            var result = 0M;

            do
            {
                massLeft = Day11.Count(massLeft);
                if (massLeft < 0)
                {
                    break;
                }
                result += massLeft;
            } while (massLeft > 0);

            return result;
        }
    }
}
