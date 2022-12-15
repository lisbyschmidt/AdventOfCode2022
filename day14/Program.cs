Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => Solution((rock, sand, maxY) => sq =>                !rock.Contains(sq) && !sand.Contains(sq)),
    "part2" => Solution((rock, sand, maxY) => sq => sq.Y < maxY && !rock.Contains(sq) && !sand.Contains(sq)),
    _ => string.Empty
});

static int Solution(Func<HashSet<Square>, HashSet<Square>, int, Func<Square, bool>> func) {
    var rockSquares = ParseInputToRockSquares(File.ReadLines("input.txt"));
    var maxY = rockSquares.Max(r => r.Y) + 2;

    var sandSquares = new HashSet<Square>();
    var source = new Square(500, 0);
    while (true) {
        var sand = source;
        var next = NextSquare(sand, func(rockSquares, sandSquares, maxY));
        while (next != default) {
            if (next.Y >= maxY) return sandSquares.Count; // Stop in case of infinite sand stream
            sand = next;
            next = NextSquare(sand, func(rockSquares, sandSquares, maxY));
        }

        sandSquares.Add(sand);
        if (sand == source) return sandSquares.Count; // Stop in case of blocked source
    }
}

static Square? NextSquare(Square s, Func<Square, bool> predicate) {
    var moves = new[] { s with { Y = s.Y + 1 }, new(s.X - 1, s.Y + 1), new(s.X + 1, s.Y + 1) };
    return moves.FirstOrDefault(predicate);
}

static HashSet<Square> ParseInputToRockSquares(IEnumerable<string> input) {
    var rockSquares = new HashSet<Square>();

    foreach (var line in input) {
        var rockPath = line.Split(" -> ").Select(xy => xy.Split(',').Select(int.Parse).ToArray())
            .Select(xy => new Square(xy[0], xy[1])).ToArray();

        for (var i = 1; i < rockPath.Length; i++) {
            var squares = SquaresBetweenTwoPoints(rockPath[i - 1], rockPath[i]);
            rockSquares.UnionWith(squares);
        }
    }

    return rockSquares;
}

static IEnumerable<Square> SquaresBetweenTwoPoints(Square from, Square to) {
    var ranges = new (int[], Func<int, Square>)[] {
        (Enumerable.Range(Math.Min(from.X, to.X), Math.Abs(to.X - from.X) + 1).ToArray(), x => new(x, from.Y)),
        (Enumerable.Range(Math.Min(from.Y, to.Y), Math.Abs(to.Y - from.Y) + 1).ToArray(), y => new(from.X, y))
    }.OrderByDescending(r => r.Item1.Length).ToArray();
    
    return ranges[0].Item1.Select(i => ranges[0].Item2(i));
}

public record Square(int X, int Y);