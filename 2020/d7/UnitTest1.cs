using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace d7
{
    public class UnitTest1
    {
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

            var result = D.T1(seed);
            
            Assert.Equal(4, result);
        }
    }

    public class Bag
    {
        public Bag()
        {
            CanContain = new List<(Bag, int amount)>();
        }
        
        public string Color { get; set; }

        public IList<(Bag, int amount)> CanContain { get; set; }
    }

    
    public static class D
    {
        public static async Task<string> LoadFile()
        {
            return await System.IO.File.ReadAllTextAsync("./input.txt");
        }
        
        public static int T1(string arg)
        {
            var pattern = "";
            const string noBags = "no other bags";
            
            var bagRules = new Dictionary<string, Bag>();
            
            var lines = arg.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                var data = line.Split("containt");
                var bagName = data[0].Replace("bags", "").Trim();
                var bag = bagRules.ContainsKey(bagName) ? bagRules[bagName] : new Bag(); 
                
            }
            
            
            return 0;
        }
    }
}