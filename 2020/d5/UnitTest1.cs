using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace d5
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

            var input = @"BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL";

            var result = D.T1(input);
            
            Assert.Equal(820, result);

        }

        [Fact]
        public async Task Prod1()
        {
            var input = await D.LoadFile();
            var result = D.T1(input);

            _testOutputHelper.WriteLine(result.ToString());

            Assert.Equal(838, result);
        }

        [Fact]
        public async Task Prod2()
        {
            var input = await D.LoadFile();
            var result = D.T2(input);

            _testOutputHelper.WriteLine(result.ToString());

            Assert.Equal(714, result);
        }

    }

    public static class D
    {
        public static async Task<string> LoadFile()
        {
            return await System.IO.File.ReadAllTextAsync("./input.txt");
        }

        private static long CalculateSeatId(string bordingPass)
        {
            var rowAsBin = bordingPass.Substring(0, 7).Replace("F", "0").Replace("B", "1");
            var seatAsBin = bordingPass.Substring(7, 3).Replace("L", "0").Replace("R", "1");

            var rowNo = Convert.ToInt32(rowAsBin, 2);
            var seatNo = Convert.ToInt32(seatAsBin, 2);

            return rowNo * 8 + seatNo;
        }

        public static long T1(string arg)
        {
            var results = arg.Split(Environment.NewLine).Select(CalculateSeatId);
            
            return results.Max();
        }

        public static long T2(string arg)
        {
            var results = arg.Split(Environment.NewLine).Select(CalculateSeatId).OrderBy(l => l).ToList();

            for (int i = 1; i < results.Count() -1; i++)
            {
                var prev = results[i - 1];
                var curr = results[i];

                if (curr - prev != 1)
                {
                    return curr - 1;
                }
            }

            return 0;
        }
    }
}