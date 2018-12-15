using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14._1.csharp
{
    internal class Program
    {
        private static readonly int[] _initialState = { 3, 7 };

        private static readonly int[] _task2WrongAnswers = { 793061, 141045485, 141045484, 141045483, 20276283 };

        private static void Main(string[] args)
        {
            var w = new W();
            var result = w.GetScoresAfterRecipe(_initialState, 793061);

            Console.WriteLine(string.Join("", result));

            var result2 = w.GetLeftMostAmoutBeforeValue(_initialState, new[] { 7, 9, 3, 0, 6, 1 });
            if (_task2WrongAnswers.Contains(result2))
            {
                return;
            }
            Console.WriteLine(result2);
        }
    }

    public class W
    {
        public int[] GetScoresAfterRecipe(int[] initalState, int recipeNo)
        {
            var scoreBoard = new List<int>(initalState);

            var elf1Index = 0;
            var elf2Index = 1;

            while (scoreBoard.Count <= recipeNo + 10)
            {
                var newRecipe = scoreBoard[elf1Index] + scoreBoard[elf2Index];
                if (newRecipe > 9)
                {
                    scoreBoard.Add((int)(newRecipe / 10));
                    scoreBoard.Add(newRecipe % 10);
                }
                else
                {
                    scoreBoard.Add(newRecipe);
                }

                elf1Index = (elf1Index + (scoreBoard[elf1Index] + 1) % scoreBoard.Count) % scoreBoard.Count;
                elf2Index = (elf2Index + (scoreBoard[elf2Index] + 1) % scoreBoard.Count) % scoreBoard.Count;
            }

            return scoreBoard.Skip(recipeNo).Take(10).ToArray();
        }

        public int GetLeftMostAmoutBeforeValue(int[] initialState, int[] value)
        {
            var scoreBoard = new List<int>(initialState);
            var elf1Index = 0;
            var elf2Index = 1;

            bool found = false;
            var intToSubstitute = 1;

            while (!found)
            {
                var newRecipe = scoreBoard[elf1Index] + scoreBoard[elf2Index];
                if (newRecipe > 9)
                {
                    scoreBoard.Add((int)(newRecipe / 10));
                    scoreBoard.Add(newRecipe % 10);
                }
                else
                {
                    scoreBoard.Add(newRecipe);
                }

                elf1Index = (elf1Index + (scoreBoard[elf1Index] + 1) % scoreBoard.Count) % scoreBoard.Count;
                elf2Index = (elf2Index + (scoreBoard[elf2Index] + 1) % scoreBoard.Count) % scoreBoard.Count;

                if (scoreBoard.Count < value.Length)
                {
                    continue;
                }


                if (scoreBoard[scoreBoard.Count - 1] == value[value.Length - 1])
                {
                    found = true;
                    for (var i = 0; i < value.Count(); i++)
                    {
                        intToSubstitute = 0;
                        if (scoreBoard[scoreBoard.Count - i - 1] != value[value.Length - i - 1])
                        {
                            found = false;
                            break;
                        }
                    }
                }

                if (scoreBoard[scoreBoard.Count - 2] == value[value.Length - 1])
                {
                    found = true;
                    for (var i = 0; i < value.Count(); i++)
                    {
                        intToSubstitute = 1;
                        if (scoreBoard[scoreBoard.Count - i - 2] != value[value.Length - i - 1])
                        {
                            found = false;
                            break;
                        }
                    }
                }
            }

            return scoreBoard.Count - value.Length - intToSubstitute;
        }
    }
}
