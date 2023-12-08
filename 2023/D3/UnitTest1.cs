using System.Text;
using Common;
using Xunit.Abstractions;

namespace D3;

public class UnitTest1 : TestBase
{
    public UnitTest1(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public async void Test1()
    {
        var input = await LoadTestFile();
        var result = D3.Task1(input);
        Assert.Equal(4361, result);
    }

    [Fact]
    public async void RealTest1()
    {
        var input = await LoadFile();
        var result = D3.Task1(input);
        _outputHelper.WriteLine(result.ToString());
        Assert.True(result > 517752);
    }
}

internal static class D3
{
    internal static int Task1(string[] input)
    {
        var engine = new Engine(input);
        engine.ValidatePartNumbers();
        return engine.SumValidPartNumbers();
    }

    internal static int Task2(IEnumerable<string> input)
    {
        throw new NotImplementedException();
    }
}

internal class Engine
{
    public Engine(IEnumerable<string> input)
    {
        var partNumbers = new List<PartNumber>();
        var symbols = new List<Symbol>();
        var y = 0;
        foreach (var line in input)
        {
            var numberXStart = -1;
            var sb = new StringBuilder();

            for (var x = 0; x < line.Length; x++)
            {
                if (line[x].IsNumber() && numberXStart != -1)
                {
                    sb.Append(line[x]);
                }

                if (line[x].IsNumber() && numberXStart == -1)
                {
                    numberXStart = x;
                    sb.Append(line[x]);
                    continue;
                }

                if (line[x] == '.')
                {
                    if (numberXStart != -1)
                    {
                        partNumbers.Add(new PartNumber
                        {
                            XStart = numberXStart,
                            XEnd = x - 1,
                            Y = y,
                            Value = int.Parse(sb.ToString())
                        });
                        sb = new StringBuilder();
                        numberXStart = -1;
                    }
                    continue;
                }

                if (!line[x].IsNumber())
                {
                    symbols.Add(new Symbol
                    {
                        X = x,
                        Y = y,
                        Value = line[x].ToString()
                    });

                    if (numberXStart != -1)
                    {
                        partNumbers.Add(new PartNumber
                        {
                            XStart = numberXStart,
                            XEnd = x - 1,
                            Y = y,
                            Value = int.Parse(sb.ToString())
                        });
                        sb = new StringBuilder();
                        numberXStart = -1;
                    }
                }
            }
            y++;
        }

        PartNumbers = partNumbers;
        Symbols = symbols;
    }

    public IEnumerable<PartNumber> PartNumbers { get; set; }
    public IEnumerable<Symbol> Symbols { get; set; }

    public void ValidatePartNumbers()
    {
        var change = new List<(int dx, int dy)>
        {
            (-1, -1),
            (-1, 0),
            (-1, +1),
            (0, -1),
            (0, 0),
            (0, +1),
            (+1, -1),
            (+1, 0),
            (+1, +1)
        };
        foreach (var partNumber in PartNumbers)
        {
            for (var x = partNumber.XStart; x <= partNumber.XEnd; x++)
                foreach (var (dx, dy) in change)
                {
                    var symbol = Symbols.FirstOrDefault(s => s.X == x + dx && s.Y == partNumber.Y + dy);
                    if (symbol == null)
                    {
                        continue;
                    }
                    partNumber.IsValid = true;
                    break;
                }
        }
    }

    public int SumValidPartNumbers()
    {
        return PartNumbers.Where(x => x.IsValid).Sum(x => x.Value);
    }
}

internal class PartNumber
{
    public int Y { get; set; }
    public int XStart { get; set; }
    public int XEnd { get; set; }
    public int Value { get; set; }
    public bool IsValid { get; set; }
}

internal class Symbol
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Value { get; set; }
}