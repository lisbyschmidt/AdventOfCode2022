using System.Text.RegularExpressions;

var input = File.ReadLines("input.txt").Select(x => Regex.Match(x, @"^(\d+)-(\d+),(\d+)-(\d+)$"))
    .Select(m => m.Groups.Values.Skip(1).Select(g => int.Parse(g.Value)).ToArray()).ToArray();

Console.WriteLine("C#\n" + (Environment.GetEnvironmentVariable("part") == "part1" ? Solution1(input) : Solution2(input)));

static int Solution1(int[][] input) => input.Count(i => i[0] <= i[2] && i[1] >= i[3] || i[2] <= i[0] && i[3] >= i[1]);

static int Solution2(int[][] input) => input.Count(i => i[0] <= i[3] && i[1] >= i[2]);