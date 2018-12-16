using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day15.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string[] _testCase1 = new[]
        {
            @"#######",
            @"#G..#E#",
            @"#E#E.E#",
            @"#G.##.#",
            @"#...#E#",
            @"#...E.#",
            @"#######"
        };

        private readonly string[] _testCase2 = new[]
        {
            @"#######",
            @"#E..EG#",
            @"#.#G.E#",
            @"#E.##E#",
            @"#G..#.#",
            @"#..E#.#",
            @"#######"
        };

        private readonly string[] _testCase3 = new[]
        {
            @"#######",
            @"#E.G#.#",
            @"#.#G..#",
            @"#G.#.G#",
            @"#G..#.#",
            @"#...E.#",
            @"#######"
        };

        private readonly string[] _testCase4 = new[]
        {
            @"#######",
            @"#.E...#",
            @"#.#..G#",
            @"#.###.#",
            @"#E#G#G#",
            @"#...#G#",
            @"#######"
        };

        private readonly string[] _testCase5 = new[]
        {
            @"#########",
            @"#G......#",
            @"#.E.#...#",
            @"#..##..G#",
            @"#...##..#",
            @"#...#...#",
            @"#.G...G.#",

            @"#.....G.#",
            @"#########"
        };

        [TestMethod]
        public void TestGetBattleOutcome()
        {
            var w = new W();

            Assert.AreEqual(36334, w.GetBattleOutcome(_testCase1));
            Assert.AreEqual(39514, w.GetBattleOutcome(_testCase2));
            Assert.AreEqual(27755, w.GetBattleOutcome(_testCase3));
            Assert.AreEqual(28944, w.GetBattleOutcome(_testCase4));
            Assert.AreEqual(18740, w.GetBattleOutcome(_testCase5));
        }
    }
}
