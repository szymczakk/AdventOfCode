using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day4._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _testCase = new[]
        {
            "[1518-11-01 00:00] Guard #10 begins shift",
            "[1518-11-01 00:05] falls asleep",
            "[1518-11-01 00:25] wakes up",
            "[1518-11-01 00:30] falls asleep",
            "[1518-11-01 00:55] wakes up",
            "[1518-11-01 23:58] Guard #99 begins shift",
            "[1518-11-02 00:40] falls asleep",
            "[1518-11-02 00:50] wakes up",
            "[1518-11-03 00:05] Guard #10 begins shift",
            "[1518-11-03 00:24] falls asleep",
            "[1518-11-03 00:29] wakes up",
            "[1518-11-04 00:02] Guard #99 begins shift",
            "[1518-11-04 00:36] falls asleep",
            "[1518-11-04 00:46] wakes up",
            "[1518-11-05 00:03] Guard #99 begins shift",
            "[1518-11-05 00:45] falls asleep",
            "[1518-11-05 00:55] wakes up",
        };

        [TestMethod]
        public void TestGetMostTimeAsleep()
        {
            var w =new Worker();
            Assert.AreEqual(240, w.GetMostTimeAsleep(_testCase));
        }

        [TestMethod]
        public void TestParseToDictionary()
        {
            var w = new Worker();
            var result = w.ParseToOrderedDictionary(_testCase);
            Assert.IsInstanceOfType(result, typeof(Dictionary<DateTime, string>));
            Assert.AreEqual(DateTime.Parse("1518-11-01 00:00"), result.Keys.First());
            Assert.AreEqual("Guard #10 begins shift", result.First().Value);

        }

        [TestMethod]
        public void TestParseInfoForEachGuard()
        {
            var w = new Worker();
            var dict = w.ParseToOrderedDictionary(_testCase);
            var result = w.ParseInfoForEachGuard(dict);
            Assert.AreEqual(50, result.First(pair => pair.Key == 10).Value.SleepTime.Minutes);
        }
    }
}
