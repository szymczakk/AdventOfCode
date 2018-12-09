using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText("input.txt");
            var w = new W();
            var result = w.SumMeta(input);
            Console.WriteLine(result);
        }
    }

    public class W
    {
        public int SumMeta(string oneRealyLongStringWithData)
        {
            var input = oneRealyLongStringWithData.Split(' ').ToList();
            var parsedData = ParseNodes(input);
            return parsedData.Sum(node => node.Metas.Sum());
        }

        public IEnumerable<Node> ParseNodes(List<string> input)
        {
            var result = new List<Node>();
            return result;
        }
    }

    public class Node
    {
        public (int ChildCount, int MetaCount) Header { get; set; }
        public List<int> Metas { get; set; }
    }
}
