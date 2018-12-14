using System;

namespace Day13._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");

            var w = new W();
            var result = w.GetFirstCollisionCordinates(input);

            Console.WriteLine(result);
        }
    }

    public class W
    {
        public (int x, int y) GetFirstCollisionCordinates(string[] input)
        {
            return (0, 0);
        }
    }
}
