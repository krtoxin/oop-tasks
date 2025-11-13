using System;

namespace BubbleSorterLab;

class Program
{
    static void Main()
    {
        var numbers = new[] { 5, 1, 4, 2, 3, -1, 10, 4 };

    Console.WriteLine("Original: " + string.Join(", ", numbers));

    var ascSorter = new BubbleSorter((a, b) => a <= b);
    var asc = ascSorter.Sort(numbers);
    Console.WriteLine("Ascending: " + string.Join(", ", asc));

    var descSorter = new BubbleSorter((a, b) => a >= b);
    var desc = descSorter.Sort(numbers);
    Console.WriteLine("Descending: " + string.Join(", ", desc));

    Console.WriteLine("Original after sort (unchanged): " + string.Join(", ", numbers));

    var demo = new[] { 3, -2, 3, 0, 1 };
    Console.WriteLine("\nDemo input: " + string.Join(", ", demo));
    var sortedDemo = ascSorter.Sort(demo);
    Console.WriteLine("Demo ascending: " + string.Join(", ", sortedDemo));
    }
}
