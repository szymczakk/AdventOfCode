using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day2._2.csharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");

            var result = Calculate(input);

            Console.WriteLine(result);
        }

        public static string Calculate(string[] inputLines)
        {
            (string, string)? searchDistanceResult = null;

            for (var i = 0; i < inputLines.Count(); i++)
            {
                searchDistanceResult = GetStringDistance(inputLines[i], inputLines.Skip(i + 1));
                if (searchDistanceResult != null)
                {
                    break;
                }
            }

            var resultSb = new StringBuilder();

            for(var i = 0; i < searchDistanceResult.Value.Item1.Length; i++)
            {
                if (searchDistanceResult.Value.Item1[i] == searchDistanceResult.Value.Item2[i])
                {
                    resultSb.Append(searchDistanceResult.Value.Item1[i]);
                }
            }

            return resultSb.ToString();
        }

        private static (string, string)? GetStringDistance(string s1, IEnumerable<string> inputs)
        {
            foreach (var input in inputs)
            {
                if (GetStringDistance(s1, input) == 1)
                {
                    return (s1, input);
                }
            }
            return null;
        }

        public static int GetStringDistance(string s1, string s2)
        {
            if (s1.Length != s2.Length)
            {
                throw new ArgumentException();
            }

            var result = 0;

            for (var i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i]) result++;
            }

            return result;
        }
    }
}
