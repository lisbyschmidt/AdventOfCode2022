var input = File.ReadLines("input.txt").Select(x => x.Select(c => c - '0').ToArray()).ToArray();

var visibleTrees = new List<(int, int)>();
var treeScenicScores = new Dictionary<(int, int), int>();

for (var i = 0; i < input.Length; i++) {
    for (var j = 0; j < input[i].Length; j++) {
        var treeHeight = input[i][j];

        var left = input[i][..j];
        var right = input[i][(j + 1)..];
        var up = Enumerable.Range(0, input.Length).Where(row => row < i).Select(row => input[row][j]).ToArray();
        var down = Enumerable.Range(0, input.Length).Where(row => row > i).Select(row => input[row][j]).ToArray();

        if (Environment.GetEnvironmentVariable("part") == "part1" && (left.All(x => x < treeHeight) ||
            right.All(x => x < treeHeight) || up.All(x => x < treeHeight) || down.All(x => x < treeHeight))) {
            visibleTrees.Add((i, j));
            continue;
        }

        treeScenicScores[(i, j)] = 1;
        foreach (var direction in new[] { left.Reverse(), right, up.Reverse(), down }) {
            var count = 0;
            foreach (var tree in direction) {
                count++;
                if (tree >= treeHeight) break;
            }
            treeScenicScores[(i, j)] *= count;
        }
    }
}

Console.WriteLine("C#\n" + (visibleTrees.Any() ? visibleTrees.Count : treeScenicScores.Values.Max()));