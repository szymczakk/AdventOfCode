using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day3._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IEnumerable<string> _testCase = new List<string>()
        {
            "#1 @ 1,3: 4x4",
            "#2 @ 3,1: 4x4",
            "#3 @ 5,5: 2x2"
        };

        [TestMethod]
        public void Main()
        {
            var result = Program.Compute(_testCase);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void TestGetArraySize()
        {
            var result = Program.GetArraySize(Program.ParseInput(_testCase));

            Assert.AreEqual(7, result.Item1);
            Assert.AreEqual(7, result.Item2);
        }
    }
}
