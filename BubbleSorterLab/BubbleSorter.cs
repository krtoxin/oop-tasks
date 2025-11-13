using System;

namespace BubbleSorterLab;

public delegate bool Compare(int a, int b);

public class BubbleSorter
{
    private readonly Compare _compare;

    public BubbleSorter(Compare compare)
    {
        _compare = compare ?? throw new ArgumentNullException(nameof(compare));
    }

    public int[] Sort(int[] items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));
        var arr = (int[])items.Clone();
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (!_compare(arr[j], arr[j + 1]))
                {
                    var tmp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
        return arr;
    }
}
