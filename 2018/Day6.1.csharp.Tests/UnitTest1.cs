using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day6._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _testCase = new[]
        {
            "1, 1",
            "1, 6",
            "8, 3",
            "3, 4",
            "5, 5",
            "8, 9"
        };

        [TestMethod]
        public void TestGetSizeOfTheLargestNotInfiniteArea()
        {
            var w = new Worker();
            var result = w.GetSizeOfTheLargestNotInfiniteArea(_testCase);
            Assert.AreEqual(17, result);
        }

        [TestMethod]
        public void TestParseLinesToPoints()
        {
            var w = new Worker();
            var result = w.ParseLinesToPoints(_testCase);
            Assert.AreEqual(1, result.First().X);
            Assert.AreEqual(1, result.First().Y);
            Assert.AreEqual(8, result.Last().X);
            Assert.AreEqual(9, result.Last().Y);
        }

        [TestMethod]
        public void TestGetMinimumAreaSize()
        {
            var w = new Worker();
            var points = w.ParseLinesToPoints(_testCase);
            var result = w.GetAreaSize(points);
            Assert.AreEqual(8, result.x);
            Assert.AreEqual(9, result.y);
        }
    }
}
