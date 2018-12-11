using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day11._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCaluclatePower1()
        {
            var w = new W();
            var result = w.CalculatePower(8, 3, 5);

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void TestCaluclatePower2()
        {
            var w = new W();
            var result = w.CalculatePower(57, 122, 79);

            Assert.AreEqual(-5, result);
        }

        [TestMethod]
        public void TestCaluclatePower3()
        {
            var w = new W();
            var result = w.CalculatePower(39, 217, 196);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestCaluclatePower4()
        {
            var w = new W();
            var result = w.CalculatePower(71, 101, 153);

            Assert.AreEqual(4, result);
        }

        [TestMethod, Ignore]
        public void TestFindWindow()
        {
            var w = new W();
            var result = w.FindWindow(18, 300, 300, 3, 3);

            Assert.AreEqual((33, 45), result);
        }

        [TestMethod]
        public void TestFindBiggestPowerForAnyWindowSize1()
        {
            var w = new W();
            var result = w.FindBiggetsPowerForAnyWindow(18, 300, 300);

            Assert.AreEqual((90, 269 ,16), result);
        }

        [TestMethod]
        public void TestFindBiggestPowerForAnyWindowSize2()
        {
            var w = new W();
            var result = w.FindBiggetsPowerForAnyWindow(42, 300, 300);

            Assert.AreEqual((232, 251, 12), result);
        }
    }
}
