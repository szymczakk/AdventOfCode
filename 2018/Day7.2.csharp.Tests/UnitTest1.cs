using System.Linq;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day7._2.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _testCase = new[]
{
            "Step C must be finished before step A can begin.",
            "Step C must be finished before step F can begin.",
            "Step A must be finished before step B can begin.",
            "Step A must be finished before step D can begin.",
            "Step B must be finished before step E can begin.",
            "Step D must be finished before step E can begin.",
            "Step F must be finished before step E can begin."
        };

        [TestMethod]
        public void TestGetOrderWithTime()
        {
            var w = new Worker();

            Assert.AreEqual(15, w.GetOrderWorkingTime(_testCase, 2, 0));
        }

        [TestMethod]
        public void TestParse()
        {
            var w = new Worker();
            var result = w.ParseInput(_testCase, 60);

            Assert.AreEqual(62, result.First(r => r.Name == "B").RequiredTime);
            Assert.AreEqual(61, result.First(r => r.Name == "A").RequiredTime);
            Assert.AreEqual(63, result.First(r => r.Name == "C").RequiredTime);
            Assert.AreEqual(66, result.First(r => r.Name == "F").RequiredTime);

        }
    }
}
