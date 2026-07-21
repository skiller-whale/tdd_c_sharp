using System.Collections.Generic;
using CsCheck;
using MergeSort;
using Xunit;

namespace MergeSortTests;

public class MergerProperties
{
    // A generator for already-sorted lists of small integers, so we always feed
    // Merger valid (sorted) input.
    private static readonly Gen<List<int>> SortedList =
        Gen.Int[0, 100].List.Select(Sorted);

    // Property: The merged output should always be in non-decreasing order.
    //
    // This currently passes — note that a buggy merge can still produce sorted
    // output, so this property alone is not enough.
    [Fact]
    public void MergedOutputIsSorted()
    {
        Gen.Select(SortedList, SortedList).Sample((a, b) =>
        {
            var merged = Merger.Merge(a, b);

            for (int k = 1; k < merged.Count; k++)
            {
                Assert.True(merged[k - 1] <= merged[k],
                    $"expected sorted output but found [{string.Join(", ", merged)}]");
            }
        });
    }

    // TODO: the merged list should contain every element from both inputs, so its
    // count should equal a.Count + b.Count. Add an assertion below, then run
    // `dotnet test`.
    [Fact]
    public void MergePreservesLength()
    {
        Gen.Select(SortedList, SortedList).Sample((a, b) =>
        {
            var merged = Merger.Merge(a, b);

            // TODO: assert something about merged.Count
        });
    }

    // TODO: the merged list should contain exactly the same elements as the two
    // inputs combined. Sorting both sides and comparing is one easy way to check
    // this. Add an assertion and run the tests.
    [Fact]
    public void MergePreservesAllElements()
    {
        Gen.Select(SortedList, SortedList).Sample((a, b) =>
        {
            var merged = Merger.Merge(a, b);

            // TODO: assert merged holds the same elements as a and b combined
        });
    }

    // Helper: returns a sorted copy of the given list.
    private static List<int> Sorted(List<int> list)
    {
        var copy = new List<int>(list);
        copy.Sort();
        return copy;
    }
}
