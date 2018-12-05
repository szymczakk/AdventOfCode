using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3._2.csharp
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
            var workingArray = new int[arraySize.Item1, arraySize.Item2];

            foreach (var claim in claims)
            {
                workingArray = PutClaimOnFabric(claim, workingArray, claims);
            }

            var result = claims.First(c => !c.Overlap).Id;
            return result;
        }

        public static int[,] PutClaimOnFabric(Claim claim, int[,] fabric, IEnumerable<Claim> claims)
        {
            for (var x = claim.FromLeftEdge; x < claim.FromLeftEdge + claim.Wide; x++)
            {
                for (var y = claim.FromTop; y < claim.FromTop + claim.Tall; y++)
                {
                    var currentFabricNumber = fabric[x, y];
                    if (currentFabricNumber != 0)
                    {
                        claim.Overlap = true;
                        claims.First(c => c.Id == currentFabricNumber).Overlap = true;
                    }
                    fabric[x, y] = claim.Id;
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

                claim.Id = int.Parse(input[0].Split('#')[1]);
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
        public int Id { get; set; }
        public int FromLeftEdge { get; set; }
        public int FromTop { get; set; }
        public int Wide { get; set; }
        public int Tall { get; set; }
        public bool Overlap { get; set; }
    }
}
