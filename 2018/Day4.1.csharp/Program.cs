using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var w = new Worker();
            var result = w.GetMostTimeAsleep(lines);
            Console.WriteLine(result);
        }
    }

    public class Worker
    {
        public int GetMostTimeAsleep(string[] input)
        {
            var orderedDictionary = ParseToOrderedDictionary(input);
            var infoAboutGuards = ParseInfoForEachGuard(orderedDictionary);
            var sleepHead = infoAboutGuards.Select(pair => pair.Value).OrderBy(guard => guard.SleepTime).First();
            return sleepHead.Id * sleepHead.SleepTime.Minutes;
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

            var guardStartDate = dict.Where(pair => pair.Value.Contains('#'));

            return result;
        }
    }

    public class Guard
    {
        public int Id { get; set; }
        public TimeSpan SleepTime{ get; set; }
    }
}
