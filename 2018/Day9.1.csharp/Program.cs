using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var w = new W();
            var result = w.GetWinningScore(465, 7149800);

            Console.WriteLine(result);
        }
    }

    public class W
    {
        public (long winningScore, string gameboard) GetWinningScore(int playerAmount, int marbleAmount)
        {
            var gameList = new List<int>(marbleAmount){0, 1};
            var scores = new long[playerAmount];

            var insertAt = 1;
            for (var i = 2; i <= marbleAmount; i++)
            {
                if (i % 23 == 0)
                {
                    scores[i % playerAmount] += i;

                    var additionalPointsIndex = insertAt - 7;
                    if (additionalPointsIndex < 0)
                    {
                        additionalPointsIndex = gameList.Count + additionalPointsIndex;
                    }

                    scores[i % playerAmount] += gameList[additionalPointsIndex];
                    gameList.RemoveAt(additionalPointsIndex);
                    insertAt = additionalPointsIndex;
                }
                else
                {
                    insertAt = (insertAt + 2) % (gameList.Count);

                    if (insertAt == 0)
                    {
                        insertAt = gameList.Count;
                    }

                    gameList.Insert(insertAt, i);
                }
            }

            return (scores.Max(), "" ); // string.Join(' ', gameList));
        }
    }
}
