using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5._2.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllText("input.txt");
            var w = new Worker();
            var result = w.OptimizePolymer(lines);
            Console.WriteLine(result);
        }
    }

    public class Worker
    {
        public int OptimizePolymer(string input)
        {
            var listOfAllPolymers = input.Distinct();

            var polymerOptimizedDict = new Dictionary<char, int>();
            foreach (var polymer in listOfAllPolymers)
            {
                if (polymerOptimizedDict.ContainsKey(Char.ToLower(polymer)))
                {
                    continue;
                }

                var polymerAfterRemovingPolymer =
                    ReactPolymer(input.Replace(polymer.ToString(), "", StringComparison.InvariantCultureIgnoreCase));

                polymerOptimizedDict.Add(Char.ToLower(polymer), polymerAfterRemovingPolymer.Count());
            }

            return polymerOptimizedDict.OrderBy(pair => pair.Value).First().Value;
        }

        public IEnumerable<char> ReactPolymer(string input)
        {
            var workingList = new List<char>(input);

            for (var i = 0; i < input.Length - 1; i++)
            {
                try
                {
                    if (SameLetterDifferentSize(workingList[i], workingList[i + 1]))
                    {
                        workingList.RemoveAt(i);
                        workingList.RemoveAt(i);
                        i--;
                        i--;
                        if (i < -1)
                        {
                            i = -1;
                        }
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    break;
                }
            }

            return workingList;
        }

        private bool SameLetterDifferentSize(char a, char b)
        {
            var ia = (int)a;
            var ib = (int)b;

            return Math.Abs(ia - ib) == 32;
        }
    }
}
