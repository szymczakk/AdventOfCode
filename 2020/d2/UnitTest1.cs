using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace d2
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _outputHelper;

        public UnitTest1(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        
        [Fact]
        public void Test1()
        {
            var input = new string[] {"1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"};
            
            Assert.Equal(2, D.Z1(input));
        }

        [Fact]
        public async Task Prod1()
        {
            var result = D.Z1(await D.LoadFile());
            _outputHelper.WriteLine(result.ToString());
            
            Assert.Equal(569, result);
        }

        [Fact]
        public void Test2()
        {
            var input = new string[] {"1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"};
            
            Assert.Equal(1, D.Z2(input));
        }

        [Fact]
        public async Task Prod2()
        {
            var result = D.Z2(await D.LoadFile());
            _outputHelper.WriteLine(result.ToString());
            
            //Assert.Equal(569, result);
        }
    }

    public static class D
    {
        public static async Task<string[]> LoadFile()
        {
            return await System.IO.File.ReadAllLinesAsync("./input.txt");
        }

        private static (string howMany, string letter, string password) ParsePassword(string line)
        {
            var splited = line.Split(" ");
            
            return (splited[0], splited[1].Substring(0, 1), splited[2]);
        }

        private static bool ValidatePassword1((string howMany, string letter, string password) line)
        {
            var letterCount = line.password.Count(c => c.ToString() == line.letter);

            var minOcccurrence = int.Parse(line.howMany.Split("-")[0]);
            var maxOcccurrence = int.Parse(line.howMany.Split("-")[1]);

            if (letterCount >= minOcccurrence && letterCount <= maxOcccurrence)
            {
                return true;
            }
            
            return false;
        }
        
        private static bool ValidatePassword2((string howMany, string letter, string password) line)
        {
            var letterCount = line.password.Count(c => c.ToString() == line.letter);

            var firstOcccurrence = int.Parse(line.howMany.Split("-")[0]) -1;
            var secondOcccurrence = int.Parse(line.howMany.Split("-")[1]) -1;

            if (line.password[firstOcccurrence].ToString() == line.letter && line.password[secondOcccurrence].ToString() != line.letter)
            {
                return true;
            }
            
            if (line.password[firstOcccurrence].ToString() != line.letter && line.password[secondOcccurrence].ToString() == line.letter)
            {
                return true;
            }
            
            return false;
        }

        public static int Z1(string[] arg)
        {
            var results = new List<bool>();
            var lines = (arg).Select(ParsePassword);
            foreach (var line in lines)
            {
                results.Add(ValidatePassword1(line));
            }

            return results.Count(r => r == true);
        } 
        
        public static int Z2(string[] arg)
        {
            var results = new List<bool>();
            var lines = (arg).Select(ParsePassword);
            foreach (var line in lines)
            {
                results.Add(ValidatePassword2(line));
            }

            return results.Count(r => r == true);
        } 
    }
}