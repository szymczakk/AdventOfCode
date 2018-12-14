using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;

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
            List<Plant> currentGeneration = initialState;

            for (long i = 1; i < generationNo + 1; i++)
            {
                currentGeneration = (GetNextGeneration(currentGeneration, rules));
                if (currentGeneration.All(p => !p.HasPlant))
                {
                    return 0;
                }
            }

            return currentGeneration.Where(p => p.HasPlant).Sum(p => p.Index);
        }

        public int CalculateAmoutnOfPlantsAfterGeneration(List<Plant> initialState, Rule[] rules, int generationNo)
        {
            return CalculateAmoutnOfPlantsAfterGeneration(initialState, rules, (long) generationNo);
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

            var startIndex = state.First(p => p.HasPlant);
            
            for (int i = startIndex.Index - 4; i < state.Count; i++)
            {
                var tempState = new List<Plant>(5);

                if (i < 0)
                {
                    for (int q = i; q < 0; q++)
                    {
                        tempState.Add(new Plant()
                        {
                            HasPlant = false,
                            Index = q
                        });
                    }
                }

                var toAdd = 5 - tempState.Count;

                tempState.AddRange(state.Where(p => p.Index >= startIndex.Index).Skip(i).Take(toAdd));

                toAdd = 5 - tempState.Count;
                for (int q = 0; q < toAdd; q++)
                {
                    tempState.Add(new Plant()
                    {
                        HasPlant = false,
                        Index = state.Count + q
                    });
                }
                var passedRule = true;
                foreach (var rule in rules)
                {
                    passedRule = true;
                    for (int j = 0; j < 5; j++)
                    {
                        if ((rule.Sequence[j] == 1) != tempState[j].HasPlant)
                        {
                            passedRule = false;
                            break;
                        }
                    }
                    if (passedRule)
                    {
                        result.Add(new Plant()
                        {
                            Index = i + 2,
                            HasPlant = (rule.Result == 1)
                        });
                        break;
                    }
                }
                if (!passedRule)
                {
                    result.Add(new Plant()
                    {
                        HasPlant = false,
                        Index = i + 2
                    });
                }
            }
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
