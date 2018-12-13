using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
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
            var parsed = w.ParseInput(_testCase);

            var result = w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 20);

            Assert.AreEqual(145, w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 0));
            Assert.AreEqual(91, w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 1));

            Assert.AreEqual(325, result);
        }

        [TestMethod]
        public void TestCalculateAmoutnOfPlantsAfterGeneration2()
        {
            var w = new W();
            var parsed = w.ParseInput(_testCase);

            var result = w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 20);

            Assert.AreEqual(145, w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 0));
            Assert.AreEqual(91, w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 1));

            Assert.AreEqual(325, result);
        }

        [TestMethod]
        public void TestParseInputInitialState()
        {
            var w = new W();
            
            var parsed = w.ParseInput(_testCase);

            Assert.AreEqual(25, parsed.Item1.Count);

            Assert.IsTrue(parsed.initialState.Single(p => p.Index == 0).HasPlant);
            Assert.IsFalse(parsed.initialState.Single(p => p.Index == 1).HasPlant);
            Assert.IsFalse(parsed.initialState.Single(p => p.Index == 2).HasPlant);
            Assert.IsTrue(parsed.initialState.Single(p => p.Index == 3).HasPlant);
            Assert.IsFalse(parsed.initialState.Single(p => p.Index == 4).HasPlant);
            Assert.IsTrue(parsed.initialState.Single(p => p.Index == 24).HasPlant);
        }

        [TestMethod]
        public void TestParseInputRules()
        {
            var w = new W();

            var parsed = w.ParseInput(_testCase);

            Assert.AreEqual(14, parsed.Item2.Length);

            Assert.AreEqual("00011", string.Join("", parsed.Item2[0].Sequence));
            Assert.AreEqual(1, parsed.Item2[0].Result);
            Assert.AreEqual("11110", string.Join("", parsed.Item2[13].Sequence));
            Assert.AreEqual(1, parsed.Item2[13].Result);
        }

        [TestMethod]
        public void TestGetNextGeneration()
        {
            var w = new W();
            var parsed = w.ParseInput(_testCase);
            var result = w.GetNextGeneration(parsed.Item1, parsed.Item2);
            
            Assert.AreEqual(1, result[25]);
            Assert.AreEqual(1, result[29]);
            Assert.AreEqual(1, result[34]);
            Assert.AreEqual(1, result[40]);
            Assert.AreEqual(1, result[43]);

            Assert.IsTrue(string.Join("", result).Contains("0001000100001000001001001001"));
        }
    }
}
