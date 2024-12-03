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
    int? sign = null;
    bool safe = true;
    bool ignored = false;
    int ig = 0;
    
    for (int i = 0; i < levels.Count - 2; i++)
    {
        int a = levels[i];
        int b = levels[i + 1];
        int c = levels[i + 2];

        Console.WriteLine($"{a}, {b}, {c}");

        (bool ab, int ab_s) = IsValid(a, b, sign);
        (bool bc, int bc_s) = IsValid(b, c, sign);

        if (ab && bc && ab_s == bc_s)
        {
            Console.WriteLine("hey");
            sign = ab_s;
            continue;
        }

        // We might be able to ignore the last level sometimes
        if (i == levels.Count - 3 && ab && !bc && !ignored)
        {
            break;
        }

        if (bc && !ignored)
        {
            Console.WriteLine("hi");
            sign = bc_s;
            ignored = true;
            ig = a;
            continue;
        }

        (bool ac, int ac_s) = IsValid(a, c, sign);
        if (ac && !ignored)
        {
            Console.WriteLine("ho");
            sign = ac_s;
            ignored = true;
            ig = b;
            levels[i + 1] = a;
            continue;
        }

        safe = false;
        break;
    }

    if (safe) {
        count++;
    }
    // else  if (ignored)
        Console.WriteLine($"{report,25} | safe: {(safe ? "Y" : "N")} | ignored: {ig}");
}

Console.WriteLine(count.ToString());

(bool, int) IsValid(int a, int b, int? p) {
    int s = Math.Sign(b - a),
        d = Math.Abs(b - a);
    bool safe = d <= 3 && s != 0 && (p == null || p == s);
    return (safe, s);
}