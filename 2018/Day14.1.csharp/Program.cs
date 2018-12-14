using System;

namespace Day14._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var w = new W();
            var result = w.GetScoresAfterRecipe(new[] {3, 7}, 793061);

            Console.WriteLine(result);
        }
    }

    public class W
    {
        public int[] GetScoresAfterRecipe(int[] initalState, int recipeNo)
        {
            var result = new int[10];
            return result;
        }
    }
}
