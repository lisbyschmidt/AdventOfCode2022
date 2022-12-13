var grid = new Grid();

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => Dijkstra(grid, 'S', 'E', (from, to) => to.Elevation - from.Elevation <= 1),
    "part2" => Dijkstra(grid, 'E', 'a', (from, to) => from.Elevation - to.Elevation <= 1),
    _ => string.Empty
});

static int Dijkstra(Grid grid, char fromMark, char toMark, Func<Square, Square, bool> reachable) {
    var dist = new Dictionary<Square, int>();
    var prio = new PriorityQueue<Square, int>();
    var fromSquare = grid.Squares().First(s => s.Mark == fromMark);
    dist.Add(fromSquare, 0);
    prio.Enqueue(fromSquare, 0);
    while (prio.Count > 0) {
        var currentSquare = prio.Dequeue();
        if (currentSquare.Mark == toMark) return dist[currentSquare];
        foreach (var neighborSquare in grid.NeighborSquares(currentSquare).Where(s => reachable(currentSquare, s))) {
            var totalDistanceToNeighbor = dist[currentSquare] + 1;
            if (totalDistanceToNeighbor < dist.GetValueOrDefault(neighborSquare, int.MaxValue)) {
                dist.Add(neighborSquare, totalDistanceToNeighbor);
                prio.Enqueue(neighborSquare, totalDistanceToNeighbor);
            }
        }
    }
    return -1;
}

public record Square(int X, int Y, char Mark) {
    public int Elevation => Mark == 'S' ? 'a' : Mark == 'E' ? 'z' : Mark;
}

public class Grid {
    private readonly string[] _input = File.ReadAllLines("input.txt");

    public IEnumerable<Square> Squares() {
        for (var x = 0; x < _input.Length; x++)
        for (var y = 0; y < _input[x].Length; y++)
            yield return new Square(x, y, _input[x][y]);
    }

    public IEnumerable<Square> NeighborSquares(Square s) {
        if (InsideGrid(s.X, s.Y - 1)) yield return new Square(s.X, s.Y - 1, _input[s.X][s.Y - 1]);
        if (InsideGrid(s.X, s.Y + 1)) yield return new Square(s.X, s.Y + 1, _input[s.X][s.Y + 1]);
        if (InsideGrid(s.X - 1, s.Y)) yield return new Square(s.X - 1, s.Y, _input[s.X - 1][s.Y]);
        if (InsideGrid(s.X + 1, s.Y)) yield return new Square(s.X + 1, s.Y, _input[s.X + 1][s.Y]);
    }

    private bool InsideGrid(int x, int y) => x >= 0 && x < _input.Length && y >= 0 && y < _input[x].Length;
}