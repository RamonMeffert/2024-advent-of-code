using System.Collections.ObjectModel;
using Internal;

var input = await File.ReadAllLinesAsync(Args[0]);

var w = input[0].Length;

// don't bother converting "0" (str) to 0 (int), just use ascii values
// we only care about the differences
byte[][] terrain = input.Select(s => s.ToList()
        .Select(c => (byte)c)
        .ToArray())
    .ToArray();

var trailheads = terrain.SelectMany(t => t).ToList()
    .Aggregate((index: 0, indices: new HashSet<int>()), 
        (ix, v) => {
            if (v == (byte)'0') {
                ix.indices.Add(ix.index);
            }
            return (++ix.index, ix.indices);
        }).indices;

// debug: 2d (x,y) indexes of starts
Console.WriteLine(string.Join('\n', trailheads.Select(i => (i % w, i / w))));