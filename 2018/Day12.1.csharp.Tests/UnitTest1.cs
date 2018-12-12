using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day12._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _testCase = new[]
        {
            "initial state: #..#.#..##......###...###",
            "",
            "...## => #",
            "..#.. => #",
            ".#... => #",
            ".#.#. => #",
            ".#.## => #",
            ".##.. => #",
            ".#### => #",
            "#.#.# => #",
            "#.### => #",
            "##.#. => #",
            "##.## => #",
            "###.. => #",
            "###.# => #",
            "####. => #"
        };
        [TestMethod]
        public void TestCalculateAmoutnOfPlantsAfterGeneration()
        {
            var w = new W();
            var result = w.CalculateAmoutnOfPlantsAfterGeneration(_testCase, 20);

            Assert.AreEqual(325, result);
        }
    }
}
