using System.Text.RegularExpressions;

var input = File.ReadLines("input.txt");
var currentDirs = new Stack<string>();
var dirSizes = new Dictionary<string, int>();
foreach (var line in input) {
    var match = Regex.Match(line, @"^\$ cd (.+)$");
    if (match.Success) {
        var newDir = Path.Combine(currentDirs.FirstOrDefault(""), match.Groups[1].Value);
        if (newDir.EndsWith(".."))
            currentDirs.Pop();
        else {
            currentDirs.Push(newDir);
            dirSizes[newDir] = 0;
        }
        continue;
    }

    match = Regex.Match(line, @"^(\d+) (.+)$");
    if (match.Success) {
        var fileSize = int.Parse(match.Groups[1].Value);
        foreach (var currentDir in currentDirs)
            dirSizes[currentDir] += fileSize;
    }
}

Console.WriteLine("C#");
Console.WriteLine(Environment.GetEnvironmentVariable("part") == "part1"
    ? dirSizes.Values.Where(x => x <= 100000).Sum()
    : dirSizes.Values.Where(x => x >= 30000000 - (70000000 - dirSizes.First().Value)).Min());