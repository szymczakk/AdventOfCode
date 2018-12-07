using System.Linq;
using System.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day7._1.csharp.Tests
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
        public void TestParseOrderMethod()
        {
            var w = new Worker();
            var result = w.OrderInstruction(_testCase);
            Assert.AreEqual("CABDFE", result);
        }

        [TestMethod]
        public void TestParseInput()
        {
            var w = new Worker();
            var result = w.ParseInput(_testCase);
            Assert.AreEqual(6, result.Count);
        }
    }
}
