using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Day12._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            var w = new W();
            var parsed = w.ParseInput(input);
            var result = w.CalculateAmoutnOfPlantsAfterGeneration(parsed.Item1, parsed.Item2, 20);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }

    public class W
    {
        public int CalculateAmoutnOfPlantsAfterGeneration(int[] initialState, Rule[] rules, int generationNo)
        {
            var results = new List<int[]>(generationNo)
            {
                initialState
            };

            for (int i = 1; i < generationNo + 1; i++)
            {
                results.Add(GetNextGeneration(results[i - 1], rules));
            }

            var sum = 0;

            foreach (var generationLine in results)
            {
                sum += generationLine.Sum();
            }

            return sum;
        }

        public (int[] initialState, Rule[] rules) ParseInput(string[] input)
        {
            var initialState = ParseInitialState(input[0]);
            var rules = ParseRules(input.Skip(2));
            return (initialState, rules );
        }

        public int[] GetNextGeneration(int[] state, Rule[] rules)
        {
            var result = new List<int>
            {
                state[0], state[1]
            };

            for (var i = 0; i < state.Length; i++)
            {
                var toApply = true;
                foreach (var rule in rules)
                {
                    toApply = true;
                    for (var j = 0; j < rule.Sequence.Length; j++)
                    {
                        try
                        {
                            if (state[j + i] != rule.Sequence[j])
                            {
                                toApply = false;
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            toApply = false;
                            break;
                        }
                    }
                    if (toApply)
                    {
                        result.Add(rule.Result);
                        break;
                    }
                }
                if (!toApply)
                {
                    result.Add(0);
                }
            }

            return result.ToArray();
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

        private int[] ParseInitialState(string input)
        {
            var plants = input.Split(" ")[2];

            var result = new int[plants.Length * 3];

            for (var i = 0; i < plants.Length; i++)
            {
                result[i + plants.Length] = plants[i] == '.' ? 0 : 1;
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
}
