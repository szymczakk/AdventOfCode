using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace D1;

public class UnitTest1
{
    private readonly ITestOutputHelper _outputHelper;
    
    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        var input = new string[] { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" };
        var r = await D.GetResult(input);
        Assert.Equal(7, r);
    }

    [Fact]
    public async Task Test11()
    {
        var input = await D.LoadFile();
        var r = await D.GetResult(input);
        _outputHelper.WriteLine(r.ToString());
    }

    [Fact]
    public void Test2()
    {
        var input = new string[] { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" };
        var r = D.GetResult2(input);
        Assert.Equal(5, r);
    }

    [Fact]
    public async Task Test22()
    {
        var input = await D.LoadFile();
        var r = D.GetResult2(input);
        _outputHelper.WriteLine(r.ToString());
    }

    public static class D
    {
        public static async Task<string[]> LoadFile()
        {
            return await System.IO.File.ReadAllLinesAsync("./input.txt");
        }

        public static async Task<int> GetResult(string[] input)
        {
            var result = 0;
            for (var i = 1; i < input.Length; i++)
            {
                var prev = int.Parse(input[i - 1]);
                var curr = int.Parse(input[i]);

                if (curr > prev)
                {
                    result++;
                }
            }

            return result;
        }

        public static int GetResult2(string[] input)
        {
            var result = 0;
            for (var i = 1; i < input.Length; i++)
            {
                try
                {
                    var v1 = int.Parse(input[i - 1]);
                    var v2 = int.Parse(input[i]);
                    var v3 = int.Parse(input[i + 1]);
                    var v = v1 + v2 + v3;

                    var q1 = int.Parse(input[i]);
                    var q2 = int.Parse(input[i + 1]);
                    var q3 = int.Parse(input[i + 2]);
                    var q = q1 + q2 + q3;

                    if (q > v)
                    {
                        result++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return result;
                }
            }

            return result;
        }
    }
}