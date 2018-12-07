using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Day6._1.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            var w = new Worker();
            var result = w.GetSizeOfTheLargestNotInfiniteArea(lines);

            Console.WriteLine(result);
        }
    }

    public class Worker
    {
        public int GetSizeOfTheLargestNotInfiniteArea(string[] lines)
        {
            var points = ParseLinesToPoints(lines).ToList();

            var areaSize = GetAreaSize(points);

            var area = new Point[areaSize.x, areaSize.y];

            for (var x = 0; x < areaSize.x; x++)
            {
                for (int y = 0; y < areaSize.y; y++)
                {
                    var calculatedDistance = points.Select(p => new {point = p, distance = CalculateDistance(x, y, p)})
                        .OrderBy(p => p.distance).ToList();
                    if (calculatedDistance[0].distance != calculatedDistance[1].distance)
                    {
                        var closesPoint = calculatedDistance.First();
                        closesPoint.point.Size++;
                        area[x, y] = closesPoint.point;
                    }
                }
            }

            points = RemoveInfinitePoints(points, area);

            return points.Max(p => p.Size);
        }

        private List<Point> RemoveInfinitePoints(List<Point> points, Point[, ] area)
        {
            var minX = points.Min(p => p.X);
            var maxX = points.Max(p => p.X);

            var minY = points.Min(p => p.Y);
            var maxY = points.Max(p => p.Y);

            var pointToRemove = new List<Point>();

            for (int x = minX; x < maxX; x++)
            {
                if (area[x, minY] != null) pointToRemove.Add(area[x, minY]);
                if(area[x, maxY - 1] != null) pointToRemove.Add(area[x, maxY-1]);
            }

            for (int y = minY; y < maxY; y++)
            {
                if(area[minX, y] != null) pointToRemove.Add(area[minX, y]);
                if(area[maxX - 1, y] != null) pointToRemove.Add(area[maxX-1, y]);
            }

            points.RemoveAll(p => pointToRemove.Distinct().Any(pt => pt.Id == p.Id));
            return points;
        }

        public IEnumerable<Point> ParseLinesToPoints(string[] lines)
        {
            return lines.Select(l => l.Split(", ")).Select(el => new Point() {X = int.Parse(el[0]), Y = int.Parse(el[1]), Id = Guid.NewGuid()});
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
        public int Size { get; set; }
    }
}
