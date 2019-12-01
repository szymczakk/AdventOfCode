using NUnit.Framework;

namespace Day1._1.Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual(2, Day11.Count(12));
            Assert.AreEqual(2, Day11.Count(14));
            Assert.AreEqual(654, Day11.Count(1969));
            Assert.AreEqual(33583, Day11.Count(100756));
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(2, Day12.Count(12));
            Assert.AreEqual(966, Day12.Count(1969));
            Assert.AreEqual(50346, Day12.Count(100756));
        }
    }
}