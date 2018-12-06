using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day5._2.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _testCase1 = "dabAcCaCBAcCcaDA";

        [TestMethod]
        public void TestOptimizePolymer()
        {
            var w = new Worker();
            var result = w.OptimizePolymer(_testCase1);
            Assert.AreEqual(4, result);
        }
    }
}
