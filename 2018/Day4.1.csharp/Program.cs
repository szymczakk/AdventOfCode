using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace Day4._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var w = new Worker();
            var result1 = w.GetMostTimeAsleep(lines);
            var result2 = w.SecondMethod(lines);
            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }
    }

    public class Worker
    {
        public int GetMostTimeAsleep(string[] input)
        {
            var orderedDictionary = ParseToOrderedDictionary(input);
            var infoAboutGuards = ParseInfoForEachGuard(orderedDictionary);

            var sleepHead = infoAboutGuards.Select(pair => pair.Value).OrderByDescending(guard => guard.SleepTime).First();
            var mostOccuringMinute =
                sleepHead.SleepMinutes.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(k => k.Key).First();
            return sleepHead.Id * mostOccuringMinute;
        }

        public int SecondMethod(string[] input)
        {
            var orderedDictionary = ParseToOrderedDictionary(input);
            var infoAboutGuards = ParseInfoForEachGuard(orderedDictionary);

            var mostOccuringMinutes = infoAboutGuards.Select(pair => pair.Value).Select(g => new
            {
                Id = g.Id,
                MostOccuringMinute = g.SleepMinutes.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                    .Select(k => k.Key).FirstOrDefault()
            });

            var result = mostOccuringMinutes.OrderByDescending(e => e.MostOccuringMinute).First();

            return result.Id * result.MostOccuringMinute;
        }

        public Dictionary<DateTime, string> ParseToOrderedDictionary(string[] input)
        {
            return input.Select(row => row.Split(new[] {'[', ']'}, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(row => DateTime.Parse(row[0]), row => row[1].Trim())
                .OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public Dictionary<int, Guard> ParseInfoForEachGuard(Dictionary<DateTime, string> dict)
        {
            var result = new Dictionary<int, Guard>();

            Guard guard = null;
            DateTime? guardFallAsleepTime = null;
            DateTime? guardWakeUpTime = null;
            foreach (var row in dict)
            {
                if (row.Value.Contains('#'))
                {
                    guard = GetGuard(row, result);
                }
                if (guard != null)
                {
                    if (row.Value.Contains("falls"))
                    {
                        guardFallAsleepTime = row.Key;
                    }

                    if (row.Value.Contains("wakes"))
                    {
                        guardWakeUpTime = row.Key;
                    }

                    if (guardFallAsleepTime != null && guardWakeUpTime != null)
                    {
                        guard.SleepTime += (guardWakeUpTime.Value - guardFallAsleepTime.Value);
                        if (guardFallAsleepTime.Value.Minute > guardWakeUpTime.Value.Minute - 1)
                        {
                            guard.SleepMinutes.AddRange(Enumerable.Range(guardFallAsleepTime.Value.Minute, 60 - guardFallAsleepTime.Value.Minute));
                            guard.SleepMinutes.AddRange(Enumerable.Range(1, guardWakeUpTime.Value.Minute));
                        }
                        else
                        {
                            guard.SleepMinutes.AddRange(Enumerable.Range(guardFallAsleepTime.Value.Minute, guardWakeUpTime.Value.Minute - guardFallAsleepTime.Value.Minute));
                        }
                        guardWakeUpTime = null;
                        guardFallAsleepTime = null;
                    }
                }
            }

            return result;
        }

        private Guard GetGuard(KeyValuePair<DateTime, string> input, Dictionary<int, Guard> infos)
        {
            var guardId = GetGuardId(input.Value);
            if (infos.ContainsKey(guardId))
            {
                return infos[guardId];
            }

            var guard = new Guard()
            {
                Id = guardId
            };
            infos.Add(guardId, guard);
            return guard;
        }

        private int GetGuardId(string input)
        {
            return int.Parse(input.Split(' ').First(el => el.StartsWith("#")).Substring(1));
        }
    }

    public class Guard
    {
        public Guard()
        {
            SleepMinutes = new List<int>();
        }
        public int Id { get; set; }
        public TimeSpan SleepTime{ get; set; }
        public List<int> SleepMinutes { get; set; }
    }
}
