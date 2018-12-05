using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Day2._2.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _testSet = new string[]
        {
            "abcde",
            "fghij",
            "klmno",
            "pqrst",
            "fguij",
            "axcye",
            "wvxyz"
        };

        [TestMethod]
        public void MainTest()
        {
            var result = Day2._2.csharp.Program.Calculate(_testSet);

            Assert.AreEqual("fgij", result);
        }

        [TestMethod]
        public void TestHelperFunction()
        {
            var testString1 = "abcdef";
            var testString2 = "abcqwe";
            var testString3 = "zxcasd";

            Assert.AreEqual(3, Program.GetStringDistance(testString1, testString2));
            Assert.AreEqual(5, Program.GetStringDistance(testString1, testString3));
            Assert.AreEqual(0, Program.GetStringDistance(testString3, testString3));
        }
    }
}
