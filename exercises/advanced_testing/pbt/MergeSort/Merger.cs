using System.Collections.Generic;

namespace MergeSort;

// Merges two already-sorted lists of integers into a single sorted list.
public static class Merger
{
    public static List<int> Merge(List<int> a, List<int> b)
    {
        var result = new List<int>();
        int i = 0;
        int j = 0;
        while (i < a.Count && j < b.Count)
        {
            if (a[i] <= b[j])
            {
                result.Add(a[i]);
                i++;
            }
            else
            {
                result.Add(b[j]);
                j++;
            }
        }
        return result;
    }
}
