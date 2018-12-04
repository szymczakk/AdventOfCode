using System;
using System.Collections.Generic;

namespace Day1._2.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");

            var res = Calculate(lines);
            Console.WriteLine(res);
        }

        static int Calculate(string[] lines)
        {
            var results = new HashSet<int>();
            var sum = 0;
            while (true)
            {
                foreach (var line in lines)
                {
                    var num = int.Parse(line);
                    sum += num;
                    if (!results.Contains(sum))
                    {
                        results.Add(sum);
                    }
                    else
                    {
                        return sum;
                    }
                }
            }
        }
    }
}
