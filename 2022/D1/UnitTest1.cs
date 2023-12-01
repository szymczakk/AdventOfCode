using Xunit.Abstractions;

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
        var input = await D1.LoadFile();
        var r = D1.Z1(input);
        Assert.True(r < 11357715);
        _outputHelper.WriteLine(r.ToString());
    }
}

public static class D1
{
    public static async Task<string> LoadFile()
    {
        return await System.IO.File.ReadAllTextAsync("./input.txt");
    }

    public static int Z1(string input)
    {
        var elfes = input.Split("\n\n")
            .Select(x => x.Split("\n"));
        
        
        
        return 0;
    }
}