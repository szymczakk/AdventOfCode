﻿using System;

namespace Day11._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var w = new W();
            var result = w.FindWindow(9810, 300, 300, 3, 3);
            Console.WriteLine(result);
        }
    }

    public class W
    {
        public (int, int) FindWindow(int serialNo, int arrayXsize, int arrayYsize, int windowXsize, int windowYsize)
        {
            var xResult = 0;
            var yResult = 0;
            var maxBlockPower = int.MinValue;
            var array = new int[arrayXsize, arrayYsize];

            for (var x = 0; x < arrayXsize - windowXsize; x++)
            {
                for (int y = 0; y < arrayYsize - windowYsize; y++)
                {
                    var blockPower = 0;
                    for (int smallX = 0; smallX < windowXsize; smallX++)
                    {
                        for (int smallY = 0; smallY < windowYsize; smallY++)
                        {
                            var xIndex = x + smallX;
                            var yIndex = y + smallY;
                            blockPower += CalculatePower(serialNo, xIndex, yIndex);
                        }
                    }

                    if (blockPower > maxBlockPower)
                    {
                        xResult = x;
                        yResult = y;
                        maxBlockPower = blockPower;
                    }
                    array[x, y] = blockPower;
                }
            }

            return (xResult, yResult);
        }

        public int CalculatePower(int serialNo, int x, int y)
        {
            var rackId = x + 10;
            var powerLevel = rackId * y;
            powerLevel += serialNo;
            powerLevel *= rackId;
            powerLevel = int.Parse(powerLevel.ToString()[powerLevel.ToString().Length - 3].ToString());
            powerLevel -= 5;
            return powerLevel;
        }
    }
}
