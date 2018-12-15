using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day14._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly int[] _initialState = { 3, 7 };

        [TestMethod]
        public void TestGetScoresAfterRecipe()
        {
            var w = new W();
            CollectionAssert.AreEqual(new int[] { 5, 1, 5, 8, 9, 1, 6, 7, 7, 9 }, w.GetScoresAfterRecipe(_initialState, 9));
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 4, 5, 1, 5, 8, 9, 1 }, w.GetScoresAfterRecipe(_initialState, 5));
            CollectionAssert.AreEqual(new int[] { 9, 2, 5, 1, 0, 7, 1, 0, 8, 5 }, w.GetScoresAfterRecipe(_initialState, 18));
            CollectionAssert.AreEqual(new int[] { 5, 9, 4, 1, 4, 2, 9, 8, 8, 2 }, w.GetScoresAfterRecipe(_initialState, 2018));
        }

        [TestMethod]
        public void TestGetLeftMostAmoutBeforeValue()
        {
            var w = new W();
            Assert.AreEqual(9, w.GetLeftMostAmoutBeforeValue(_initialState, new[] { 5, 1, 5, 8, 9 }));
            Assert.AreEqual(5, w.GetLeftMostAmoutBeforeValue(_initialState, new[] { 0, 1, 2, 4, 5 }));
            Assert.AreEqual(18, w.GetLeftMostAmoutBeforeValue(_initialState, new[] { 9, 2, 5, 1, 0 }));
            Assert.AreEqual(2018, w.GetLeftMostAmoutBeforeValue(_initialState, new[] { 5, 9, 4, 1, 4 }));
        }
    }
}