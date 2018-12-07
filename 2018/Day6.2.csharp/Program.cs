using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6._2.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var w = new Worker();
            var result = w.FindSizeOfRegion(lines);

            Console.WriteLine(result);
        }
    }

    public class Worker
    {
        public int FindSizeOfRegion(string[] lines)
        {
            var points = ParseLinesToPoints(lines).ToList();

            var areaSize = GetAreaSize(points);

            var result = 0;

            for (var x = 0; x < areaSize.x; x++)
            {
                for (int y = 0; y < areaSize.y; y++)
                {
                    var sumOfDistance = points.Select(p => CalculateDistance(x, y, p)).Sum();
                    if (sumOfDistance < 10000)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public IEnumerable<Point> ParseLinesToPoints(string[] lines)
        {
            return lines.Select(l => l.Split(", ")).Select(el => new Point() { X = int.Parse(el[0]), Y = int.Parse(el[1]), Id = Guid.NewGuid() });
        }

        public (int x, int y) GetAreaSize(IEnumerable<Point> points)
        {
            var maxX = points.OrderBy(point => point.X).Last();
            var maxY = points.OrderBy(point => point.Y).Last();

            return (maxX.X, maxY.Y);
        }

        private int CalculateDistance(int x, int y, Point point)
        {
            return Math.Abs(point.X - x) + Math.Abs(point.Y - y);
        }
    }

    public class Point
    {
        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
