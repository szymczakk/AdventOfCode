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
            Console.WriteLine("Hello World!");
        }
    }

    public class Worker
    {
        public string OrderInstruction(string[] input)
        {
            var parsedAndOrdered = ParseInput(input);

            var linkedList = new LinkedList<string>();

            linkedList.AddFirst(parsedAndOrdered.First().Key);

            foreach (var rowInDict in parsedAndOrdered)
            {
                foreach (var taskToComplete in rowInDict.Value)
                {
                    var beforeNode = linkedList.Find(rowInDict.Key);

                    linkedList.AddAfter(beforeNode, taskToComplete.Value);
                }
            }


            return "";
        }

        public Dictionary<string, SortedList<string, string>> ParseInput(string[] input)
        {
            var result = new Dictionary<string, SortedList<string, string>>();

            foreach (var row in input)
            {
                var arr = row.Split(' ');
                var before = arr[1];
                var after = arr[7];

                if (result.ContainsKey(before))
                {
                    result[before].Add(after, after);
                }
                else
                {
                    result.Add(before, new SortedList<string, string>(){{after, after}});
                }
            }

            return result;
        }
    }
}
