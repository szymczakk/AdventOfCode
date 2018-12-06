using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day5._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _testCase1 = "aA";
        private readonly string _testCase2 = "abBA";
        private readonly string _testCase3 = "abAB";
        private readonly string _testCase4 = "aabAAB";
        private readonly string _testCase5 = "dabAcCaCBAcCcaDA";

        [TestMethod]
        public void TestPolymer1()
        {
            var w = new Worker();
            var result = w.ReactPolymerAndGetUnits(_testCase1);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestPolymer2()
        {
            var w = new Worker();
            var result = w.ReactPolymerAndGetUnits(_testCase2);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestPolymer3()
        {
            var w = new Worker();
            var result = w.ReactPolymerAndGetUnits(_testCase3);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void TestPolymer4()
        {
            var w = new Worker();
            var result = w.ReactPolymerAndGetUnits(_testCase4);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void TestPolymer5()
        {
            var w = new Worker();
            var result = w.ReactPolymerAndGetUnits(_testCase5);
            Assert.AreEqual(10, result);
        }
    }
}
