using Internal;
using System.Text.RegularExpressions;

string input = File.ReadAllText("input");
input.ReplaceLineEndings("");

// The next switch will be a don't
string splitter = "don't()";

int sum = 0;

while (true)
{
    // Find the index of the next do() or don't()
    int nextSplit = input.IndexOf(splitter);
 
    // If there's a do or don't left, take the input up to the index we found above
    // If not, take the remaining input
    string before = nextSplit > 0 ? input[..nextSplit] : input;

    // If we're splitting on don't, we should be interpreting what's in "before".
    if (splitter == "don't()")
    {
        // Enable these lines for debug output
        // Console.WriteLine("Including:");
        // Debug(before);
        
        // Find the multiplication functions
		var matches = Regex.Matches(before, @"mul\(([0-9]+),([0-9]+)\)");
        
        // Calculate the multiplications and add them to the total sum
        foreach (Match match in matches)
        {
            int a = int.Parse(match.Groups[1].Value);
            int b = int.Parse(match.Groups[2].Value);

            sum += a * b;

            // Enable this line for debug output
            // Console.WriteLine($"  {a} * {b} = {a * b} => {sum}");
        }
    } else {
        // Enable these lines for debug output
        // Console.WriteLine("Ignoring:");
        // Debug(before);
    }

    // If the nextsplit is negative, we should stop - there's no more input left to process
    if (nextSplit <= 0) { break; }

    // Remove the part we've processed from the input
    input = input[nextSplit..];

    // Switch the splitter
    splitter = splitter == "don't()" ? "do()" : "don't()";
}

// Print the result to stdout
Console.WriteLine(sum.ToString());

// Print a pretty version of an input string to the console for debugging
// do(), don't() and mul( are colorized in different colors and lines are
// kinda split at 72 characters
void Debug(string input)
{
    var chunks = input
        .Replace("do()", "\x1b[92mdo()\x1b[39m") // green
        .Replace("don't()", "\x1b[91mdon't()\x1b[39m") // red
        .Replace("mul(", "\x1b[95mmul(\x1b[39m") // magenta
        .Chunk(72)
        .Select(c => "    " + new string(c.ToArray()));
    Console.WriteLine(string.Join('\n', chunks));
}