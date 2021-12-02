using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace D2;

public class UnitTest1
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly string[] _testInput = new string[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };

    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    [Fact]
    public void Test1()
    {
        var r = D.D1(_testInput);
        Assert.Equal(150, r);
    }

    [Fact]
    public async Task Test11()
    {
        var r = D.D1(await D.LoadFile());
        _outputHelper.WriteLine(r.ToString());
    }

    [Fact]
    public void Test2()
    {
        var r = D.D2(_testInput);
        Assert.Equal(900, r);
    }

    [Fact]
    public async Task Test22()
    {
        var r = D.D2(await D.LoadFile());
        _outputHelper.WriteLine(r.ToString());
    }

    public class D
    {
        public static async Task<string[]> LoadFile()
        {
            return await System.IO.File.ReadAllLinesAsync("./input.txt");
        }

        public static int D1(string[] input)
        {
            var x = 0;
            var y = 0;
            foreach (var s in input)
            {
                var ss = s.Split(' ');
                var value = int.Parse(ss[1]);
                switch (ss[0].Trim())
                {
                    case "forward":
                        x += value;
                        break;
                    case "up":
                        y -= value;
                        break;
                    case "down":
                        y += value;
                        break;
                }
                
            }

            return x * y;
        }

        public static int D2(string[] input)
        {
            var aim = 0;
            var x = 0;
            var y = 0;
            var depth = 0;
            foreach (var s in input)
            {
                var ss = s.Split(' ');
                var value = int.Parse(ss[1]);
                switch (ss[0].Trim())
                {
                    case "forward":
                        x += value;
                        depth += aim * value;
                        break;
                    case "up":
                        y -= value;
                        aim -= value;
                        break;
                    case "down":
                        y += value;
                        aim += value;
                        break;
                }
                
            }

            return x * depth;
        }
    }
}