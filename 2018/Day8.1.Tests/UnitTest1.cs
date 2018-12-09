using Day8._1.csharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day8._1.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _testCase = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
        [TestMethod]
        public void TestCountMeta()
        {
            var w = new W();
            var result = w.SumMeta(_testCase);

            Assert.AreEqual(138, result);
        }
    }
}
