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
        var result = D5.Task1(input);
        Assert.Equal(288, result);
    }

    [Fact]
    public async void RealTest()
    {
        var input = await LoadFile();
        var result = D5.Task1(input);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(440000, result);
    }
}

public static class D5
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
}

public class Race
{
    public int Time { get; set; }
    public int RecordDistance { get; set; }

    public IList<Boat> WinningBoats { get; set; } = new List<Boat>();
}

public class Boat
{
    public int Speed { get; set; }

    public int GetDistance(int time)
    {
        return Speed * time;
    }
}