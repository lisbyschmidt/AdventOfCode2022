var input = File.ReadLines("input.txt").ToArray();
var part = Environment.GetEnvironmentVariable("part");
Console.WriteLine("C#");
Console.WriteLine(part == "part1" ? Solution1(input) : Solution2(input));

static int GetValue(char x) => x >= 'a' ? x - 'a' + 1 : x - 'A' + 27;

static int Solution1(string[] input) {
    return input.Select(line => {
        var length = line.Length / 2;
        var intersect = line[..length].Intersect(line[length..]).Single();
        return GetValue(intersect);
    }).Sum();
}

static int Solution2(string[] input) {
    var sum = 0;
    for (var i = 0; i < input.Length; i += 3) {
        var group = input[i..(i + 3)].Select(x => x.ToArray());
        var result = group.Aggregate((x, y) => x.Intersect(y).ToArray());
        sum += GetValue(result.Single());
    }
    return sum;
}