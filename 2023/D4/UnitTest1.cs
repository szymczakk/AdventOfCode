using Common;
using Xunit.Abstractions;

namespace D4;

public class UnitTest1 : TestBase
{
    public UnitTest1(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public async void Test1()
    {
        var input = await LoadTestFile();
        var result = D4.Task1(input);
        Assert.Equal(13, result);
    }

    [Fact]
    public async void RealTest1()
    {
        var input = await LoadFile();
        var result = D4.Task1(input);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(21485, result);
    }

    [Fact]
    public async void Test2()
    {
        var input = await LoadTestFile();
        var result = D4.Task2(input);
        Assert.Equal(30, result);
    }

    [Fact]
    public async void RealTest2()
    {
        var input = await LoadFile();
        var result = D4.Task2(input);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(11024379, 0);
    }
}

public static class D4
{
    public static double Task1(IEnumerable<string> input)
    {
        double result = 0;
        foreach (var line in input)
        {
            var card = new Card(line);
            var score = card.CalculateScore();
            result += score;
        }
        return result;
    }

    public static int Task2(IEnumerable<string> input)
    {
        var cards = input
            .Select(x => new Card(x))
            .ToArray();

        for (var i = 0; i < cards.Length; i++)
        {
            var card = cards[i];
            var winning = card.WinningCount();
            for (var z = 0; z < card.Copies; z++)
            for (var x = 1; x <= winning; x++)
                cards[i + x].Copies++;
        }

        return cards.Sum(x => x.Copies);
    }
}

internal class Card
{
    public Card(string line)
    {

        try
        {
            var parts = line.Split(": ");
            Id = int.Parse(parts[0].Split(" ").Last(x => !string.IsNullOrEmpty(x)));
            var parts2 = parts[1].Split(" | ");

            WinningNumbers = parts2[0]
                .Split(" ")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => int.Parse(x.Trim()))
                .ToList();

            YourNumbers = parts2[1]
                .Split(" ")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => int.Parse(x.Trim()))
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public int Id { get; set; }
    public int Copies { get; set; } = 1;
    public IEnumerable<int> WinningNumbers { get; set; }
    public IEnumerable<int> YourNumbers { get; set; }

    public int WinningCount()
    {
        return WinningNumbers.Count(x => YourNumbers.Contains(x));
    }

    public double CalculateScore()
    {
        var winningCount = WinningCount();
        return winningCount switch
        {
            0 => 0,
            1 => 1,
            _ => Math.Pow(2, winningCount - 1)
        };
    }
}