using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Day12._1.csharp
{
    class Program
    {
        private static readonly int[] falseResults1 = new[] { 7394 };
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            var w = new W();
            var parsed = w.ParseInput(input);
            //var result = w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 20); //3494

            //if (falseResults1.Any(fr => fr == result))
            //{
            //    return;
            //}

            //Console.WriteLine(result);
            //Console.ReadLine();

            var result2 = w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 50000000000);
            Console.WriteLine(result2);
            Console.ReadLine();

        }
    }

    public class W
    {
        public int CalculateAmoutnOfPlantsAfterGeneration(List<Plant> initialState, Rule[] rules, long generationNo)
        {
            var sum = 0;
            //int[] currentGeneration = initialState;

            //for (long i = 1; i < generationNo + 1; i++)
            //{
            //    currentGeneration = (GetNextGeneration(currentGeneration, rules));
            //    if (currentGeneration.All(p => p == 0))
            //    {
            //        return 0;
            //    }
            //}

            

            //for (var i = 0; i < currentGeneration.Length; i++)
            //{
            //    if (currentGeneration[i] == 1)
            //    {
            //        sum += i - (initialState.Length / 3);
            //    }
            //}

            return sum;
        }
        public int CalculateAmoutnOfPlantsAfterGeneration(int[] initialState, Rule[] rules, int generationNo)
        {
            var sum = 0;

            //var results = new List<int[]>(generationNo)
            //{
            //    initialState
            //};

            //for (int i = 1; i < generationNo + 1; i++)
            //{
            //    results.Add(GetNextGeneration(results[i - 1], rules));
            //}


            //var generationLine = results.Last();

            //for (var i = 0; i < generationLine.Length; i++)
            //{
            //    if (generationLine[i] == 1)
            //    {
            //        sum += i - (initialState.Length/3);
            //    }
            //}

            return sum;
        }

        public (List<Plant> initialState, Rule[] rules) ParseInput(string[] input)
        {
            var initialState = ParseInitialState(input[0]);
            var rules = ParseRules(input.Skip(2));
            return (initialState, rules );
        }

        public List<Plant> GetNextGeneration(List<Plant> state, Rule[] rules)
        {
            var result = new List<Plant>();

            foreach (var s in state)
            {
                var workingIndex = s.Index;
                foreach (var rule in rules)
                {
                    
                }
            }

            //for (var i = 0; i < state.Length - 2; i++)
            //{
            //    var toApply = true;
            //    foreach (var rule in rules)
            //    {
            //        toApply = true;
            //        for (var j = 0; j < rule.Sequence.Length; j++)
            //        {
            //            try
            //            {
            //                if (state[j + i] != rule.Sequence[j])
            //                {
            //                    toApply = false;
            //                    break;
            //                }
            //            }
            //            catch (Exception e)
            //            {
            //                toApply = false;
            //                break;
            //            }
            //        }
            //        if (toApply)
            //        {
            //            result.Add(rule.Result);
            //            break;
            //        }
            //    }
            //    if (!toApply)
            //    {
            //        result.Add(0);
            //    }
            //}

            return result;
        }

        private Rule[] ParseRules(IEnumerable<string> input)
        {
            var result = new List<Rule>();

            foreach (var rule in input)
            {
                var parsedRule = new Rule();
                for (var i = 0; i < 5; i++)
                {
                    parsedRule.Sequence[i] = rule[i] == '.' ? 0 : 1;
                }
                parsedRule.Result = rule.Last() == '.' ? 0 : 1;
                result.Add(parsedRule);
            }

            return result.ToArray();
        }

        private List<Plant> ParseInitialState(string input)
        {
            var plants = input.Split(" ")[2];

            var result = new List<Plant>();

            for (var i = 0; i < plants.Length; i++)
            {
                result.Add(new Plant()
                {
                    Index = i,
                    HasPlant = plants[i] == '#'
                });
            }

            return result;
        }
    }

    public class Rule
    {
        public Rule()
        {
            Sequence = new int[5];
        }
        public int[] Sequence { get; set; }
        public int Result { get; set; }
    }

    public class Plant
    {
        public int Index { get; set; }
        public bool HasPlant { get; set; }
    }
}
