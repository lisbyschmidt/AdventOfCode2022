var tokens = File.ReadLines("input.txt").Where(l => l != "").Select(l => new ListToken(l) as Token).ToList();
Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => Part1(tokens), "part2" => Part2(tokens), _ => string.Empty
});

static int Part1(List<Token> tokens) {
    var rightOrderIndices = new List<int>();
    for (var i = 0; i < tokens.Count; i += 2)
        if (tokens[i].CompareTo(tokens[i + 1]) == -1)
            rightOrderIndices.Add(i / 2 + 1);
    return rightOrderIndices.Sum();
}

static int Part2(List<Token> tokens) {
    var dividers = new[] { new ListToken("[[2]]"), new ListToken("[[6]]") };
    tokens.AddRange(dividers);
    tokens.Sort();
    return (tokens.IndexOf(dividers[0]) + 1) * (tokens.IndexOf(dividers[1]) + 1);
}

public abstract class Token : IComparable<Token> {
    public abstract int CompareTo(Token? other);
}

public class ValueToken : Token {
    public required int Value { get; init; }

    public override int CompareTo(Token? other) => other switch {
        null => -1,
        ValueToken vt => this.Value.CompareTo(vt.Value),
        _ => other.CompareTo(this) * -1
    };
}

public class ListToken : Token {
    private readonly List<Token> _tokens = new();

    public ListToken(string input) {
        var childTokenInputs = ChildTokenInputs(input);
        foreach (var cti in childTokenInputs) {
            Token childToken = int.TryParse(cti, out var i) ? new ValueToken { Value = i } : new ListToken(cti);
            _tokens.Add(childToken);
        }
    }

    private static IEnumerable<string> ChildTokenInputs(string input) {
        var bracketCount = 0;
        var currentString = "";
        for (var i = 1; i < input.Length - 1; i++) { // input except outermost brackets
            if (input[i] == '[') bracketCount++;
            if (input[i] == ']') bracketCount--;
            if (input[i] == ',' && bracketCount == 0) {
                yield return currentString;
                currentString = "";
                continue;
            }
            currentString += input[i];
        }
        if (currentString != "")
            yield return currentString;
    }

    public override int CompareTo(Token? other) => other switch {
        null => -1,
        ListToken lt => CompareLists(this, lt),
        ValueToken vt => CompareLists(this, new ListToken($"[{vt.Value}]")),
        _ => other.CompareTo(this) * -1
    };

    private static int CompareLists(ListToken l, ListToken r) {
        for (var i = 0; i < l._tokens.Count && i < r._tokens.Count; i++) {
            var comparison = l._tokens[i].CompareTo(r._tokens[i]);
            if (comparison != 0) return comparison;
        }
        return l._tokens.Count.CompareTo(r._tokens.Count);
    }
}