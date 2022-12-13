var input = File.ReadLines("input.txt").Single();

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => Solution(4, input), "part2" => Solution(14, input), _ => string.Empty
});

static int Solution(int distinctChars, string input) {
    for (var i = distinctChars; i < input.Length; i++) {
        var slice = input[(i - distinctChars)..i];
        if (slice.Distinct().Count() == distinctChars)
            return i;
    }
    return -1;
}