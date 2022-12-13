var cycle = 0;
var cycleValue = new Dictionary<int, int> { [cycle] = 1 };
foreach (var line in File.ReadLines("input.txt")) {
    cycle++;
    cycleValue[cycle] = cycleValue[cycle - 1];
    if (line == "noop") continue;

    var value = int.Parse(line.Split().Last());
    cycle++;
    cycleValue[cycle] = cycleValue[cycle - 1] + value;
}

Console.WriteLine("C#");

if (Environment.GetEnvironmentVariable("part") == "part1") {
    var cycles = new[] { 20, 60, 100, 140, 180, 220 };
    Console.WriteLine(cycles.Select(c => c * cycleValue[c - 1]).Sum());
    return;
}

for (var c = 0; c < cycle; c++) {
    if (c % 40 == 0 && c > 0) Console.WriteLine();
    Console.Write(new[] { cycleValue[c] - 1, cycleValue[c], cycleValue[c] + 1 }.Contains(c % 40) ? "#" : ".");
}