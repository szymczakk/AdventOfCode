using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day7._2.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var w = new Worker();
            var result = w.GetOrderWorkingTime(lines, 5, 60);
            Console.WriteLine(result);
        }
    }

    public class Worker
    {
        public int GetOrderWorkingTime(string[] lines, int workers, int offset)
        {
            var parsedAndOrdered = ParseInput(lines, offset);

            var possibleNodes = new List<Node>();
            var finishedTasks = new List<Node>();
            var workInProgress = new List<Node>(workers) {};

            for (var i = 0; i < workers; i++)
            {
                workInProgress.Add(null);
            }

            possibleNodes.AddRange(parsedAndOrdered.Where(pao => !pao.Prev.Any()).OrderBy(pao => pao.Name).ToList());

            var time = -1;

            while (finishedTasks.Count != parsedAndOrdered.Count)
            {
                time++;

                FreeWorker(workInProgress, finishedTasks, time);

                while (workInProgress.Any(fw => fw == null) && possibleNodes.Any(pv => !pv.Prev.Any() || pv.Prev.All(prev => finishedTasks.Any(ft => ft.Name == prev))))
                {
                    var nextFreeWorkerIndex = workInProgress.FindIndex(fw => fw == null);
                    var next =
                        possibleNodes.First(
                            pv => !pv.Prev.Any() || pv.Prev.All(prev => finishedTasks.Any(ft => ft.Name == prev)));

                    next.FinishedTime = next.RequiredTime + time;
                    workInProgress[nextFreeWorkerIndex] = next;

                    possibleNodes.Remove(next);

                    foreach (var nextTask in next.Next)
                    {
                        possibleNodes.Add(parsedAndOrdered.Find(n => n.Name == nextTask));
                    }

                    possibleNodes =
                        possibleNodes.Distinct().OrderBy(pv => pv.Name).ToList();
                }
            }

            return time;
        }

        private void FreeWorker(List<Node> workInProgress, List<Node> finishedTasks ,int time)
        {
            for (var i = 0; i < workInProgress.Count; i++)
            {
                if (workInProgress[i] != null)
                {
                    if (workInProgress[i].FinishedTime == time)
                    {
                        finishedTasks.Add(workInProgress[i]);
                        workInProgress[i] = null;
                    }
                }
            }
        }

        public List<Node> ParseInput(string[] input, int offset)
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
                        },
                        RequiredTime = ((int)before.ToCharArray()[0]-64) + offset
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
                        Next = new List<string>(),
                        RequiredTime = ((int)after.ToCharArray()[0]-64) + offset
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
        public int RequiredTime { get; set; }
        public int FinishedTime { get; set; }
    }
}
