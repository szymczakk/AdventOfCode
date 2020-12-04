using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace d3
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _outputHelper;

        public UnitTest1(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        
        [Fact]
        public void Test1()
        {
            var input = 
                @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

            var result = D.T1(input.Split(Environment.NewLine), 3, 1);
            Assert.Equal(7, result);
        }

        [Fact]
        public async Task Prod1()
        {
            var input = await D.LoadFile();

            var result = D.T1(input, 3 , 1);
            
            Assert.Equal(159, result);
            _outputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public void Test2()
        {
            var input = 
                @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";
            
            var slopes = new (int x, int y)[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)};


            var results = new List<int>();
            foreach (var slope in slopes)
            {
                var r = D.T1(input.Split(Environment.NewLine), slope.x, slope.y);
                results.Add(r);
            }

            var result = results.Aggregate(1, (r, c) => r * c);
            
            Assert.Equal(336, result);
            _outputHelper.WriteLine(result.ToString());
        }

        [Fact]
        public async Task Prod2()
        {
            var input = await D.LoadFile();

            var slopes = new (int x, int y)[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)};

            var results = new List<long>();
            foreach (var slope in slopes)
            {
                var r = D.T1(input, slope.x, slope.y);
                results.Add(r);
            }

            long result = results.Aggregate(1L, (r, c) => r * c);

            _outputHelper.WriteLine(result.ToString());

            Assert.NotEqual(2124702224, result);
            Assert.Equal(6419669520, result);
        }
    }

    public static class D
    {
        public static async Task<string[]> LoadFile()
        {
            return await System.IO.File.ReadAllLinesAsync("./input.txt");
        }

        public static int T1(string[] args, int xToAdd, int yToAdd)
        {
            var startPos = (0, 0);
            const char tree = '#';


            var map = args;
            var (currx, curry) = startPos;

            var result = new List<bool>();
            
            for(var i = startPos.Item1; i <= map.Length / yToAdd; i++)
            {
                currx = currx + xToAdd;
                curry = curry + yToAdd;

                if (currx >= map[0].Length)
                {
                    currx = currx - map[0].Length;
                }

                if (curry >= map.Length)
                {
                    break;
                }
                
                var isTree = map[curry][currx] == tree;

                if (isTree)
                {
                    result.Add(true);
                }
            }
            
            
            return result.Count();
        }
    }
}