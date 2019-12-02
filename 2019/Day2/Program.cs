using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split(",").Select(int.Parse).ToArray();

            var result = Day2.Compute(input);
            Console.WriteLine($"Result1 {result[0]}");
            Console.ReadLine();

            var input2 = File.ReadAllText("input2.txt").Split(",").Select(int.Parse).ToArray();
            var result2 = Day2.WhatIsNounAndVerb(19690720, input2);
            Console.WriteLine($"Result2 {result2}");
        }
    }

    public class Day2
    {
        public static int WhatIsNounAndVerb(int numberToLookFor, int[] input)
        {
            int[] inputCopy = new int[input.Length];
            input.CopyTo(inputCopy,0);

            for (int i = 0; i <= 99; i++)
            {
                if (i >= inputCopy.Length)
                {
                    break;
                }
                for (int j = 0; j <= 99 ; j++)
                {
                    input.CopyTo(inputCopy, 0);
                    if (j >= inputCopy.Length)
                    {
                        break;
                    }
                    inputCopy[1] = i;
                    inputCopy[2] = j;
                    var tempResult = Compute(inputCopy);
                    var noun = tempResult[1];
                    var verb = tempResult[2];
                    var countedResult = 100 * noun + verb;
                    if (numberToLookFor == tempResult[0])
                    {
                        return countedResult;
                    }
                    if(countedResult > numberToLookFor)
                    {
                        break;
                    }
                }
            }
            throw new Exception();
        }

        public static int[] Compute(int[] input)
        {
            int[] result = new int[input.Length];
            input.CopyTo(result, 0);
            for (int i = 0; i < result.Length; i += 4)
            {
                var action = result[i];
                if (action == 99)
                {
                    return result;
                }

                var positionOne = result[i + 1];
                var valueOne = result[positionOne];
                var positionTwo = result[i + 2];
                var valueTwo = result[positionTwo];

                var resultPosition = result[i + 3];
                if (action == 1)
                {
                    result[resultPosition] = valueOne + valueTwo;
                    continue;
                }

                if (action == 2)
                {
                    result[resultPosition] = valueOne * valueTwo;
                    continue;
                }

                Console.WriteLine($"Error opcode on index {i}");
                break;
            }

            return result;
        }
    }
}
