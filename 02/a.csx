using Internal;

// Read input
var reports = File.ReadLines("input");

// Initialize count
int count = 0;

// Loop over reports
foreach (var report in reports)
{
    // Parse the levels in the report
    var levels = new Queue<int>(report.Split(' ').Select(int.Parse));
    
    // Initialize some variables for later use
    int? sign = null;
    bool safe = true;

    while (levels.Count >= 2)
    {
        // Find the current and next levels
        int cur = levels.Dequeue();
        int nxt = levels.Peek();

        // Find the difference between the two levels and whether that is positive or negative
        int diff = nxt - cur;
        int s = Math.Sign(diff);

        // If the difference is greater than three, or the sign is different
        // from the previous pair, the report is unsafe.
        if (Math.Abs(diff) > 3 || (sign != null && sign != s))
        {
            safe = false;
            // At this point we can stop processing
            break;
        }

        // Save the previous sign so we can check if the next pair of levels has the same sign
        sign = s;
    }

    // If this report is safe, count it
    if (safe) count++;
}

// Write the result to stdout
Console.WriteLine(count.ToString());
