using System;
using System.Collections.Generic;
using System.Text;

namespace Day5._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllText("input.txt");
            var w = new Worker();
            var result = w.ReactPolymerAndGetUnits(lines);
            Console.WriteLine(result);
        }
    }

    public class Worker
    {
        public int ReactPolymerAndGetUnits(string input)
        {
            var workingList = new List<char>(input);

            for (var i = 0; i < input.Length-1; i++)
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


            return workingList.Count;
        }

        private bool SameLetterDifferentSize(char a, char b)
        {
            var ia = (int) a;
            var ib = (int) b;

            return Math.Abs(ia - ib) == 32;
        }
    }
}
