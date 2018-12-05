using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Day3._1.csharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            var result = Compute(input);
            Console.WriteLine("Hello World!");
        }

        public static int Compute(IEnumerable<string> inputLines)
        {
            var claims = ParseInput(inputLines);
            var arraySize = GetArraySize(claims);
            var workingArray = new int[arraySize.Item1,arraySize.Item2];

            foreach (var claim in claims)
            {
                workingArray = PutClaimOnFabric(claim, workingArray);
            }

            var result = 0;
            foreach (var el in workingArray)
            {
                if (el > 1) result++;
            }
            return result;
        }

        public static int[,] PutClaimOnFabric(Claim claim, int[,] fabric)
        {
            for (var x = claim.FromLeftEdge; x < claim.FromLeftEdge + claim.Wide; x++)
            {
                for (var y = claim.FromTop; y < claim.FromTop + claim.Tall; y++)
                {
                    fabric[x, y]++;
                }
            }
            return fabric;
        }

        public static (int, int) GetArraySize(IEnumerable<Claim> claims)
        {
            var x = claims.Max(c => c.FromLeftEdge + c.Wide);
            var y = claims.Max(c => c.FromTop + c.Tall);
            return (x, y);
        }

        public static IEnumerable<Claim> ParseInput(IEnumerable<string> inputLines)
        {
            var result = new List<Claim>();
            foreach (var inputLine in inputLines)
            {
                var claim = new Claim();
                var input = inputLine.Split(new[] { '@', ':', ',', 'x' }, StringSplitOptions.RemoveEmptyEntries);

                claim.FromLeftEdge = int.Parse(input[1].Trim());
                claim.FromTop = int.Parse(input[2].Trim());
                claim.Wide = int.Parse(input[3].Trim());
                claim.Tall = int.Parse(input[4].Trim());

                result.Add(claim);
            }

            return result;
        }
    }

    public class Claim
    {
        public int FromLeftEdge { get; set; }
        public int FromTop { get; set; }
        public int Wide { get; set; }
        public int Tall { get; set; }
    }
}
