using System;
using System.Linq;
using System.Threading.Tasks;

namespace d1
{
    public static class Program
    {
        async static Task Main(string[] args)
        {
            var input = await System.IO.File.ReadAllLinesAsync("./input.txt");
            var result1 = C1(input);
            Console.WriteLine($"Result1: {result1}");
            var r2 = C2(input);
            Console.WriteLine($"Result2:{r2}");
        }

        public static int C1(string[] arg)
        {
            var input = arg.Select(int.Parse).ToArray();
            for (int i = 0; i < input.Count(); i++)
            {
                var num = input[i];
                for (int j = i; j < input.Count(); j++)
                {
                    if (num + input[j] == 2020)
                    {
                        return num * input[j];
                    }
                }
            }
            return 0;
        }

        public static int C2(string[] arg)
        {
            var input = arg.Select(int.Parse).ToArray();
            for (int i = 0; i < input.Count(); i++)
            {
                var num1 = input[i];
                for (int j = i; j < input.Count(); j++)
                {
                    var num2 = input[j];
                    for (int k = j; k < input.Count(); k++)
                    {
                        if (num1 + num2 + input[k] == 2020)
                        {
                            return num1 * num2 * input[k];
                        }
                    }
                }
            }
            return 0;
        }
    }
}