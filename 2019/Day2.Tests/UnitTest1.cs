using NUnit.Framework;

namespace Day2.Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            CollectionAssert.AreEqual(new []{ 2, 0, 0, 0, 99 }, Day2.Compute(new []{ 1, 0, 0, 0, 99 }));
            CollectionAssert.AreEqual(new []{ 2, 3, 0, 6, 99 }, Day2.Compute(new []{ 2, 3, 0, 3, 99 }));
            CollectionAssert.AreEqual(new []{ 2, 4, 4, 5, 99, 9801 }, Day2.Compute(new []{ 2, 4, 4, 5, 99, 0 }));
            CollectionAssert.AreEqual(new []{ 30, 1, 1, 4, 2, 5, 6, 0, 99 }, Day2.Compute(new []{ 1, 1, 1, 4, 99, 5, 6, 0, 99 }));
        }
    }
}