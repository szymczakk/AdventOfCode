using Common;
using Xunit.Abstractions;

namespace D2;

public class UnitTest1 : TestBase
{
    public UnitTest1(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }


    [Fact]
    public async Task Test1()
    {
        var data = await LoadTestFile();
        var result = D2.Task1(data);
        Assert.Equal(8, result);
    }

    [Fact]
    public async Task RealData1()
    {
        var data = await LoadFile();
        var result = D2.Task1(data);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(2256, result);
    }

    [Fact]
    public async Task Test2()
    {
        var data = await LoadTestFile();
        var result = D2.Task2(data);
        Assert.Equal(2286, result);
    }

    [Fact]
    public async Task RealData2()
    {
        var data = await LoadFile();
        var result = D2.Task2(data);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(74229, result);
    }
}

public static class D2
{
    public static int Task1(IEnumerable<string> input)
    {
        return input.Select(x => new Game(x))
            .Aggregate(0, (i, game) =>
            {
                if (game.IsGameValid(12, 13, 14))
                {
                    return i + game.Id;
                }
                return i;
            });
    }

    public static int Task2(IEnumerable<string> input)
    {
        var result = input.Select(x => new Game(x))
            .Aggregate(0, (x, y) => x + y.MinCubeSetPow());
        return result;
    }
}

public class Game
{

    public Game(string line)
    {
        var splitted = line.Split(":");
        SetId(splitted[0]);
        SetCubeSets(splitted[1]);
    }

    public int Id { get; private set; }
    public List<CubeSet> CubeSets { get; } = new List<CubeSet>();

    private void SetId(string gameWithId)
    {
        var id = gameWithId.Trim().Split(" ");
        Id = int.Parse(id[1]);
    }

    private void SetCubeSets(string lineAfterId)
    {
        var splitted = lineAfterId.Trim().Split(";");
        foreach (var cubeSet in splitted)
        {
            CubeSets.Add(new CubeSet(cubeSet));
        }
    }


    public bool IsGameValid(int redCount, int greenCount, int blueCount)
    {
        foreach (var cubeSet in CubeSets)
        {
            var red = cubeSet.Red.Count();
            var blue = cubeSet.Blue.Count();
            var green = cubeSet.Green.Count();
            if (red > redCount || blue > blueCount || green > greenCount)
            {
                return false;
            }
        }
        return true;
    }

    public int MinCubeSetPow()
    {
        var maxRed = int.MinValue;
        var maxBlue = int.MinValue;
        var maxGreen = int.MinValue;

        foreach (var cubeSet in CubeSets)
        {
            var cubeSetBlue = cubeSet.Blue.Count();
            var cubeSetRed = cubeSet.Red.Count();
            var cubeSetGreen = cubeSet.Green.Count();

            if (cubeSetBlue > 0 && cubeSetBlue > maxBlue)
            {
                maxBlue = cubeSetBlue;
            }

            if (cubeSetRed > 0 && cubeSetRed > maxRed)
            {
                maxRed = cubeSetRed;
            }

            if (cubeSetGreen > 0 && cubeSetGreen > maxGreen)
            {
                maxGreen = cubeSetGreen;
            }
        }

        return maxBlue * maxRed * maxGreen;
    }
}

public class CubeSet
{

    public CubeSet(string cubeSetLine)
    {
        var splitted = cubeSetLine.Trim().Split(",");
        foreach (var cube in splitted)
        {
            var cubeSplitted = cube.Trim().Split(" ");
            var count = int.Parse(cubeSplitted[0]);
            var color = Enum.Parse<Color>(cubeSplitted[1]);
            switch (color)
            {
                case Color.blue:
                    Blue = Enumerable.Repeat(new Cube(color), count);
                    break;
                case Color.green:
                    Green = Enumerable.Repeat(new Cube(color), count);
                    break;
                case Color.red:
                    Red = Enumerable.Repeat(new Cube(color), count);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public IEnumerable<Cube> Blue { get; } = new List<Cube>();
    public IEnumerable<Cube> Green { get; } = new List<Cube>();
    public IEnumerable<Cube> Red { get; } = new List<Cube>();
}

public record Cube(Color Color);

public enum Color
{
    blue,
    red,
    green
}