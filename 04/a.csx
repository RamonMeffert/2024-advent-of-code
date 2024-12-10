using Internal;
using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("input");
string target = "XMAS";
int n = target.Length;
int w = input[0].Length;
int h = input.Length;
Console.WriteLine($"{w}x{h}");
// int a = GetStraights(Id, Id);
// int b = GetStraights(Id, Inv(w));
// int c = GetStraights(Inv(h), Id);
// int d = GetStraights(Inv(h), Inv(w));

int e = GetDiagonals(Id, Id);
// int f = GetDiagonals(Id, Inv(w));
// int g = GetDiagonals(Inv(h), Id);
// int j = GetDiagonals(Inv(h), Inv(w));

// Console.WriteLine($"{a + c + d + e + f + g+ j}");

int GetStraights(Func<int, int> xOff, Func<int, int> yOff) {
    int sum = 0;

    for (int y = 0; y < h; y++) {
        StringBuilder b = new();
        for (int x = 0; x < w; x++) {
            b.Append(input[yOff(y)][xOff(x)]); 
        }
        // Console.WriteLine(b.ToString());
        sum += Regex.Matches(b.ToString(), target).Count;
    }
    
    return sum;
}

int GetDiagonals(Func<int, int> xOff, Func<int, int> yOff) {
    int sum = 0;

    // there are w + h - 1 diagonals
    int numDiags = w + h - 1;
    for (int d = 0; d < (w + h - 1); d++)
    {
        StringBuilder b = new();
        int numEntries = d + 1 - 2 * Math.Max(0, d + 1 - w);
        if (numEntries < n) continue;
        for (int i = 0; i < numEntries; i++) {
            int x = Math.Max(0, d - w + 1) + i;
            int y = Math.Min(d, w - 1) - i;
            Console.WriteLine($"{xOff(x)}, {yOff(y)}");
            b.Append(input[yOff(y)][xOff(x)]); 
        }
        sum += Regex.Matches(b.ToString(), target).Count;
    }

    return sum;
}

Func<int, int> Inv(int stat) => ((int var) => stat - 1 - var);
Func<int, int> Id => (int var) => var;
