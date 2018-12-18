using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day16.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _before = "3, 2, 1, 1";
        private readonly string _after = "3, 2, 2, 1";
        private readonly string _action = "9 2 1 2";

        [TestMethod]
        public void TestCanBeCalculatedBy3OrMoreInstruction1()
        {
            var w = new W();

            Assert.IsTrue(w.CanBeCalculatedByXOrMoreInstruction(3, _before, _after, _action));
        }

        [TestMethod]
        public void TestCanBeCalculatedBy3OrMoreInstruction2()
        {
            var w = new W();

            Assert.IsTrue(w.CanBeCalculatedByXOrMoreInstruction(3, "3, 2, 1, 1", "3, 2, 11, 1", "9 2 10 2"));
        }

        [TestMethod]
        public void TestCanBeCalculatedBy3OrMoreInstruction3()
        {
            var w = new W();

            Assert.IsTrue(w.CanBeCalculatedByXOrMoreInstruction(3, "3, 2, 1, 1", "3, 2, 13, 1", "9 2 13 2"));
        }

        [TestMethod]
        public void TestCanBeCalculatedBy3OrMoreInstruction4()
        {
            var w = new W();

            Assert.IsTrue(w.CanBeCalculatedByXOrMoreInstruction(3, "3, 2, 1, 1", "3, 2, 1, 23", "9 0 20 3"));
        }
    }
}
    