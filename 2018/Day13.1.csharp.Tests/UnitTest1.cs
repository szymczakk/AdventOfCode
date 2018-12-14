using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day13._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _testCase = {
            @"/->-\        ",
            @"|   |  /----\",
            @"| /-+--+-\  |",
            @"| | |  | v  |",
            @"\-+-/  \-+--/",
              @"\------/   "
        };

        [TestMethod]
        public void TestMethod1()
        {
            var w = new W();
            var result = w.GetFirstCollisionCordinates(_testCase);

            Assert.AreEqual((7, 3), result);
        }
    }
}
