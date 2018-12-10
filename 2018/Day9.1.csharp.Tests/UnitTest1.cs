using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day9._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetWinningScore()
        {
            var w = new W();
            var result = w.GetWinningScore(9, 25);
            Assert.AreEqual(32, result.Item1);
            Assert.AreEqual("0 16 8 17 4 18 19 2 24 20 25 10 21 5 22 11 1 12 6 13 3 14 7 15", result.Item2);
        }

        [TestMethod]
        public void TestGetWinningScore2()
        {
            var w = new W();

            Assert.AreEqual(8317, w.GetWinningScore(10, 1618).Item1);
        }

        [TestMethod]
        public void TestGetWinningScore3()
        {
            var w = new W();

            Assert.AreEqual(146373, w.GetWinningScore(13, 7999).Item1);
        }

        [TestMethod]
        public void TestGetWinningScore4()
        {
            var w = new W();

            Assert.AreEqual(2764, w.GetWinningScore(17, 1104).Item1);
        }

        [TestMethod]
        public void TestGetWinningScore5()
        {
            var w = new W();

            Assert.AreEqual(54718, w.GetWinningScore(21, 6111).Item1);
        }

        [TestMethod]
        public void TestGetWinningScore6()
        {
            var w = new W();

            Assert.AreEqual(37305, w.GetWinningScore(30, 5807).Item1);
        }
    }

}
