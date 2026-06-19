using System;
using System.Collections.Generic;

namespace Sorting;

// Do not edit this class
public static class Sorter
{
    public static List<string> Sort(List<string> list)
    {
        var result = new List<string>(list);
        result.Sort((a, b) =>
        {
            try
            {
                return int.Parse(a).CompareTo(int.Parse(b));
            }
            catch (FormatException)
            {
                return 0;
            }
        });
        return result;
    }
}
