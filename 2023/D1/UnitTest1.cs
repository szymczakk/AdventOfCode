using Common;
using Xunit.Abstractions;

namespace D1;

public class UnitTest1 : TestBase
{
    public UnitTest1(ITestOutputHelper outputHelper): base(outputHelper)
    {
    }

    [Fact]
    public async Task TestData1()
    {
        var testData = await LoadTestFile();
        var result = D1.Task1(testData);
        Assert.Equal(142, result);
    }

    [Fact]
    public async Task RealData1()
    {
        var data = await LoadFile();
        var result = D1.Task1(data);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(54667, result);
    }

    [Fact]
    public async Task TestData2()
    {
        var testData = await LoadTestFile("./test-input2.txt");
        var result = D1.Task2(testData);
        Assert.Equal(281, result);
    }
    
    [Fact]
    public async Task RealData2()
    {
        var data = await LoadFile();
        var result = D1.Task2(data);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(54203, result);
    }
}

public static class D1
{
    public static int Task1(IEnumerable<string> input)
    {
        var result = 0;
        foreach (var line in input)
        {
            var firstNum = line.First(x => int.TryParse(x.ToString(), out _));
            var lastNum = line.Last(x => int.TryParse(x.ToString(), out _));
            var sumChar = $"{firstNum}{lastNum}";
            var sum = int.Parse(sumChar);
            result += sum;
        }
        return result;
    }

    public static int Task2(IEnumerable<string> input)
    {
        var numbers = new Dictionary<string, int>
        {
            { "one", 1 },
            { "1", 1 },
            { "two", 2 },
            { "2", 2 },
            { "three", 3 },
            { "3", 3 },
            { "four", 4 },
            { "4", 4 },
            { "five", 5 },
            { "5", 5 },
            { "six", 6 },
            { "6", 6 },
            { "seven", 7 },
            { "7", 7 },
            { "eight", 8 },
            { "8", 8 },
            { "nine", 9 },
            { "9", 9 }
        };
        var result = 0;
        foreach (var line in input)
        {
            var searchResult = new Dictionary<int, int>();
            
            foreach (var k in numbers)
            {
                var index = 0;
                while ((index = line.IndexOf(k.Key, index, StringComparison.Ordinal)) != -1)
                {
                    searchResult.Add(index++, k.Value);
                }
            }
            
            var min = searchResult.Min(x => x.Key);
            var max = searchResult.Max(x => x.Key);
            var minValue = searchResult[min];
            var maxValue = searchResult[max];
            var resultingNumber = int.Parse($"{minValue}{maxValue}");
            result += resultingNumber;
        }
        return result;
    }
}