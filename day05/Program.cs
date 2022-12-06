using System.Text.RegularExpressions;

var part = Environment.GetEnvironmentVariable("part");
var input = File.ReadLines("input.txt").ToArray();
var iBlankLine = Array.IndexOf(input, string.Empty);

var stacksInput = input.Take(iBlankLine - 1).Select(l =>
    l.ToArray().Where((_, i) => (i - 1) % 4 == 0).ToArray()).ToArray();
var stacks = new Stack<char>[stacksInput[0].Length];
for (var stack = 0; stack < stacks.Length; stack++)
{
    stacks[stack] = new Stack<char>();
    for (var line = stacksInput.Length - 1; line >= 0; line--)
    {
        if (stacksInput[line][stack] == ' ') break;
        stacks[stack].Push(stacksInput[line][stack]);
    }
}

var movesInput = input.Skip(iBlankLine + 1).Select(l =>
    Regex.Match(l, @"^move (\d+) from (\d+) to (\d+)$").Groups.Values.Skip(1)
        .Select(g => int.Parse(g.Value)).ToArray());
foreach (var move in movesInput)
{
    var moving = Enumerable.Range(0, move[0]).Select(_ => stacks[move[1] - 1].Pop());
    if (part == "part2") moving = moving.Reverse();
    foreach (var crate in moving)
        stacks[move[2] - 1].Push(crate);
}

Console.WriteLine("C#");
Console.WriteLine(stacks.Select(s => s.Peek()).ToArray());