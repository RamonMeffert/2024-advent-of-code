using Internal;

// Read input
var reports = File.ReadLines("input");

// Initialize count
int count = 0;

// Loop over reports
foreach (var report in reports)
{
    // Parse the levels in the report
    var levels = report.Split(' ').Select(int.Parse).ToList();

    int[] signs = new int[levels.Count - 1];

    for (int i = 1; i < levels.Count; i++)
    {
        int diff = levels[i] - levels[i - 1];
        signs[i - 1] = Math.Sign(diff);
    }

    // We might be able to fix 1 problem
    int sum = Math.Abs(signs.Sum());

    if (sum == levels.Count - 1 || sum == levels.Count - 2)
    {
        Console.WriteLine($"{report} is fixable: {string.Join(',', signs)} -> {sum}");
    }
    else {
        Console.WriteLine($"{report} is not fixable: {string.Join(',', signs)} -> {sum}");
    }
}

// Write the result to stdout
Console.WriteLine(count.ToString());
