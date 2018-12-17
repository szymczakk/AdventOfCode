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
        public void TestCanBeCalculatedBy3OrMoreInstruction()
        {
            var w = new W();

            Assert.IsTrue(w.CanBeCalculatedByXOrMoreInstruction(3, _before, _after, _action));
        }
    }
}
    