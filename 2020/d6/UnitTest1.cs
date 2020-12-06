using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace d6
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
            var seed = @"abc

a
b
c

ab
ac

a
a
a
a

b";

            var result = D.T1(seed);

            Assert.Equal(11, result);
        }

        [Fact]
        public async Task Prod1()
        {
            var input = await D.LoadFile();
            var result = D.T1(input);
            
            _testOutputHelper.WriteLine(result.ToString());
            
            Assert.Equal(6443, result);
            
        }

        [Fact]
        public void Test2()
        {
            var seed = @"abc

a
b
c

ab
ac

a
a
a
a

b";

            var result = D.T2(seed);

            Assert.Equal(6, result);
        }
        
        [Fact]
        public async Task Prod2()
        {
            var input = await D.LoadFile();
            var result = D.T2(input);
            
            _testOutputHelper.WriteLine(result.ToString());
            
            Assert.True(result < 3597);
            Assert.Equal(3232, result);
        }
        
        
    }

    public static class D
    {
        public static async Task<string> LoadFile()
        {
            return await System.IO.File.ReadAllTextAsync("./input.txt");
        }

        private static List<string> GetGroups(string input)
        {
            var lines = input.Split(Environment.NewLine);
            
            var result = new List<string>();
            var sb = new StringBuilder();

            for (var i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    result.Add(sb.ToString());
                    sb = new StringBuilder();
                    continue;
                }

                sb.AppendLine(lines[i]);
            }
            
            result.Add(sb.ToString());

            return result;
        }

        private static int CountYesForGroup(string group)
        {
            return group.Replace(Environment.NewLine, "").Distinct().Count();
        }

        private static int CountYesForEveryoneInGroup(string group)
        {
            var answers = new Dictionary<char, int>();

            foreach (var answer in group.Split(Environment.NewLine))
            {
                foreach (var q in answer)
                {
                    if(answers.ContainsKey(q))
                    {
                        answers[q]++;
                    }
                    else
                    {
                        answers[q] = 1;
                    }
                }
            }

            return answers.Count(a => a.Value == group.Split(Environment.NewLine).Length - 1);
        }
        
        public static int T1(string arg)
        {
            var grups = GetGroups(arg);

            var results = grups.Select(CountYesForGroup);
            
            return results.Sum();
        }
        
        public static int T2(string arg)
        {
            var grups = GetGroups(arg);

            var results = grups.Select(CountYesForEveryoneInGroup);
            
            return results.Sum();
        }
    }
}