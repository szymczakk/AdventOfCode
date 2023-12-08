using Common;
using Xunit.Abstractions;

namespace D6;

public class UnitTest1 : TestBase
{
    public UnitTest1(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public async void Test1()
    {
        var input = await LoadTestFile();
        var result = D6.Task1(input);
        Assert.Equal(288, result);
    }

    [Fact]
    public async void RealTest()
    {
        var input = await LoadFile();
        var result = D6.Task1(input);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(440000, result);
    }

    [Fact]
    public async void Test2()
    {
        var input = await LoadTestFile();
        var result = D6.Task2(input);
        Assert.Equal(71503, result);
    }

    [Fact]
    public async void RealTest2()
    {
        var input = await LoadFile();
        var result = D6.Task2(input);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(26187338, result);
    }
}

public static class D6
{
    public static int Task1(string[] input)
    {
        var result = 1;
        var times = input[0].Split(" ")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .Skip(1)
            .Select(int.Parse)
            .ToArray();

        var distances = input[1].Split(" ")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .Skip(1)
            .Select(int.Parse)
            .ToArray();

        var races = times.Select((t, i) => new Race
            {
                Time = t,
                RecordDistance = distances[i]
            })
            .ToList();

        foreach (var race in races)
        {
            for (var i = 0; i <= race.Time; i++)
            {
                var boat = new Boat
                {
                    Speed = i
                };

                if (boat.GetDistance(race.Time - i) > race.RecordDistance)
                {
                    race.WinningBoats.Add(boat);
                }
            }
            result *= race.WinningBoats.Count;
        }

        return result;
    }

    public static int Task2(string[] input)
    {
        var time = input[0].Split(" ")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .Skip(1)
            .Aggregate(string.Empty, (s, s1) => s + s1);

        var distance = input[1].Split(" ")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x))
            .Skip(1)
            .Aggregate(string.Empty, (s, s1) => s + s1);

        var race = new Race
        {
            Time = double.Parse(time),
            RecordDistance = double.Parse(distance)
        };


        for (var i = 0; i <= race.Time; i++)
        {
            var boat = new Boat
            {
                Speed = i
            };

            if (boat.GetDistance(race.Time - i) > race.RecordDistance)
            {
                race.WinningBoats.Add(boat);
            }
        }
        return race.WinningBoats.Count;
    }
}

public class Race
{
    public double Time { get; set; }
    public double RecordDistance { get; set; }

    public IList<Boat> WinningBoats { get; set; } = new List<Boat>();
}

public class Boat
{
    public int Speed { get; set; }

    public double GetDistance(double time)
    {
        return Speed * time;
    }
}