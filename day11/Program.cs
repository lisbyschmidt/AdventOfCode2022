var relief = (long x) => new Item(x / 3);
var rounds = 20;

if (Environment.GetEnvironmentVariable("part") == "part2") {
    const int modulus = 13 * 3 * 7 * 2 * 19 * 5 * 11 * 17;
    relief = x => new Item(x % modulus);
    rounds = 10000;
}

var monkeys = new List<Monkey>();
monkeys.Add(new(old => relief(old * 3), Next(13, 6, 2), 89, 73, 66, 57, 64, 80));
monkeys.Add(new(old => relief(old + 1), Next(3, 7, 4), 83, 78, 81, 55, 81, 59, 69));
monkeys.Add(new(old => relief(old * 13), Next(7, 1, 4), 76, 91, 58, 85));
monkeys.Add(new(old => relief(old * old), Next(2, 6, 0), 71, 72, 74, 76, 68));
monkeys.Add(new(old => relief(old + 7), Next(19, 5, 7), 98, 85, 84));
monkeys.Add(new(old => relief(old + 8), Next(5, 3, 0), 78));
monkeys.Add(new(old => relief(old + 4), Next(11, 1, 2), 86, 70, 60, 88, 88, 78, 74, 83));
monkeys.Add(new(old => relief(old + 5), Next(17, 3, 5), 81, 58));

Func<Item, Monkey> Next(int div, int yes, int no) => wl => wl.WorryLevel % div == 0 ? monkeys[yes] : monkeys[no];

for (var round = 0; round < rounds; round++)
    foreach (var monkey in monkeys)
        monkey.InspectItems();

Console.WriteLine("C#\n" + monkeys.Select(m => m.InspectCount).OrderDescending().Take(2).Aggregate((i, j) => i * j));

public class Monkey {
    private readonly Func<Item, Item> _operation;
    private readonly Func<Item, Monkey> _nextMonkey;
    private readonly Queue<Item> _items;

    public Monkey(Func<Item, Item> operation, Func<Item, Monkey> nextMonkey, params int[] items) {
        _operation = operation;
        _nextMonkey = nextMonkey;
        _items = new Queue<Item>(items.Select(wl => new Item(wl)));
    }

    public long InspectCount { get; private set; }

    public void InspectItems() {
        while (_items.Count > 0) {
            var updatedItem = _operation(_items.Dequeue());
            _nextMonkey(updatedItem)._items.Enqueue(updatedItem);
            InspectCount++;
        }
    }
}

public record Item(long WorryLevel) { public static implicit operator long(Item i) => i.WorryLevel; }