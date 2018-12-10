using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            var w = new W();
            w.DoThings(input);
            Console.ReadLine();
        }
    }

    public class W
    {
        public void DoThings(string[] input)
        {
            var parsed = ParseInput(input);
            var hasAText = false;

            var currentX = int.MaxValue;
            var currentY = int.MaxValue;
            var counter = 0;
            while (!hasAText)
            {
                hasAText = DoWeProbableHadAText(parsed, ref currentX, ref currentY);
                if (hasAText)
                {
                    parsed = MovePoints(parsed, MovePointDirection.Backward);
                    counter--;
                    break;
                }
                parsed = MovePoints(parsed, MovePointDirection.Forward);
                counter++;
            }
            Console.WriteLine("Time: {0}", counter);
            PrintPoints(parsed, currentX, currentY);
        }

        private bool DoWeProbableHadAText(List<Point> parsed, ref int currentX, ref int currentY)
        {
            var maxX = parsed.Max(point => point.x);
            var maxY = parsed.Max(point => point.y);
            bool passed = true;

            if (maxX < currentX)
            {
                currentX = maxX;
                passed = false;
            }

            if (maxY < currentY)
            {
                currentY = maxY;
                passed = false;
            }

            return passed;
        }

        public List<Point> ParseInput(string[] input)
        {
            var result = new List<Point>(input.Length);

            foreach (var line in input)
            {
                var p = new Point();
                var splited = line.Replace("position=", "").Replace(" velocity=", "").Split(new[] {'<', '>', ','},StringSplitOptions.RemoveEmptyEntries);
                p.x = int.Parse(splited[0]);
                p.y = int.Parse(splited[1]);
                p.Vx = int.Parse(splited[2]);
                p.Vy = int.Parse(splited[3]);
                result.Add(p);
            }

            return result;
        }

        public List<Point> MovePoints(List<Point> points, MovePointDirection direction)
        {
            foreach (var point in points)
            {
                if (direction == MovePointDirection.Forward)
                {
                    point.x += point.Vx;
                    point.y += point.Vy;
                }
                else
                {
                    point.x -= point.Vx;
                    point.y -= point.Vy;
                }
            }
            return points;
        }

        public void PrintPoints(List<Point> points, int xSize, int ySize)
        {
            for (var y = -ySize - 2; y <= ySize + 2; y++)
            {
                for (int x = -xSize - 2; x < xSize + 2; x++)
                {
                    var p = points.FirstOrDefault(point => point.x == x && point.y == y) == null ? "." : "#" ;

                    Console.Write(p);
                }
                Console.Write("\n");
            }
        }
    }

    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int Vx { get; set; }
        public int Vy { get; set; }
    }

    public enum MovePointDirection
    {
        Forward,
        Backward
    }
}
