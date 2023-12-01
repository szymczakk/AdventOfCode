using Common;
using Xunit.Abstractions;

namespace D1;

public class UnitTest1: TestBase
{
    private readonly ITestOutputHelper _outputHelper;

    public UnitTest1(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public async Task TestData()
    {
        var testData = await LoadTestFile();
        var result = D1.Task1(testData);
        Assert.Equal(142, result);
    }

    [Fact]
    public async Task RealData()
    {
        var data = await LoadFile();
        var result = D1.Task1(data);
        _outputHelper.WriteLine(result.ToString());
        Assert.Equal(54667, result);
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
}