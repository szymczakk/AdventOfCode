using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace d7
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var seed = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

            var result = D.T1(seed, "shiny gold");

            Assert.Equal(4, result);
        }

        [Fact]
        public async Task Prod1()
        {
            var seed = await D.LoadFile();
            var result = D.T1(seed, "shiny gold");
            _testOutputHelper.WriteLine(result.ToString());

            Assert.True(result < 198);
            Assert.NotEqual(12, result);
            Assert.NotEqual(4, result);
            Assert.NotEqual(141, result);
            Assert.NotEqual(31, result);
            Assert.NotEqual(32, result); //gues
            Assert.Equal(144, result);
        }

        [Fact]
        public void Test2()
        {
            var seed = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags, 2 muted violet bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";

            var result = D.T2(seed, "shiny gold");
            Assert.Equal(126, result);
        }
        
        [Fact]
        public void Test21()
        {
            var seed = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

            var result = D.T2(seed, "shiny gold");
            Assert.Equal(32, result);
        }

        [Fact]
        public async Task Prod2()
        {
            var input = await D.LoadFile();

            var result = D.T2(input, "shiny gold");
            
            _testOutputHelper.WriteLine(result.ToString());
            
            Assert.True(result > 3262);
        }
    }

    public class Bag
    {
        public Bag(string color)
        {
            CanContain = new List<(Bag, int amount)>();
            Color = color;
        }

        public string Color { get; }

        public IList<(Bag, int amount)> CanContain { get; set; }
    }


    public static class D
    {
        public static async Task<string> LoadFile()
        {
            return await System.IO.File.ReadAllTextAsync("./input.txt");
        }

        public static int T1(string arg, string bagNameToLookFor)
        {
            var bagRules = GetBagRules(arg);
            return GetBagsThatCanContain(bagNameToLookFor, bagRules).Count();
        }

        public static double T2(string arg, string bagNameToLookFor)
        {
            var bagRules = GetBagRules(arg);

            var bag = bagRules[bagNameToLookFor];

            var result = CountContainingBags(bag, 1);

            return result;
        }

        private static int CountContainingBags(Bag bag, int amount)
        {
            var result = (bag.CanContain.Select(cc => cc.amount).Sum()) * amount;

            foreach (var b in bag.CanContain)
            {
                result += CountContainingBags(b.Item1, b.amount);
            }
            
            
            return result;
        }

        // private static (double result, int power) CountContainingBags(Bag bag, int depth)
        // {
        //     var result = (0, 0);
        //     if (!bag.CanContain.Any())
        //     {
        //         return result;
        //     }
        //
        //     foreach (var b in bag.CanContain)
        //     {
        //         var (currentResult, power) = CountContainingBags(b.Item1, ++depth);
        //         power++;
        //         var actualResutl = currentResult + (Math.Pow(b.amount, power));
        //         return (actualResutl, power);
        //     }
        //
        //     return result;
        // }

        private static bool LookForBag(string bagToLookFor, Bag bag)
        {
            if (bag.CanContain.Any(cc => cc.Item1.Color == bagToLookFor))
            {
                return true;
            }

            foreach (var b in bag.CanContain)
            {
                if (LookForBag(bagToLookFor, b.Item1))
                {
                    return true;
                }
            }

            return false;
        }

        private static string InitialBag(string lineFirstHalf)
        {
            return lineFirstHalf.Replace("bags", "").Trim();
        }

        private static IEnumerable<(int amoutn, string bagName)> ParseBagRules(string lineSecondHalf)
        {
            var result = new List<(int, string)>();

            const string noBags = "no other bags";
            if (lineSecondHalf.Contains(noBags))
            {
                return result;
            }

            var rules = lineSecondHalf.Split(",");
            foreach (var rule in rules)
            {
                var r = rule.Replace("bags", "")
                    .Replace("bag", "")
                    .Replace(",", "")
                    .Replace(".", "'")
                    .Trim()
                    .Split(" ");

                result.Add((int.Parse(r[0]), $"{r[1].Trim()} {r[2].Trim()}"));
            }

            return result;
        }

        private static Dictionary<string, Bag> GetBagRules(string arg)
        {
            var bagRules = new Dictionary<string, Bag>();

            var lines = arg.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                var data = line.Split("contain");
                if (data.Any(string.IsNullOrEmpty))
                {
                    continue;
                }

                var bagName = InitialBag(data[0]);
                var bag = bagRules.ContainsKey(bagName) ? bagRules[bagName] : new Bag(bagName);

                foreach (var ruleStr in ParseBagRules(data[1]))
                {
                    Bag b;
                    if (bagRules.ContainsKey(ruleStr.bagName))
                    {
                        b = bagRules[ruleStr.bagName];
                    }
                    else
                    {
                        b = new Bag(ruleStr.bagName);
                        bagRules.Add(ruleStr.bagName, b);
                    }

                    bag.CanContain.Add((b, ruleStr.amoutn));
                }

                if (!bagRules.ContainsKey(bagName))
                {
                    bagRules.Add(bagName, bag);
                }
            }

            return bagRules;
        }

        private static List<Bag> GetBagsThatCanContain(string bagNameToLookFor, Dictionary<string, Bag> bagRules)
        {
            var result = new List<Bag>();

            foreach (var bag in bagRules.Select(bagRule => bagRule.Value))
            {
                if (LookForBag(bagNameToLookFor, bag) && !result.Contains(bag))
                {
                    result.Add(bag);
                }
            }

            return result;
        }
    }
}