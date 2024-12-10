using System.Collections.ObjectModel;
using Internal;

var lines = File.ReadLines("input");

LinkedList<int> ordering = new();
Collection<Collection<int>> updates = new();

bool switched = false;

foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line)) { switched = true; continue; }

    if (switched) {
        // reading updates
        updates.Add( new Collection<int>(line.Split(',').Select(int.Parse).ToList()));
    } else {
        // reading orderings
        var order = line.Split('|').Select(int.Parse).ToArray();
        int before = order[0];
        int after = order[1];
        
        // base case
        if (ordering.Count == 0) {
            ordering.AddFirst(before);
            ordering.AddLast(after);
        } else {
            var maybeAfter = ordering.Find(after);
            var maybeBefore = ordering.Find(before);
            var index = ordering.Index().ToDictionary(k => k.Item, v => v.Index);

            if (maybeAfter is not null && maybeBefore is not null) {
                // both already in the list - should we move them?

                // no
                if (index[before] < index[after]) continue;

                // yes: move. move before before after
                ordering.Remove(maybeBefore);
                ordering.AddBefore(maybeAfter, before);
            } else if (maybeAfter is null && maybeBefore is not null) {
                // only before is already in the list
                ordering.AddAfter(maybeBefore, after);
            } else if (maybeBefore is null && maybeAfter is not null) {
                // only after is already in the list
                ordering.AddBefore(maybeAfter, before);
            } else {
                // neither is in the list, add them at the end
                ordering.AddLast(before);
                ordering.AddLast(after);
            }
        }
    }
}

var correctOrder = ordering.Index().ToDictionary(k => k.Item, v => v.Index);

Console.WriteLine(string.Join(' ', correctOrder));

int sum = 0;

foreach (var update in updates) {
    var updateOrder = update.Select(page => correctOrder[page]);

    if (EqualOrder(update.Count, updateOrder, updateOrder.Order())) {

        int middle = update.Count / 2;
        for (int i = 0; i < update.Count; i++) {
            if (i == middle) {
                Console.Write($"<{ update[i] }>");
                sum += update[i];
            } else {
                Console.Write($" {update[i]} ");
            }
        }
        Console.WriteLine();
    }
}

Console.WriteLine($"Sum of middles: {sum}");

// assumes same length
bool EqualOrder(int len, IEnumerable<int> aa, IEnumerable<int> bb) {
    var a = aa.ToArray();
    var b = bb.ToArray();

    for (int i = 0; i < len; i++) {
        if (a[i] != b[i]) return false;
    }

    return true;
}