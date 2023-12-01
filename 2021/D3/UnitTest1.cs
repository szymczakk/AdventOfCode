using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace D3;

public class UnitTest1
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly string[] _input = { "00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" };

    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public void Test1()
    {
        var result = D.Z1(_input);
        Assert.Equal(198, result);
    }

    [Fact]
    public async Task T1()
    {
        var input = await D.LoadFile();
        var r = D.Z1(input);
        _outputHelper.WriteLine(r.ToString());
    }

    public static class D
    {
        public static async Task<string[]> LoadFile()
        {
            return await System.IO.File.ReadAllLinesAsync("./input.txt");
        }

        public static int Z1(string[] input)
        {
            var length = input[0].Length;
            var sbs = new StringBuilder[length].Select(x => new StringBuilder()).ToArray();

            for (int i = 0; i < length; i++)
            {
                foreach (var str in input)
                {
                    sbs[i].Append(str[i]);
                }
            }

            var resultingGamma = new StringBuilder();

            foreach (var sb in sbs)
            {
                var q = sb.ToString();
                var ones = q.Count(x => x == '1');
                var zeros = q.Count(x => x == '0');
                resultingGamma.Append(ones > zeros ? "1" : "0");
            }

            var resultingEpsilon = new StringBuilder();

            foreach (var g in resultingGamma.ToString())
            {
                resultingEpsilon.Append(g == '1' ? '0' : '1');
            }
            
            var gamma = Convert.ToInt32(resultingGamma.ToString(),2);
            var epsilon = Convert.ToInt32(resultingEpsilon.ToString(), 2);

            return gamma * epsilon;
        }
    }
}