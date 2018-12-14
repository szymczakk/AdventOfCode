using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day14._1.csharp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly int[] _initialState = {3, 7};

        [TestMethod]
        public void TestGetScoresAfterRecipe()
        {
            var w = new W();
            var result = w.GetScoresAfterRecipe(_initialState, 9);
            
            Assert.AreEqual(new[]{ 5, 9, 4,1 ,4 , 2, 9, 8, 8, 2 }, result);
        }
    }
}
