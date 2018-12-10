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
            var result = w.GetWinningScore(465, 71498);

            Console.WriteLine(result);
        }
    }

    public class W
    {
        public (int winningScore, string gameboard) GetWinningScore(int playerAmout, int marbleAmount)
        {
            var gameList = new List<int>(){0, 1};
            var scores = new int[playerAmout];

            var insertAt = 1;
            for (var i = 2; i <= marbleAmount; i++)
            {
                if (i % 23 == 0)
                {
                    scores[i % playerAmout] += i;

                    //var additionalPointsIndex = (insertAt + (gameList.Count - 7) )%gameList.Count;
                    var additionalPointsIndex = insertAt - 7;
                    if (additionalPointsIndex < 0)
                    {
                        additionalPointsIndex = gameList.Count + additionalPointsIndex;
                    }

                    scores[i % playerAmout] += gameList[additionalPointsIndex];
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

            return (scores.Max(), string.Join(' ', gameList));
        }
    }
}
