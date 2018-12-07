using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;

namespace Day7._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var w = new Worker();
            var result = w.OrderInstruction(lines);
            Console.WriteLine(result);
        }
    }

    public class Worker
    {
        public string OrderInstruction(string[] input)
        {
            var parsedAndOrdered = ParseInput(input);
            
            var possibleValues = new List<Node>();
            var finishedTasks = new List<Node>();

            possibleValues.AddRange(parsedAndOrdered.Where(pao => !pao.Prev.Any()).OrderBy(pao => pao.Name).ToList());
            var sb = new StringBuilder();

            while (possibleValues.Any())
            {
                var next = possibleValues.First();
                possibleValues.Remove(next);
                finishedTasks.Add(next);
                sb.Append(next.Name);

                foreach (var nextTask in next.Next)
                {
                    possibleValues.Add(parsedAndOrdered.Find(n => n.Name == nextTask));
                }

                possibleValues =
                    possibleValues.Where(pv => !pv.Prev.Any() || pv.Prev.All(prev => finishedTasks.Any(ft => ft.Name == prev))).OrderBy(pv => pv.Name).ToList();
            }
            return sb.ToString();
        }

        public List<Node> ParseInput(string[] input)
        {
            var result = new List<Node>();

            foreach (var row in input)
            {
                var arr = row.Split(' ');
                var before = arr[1];
                var after = arr[7];

                if (result.Any(n => n.Name == before))
                {
                    var node = result.First(n => n.Name == before);
                    node.Next.Add(after);
                }
                else
                {
                    result.Add(new Node()
                    {
                        Name = before,
                        Prev = new List<string>(),
                        Next = new List<string>()
                        {
                            after
                        }
                    });
                }

                if (result.Any(n => n.Name == after))
                {
                    var node = result.First(n => n.Name == after);
                    node.Prev.Add(before);
                }
                else
                {
                    result.Add(new Node()
                    {
                        Name = after,
                        Prev = new List<string>()
                        {
                            before
                        },
                        Next = new List<string>()
                    });
                }
            }

            return result;
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public List<string> Next { get; set; }
        public List<string> Prev { get; set; }
    }
}
