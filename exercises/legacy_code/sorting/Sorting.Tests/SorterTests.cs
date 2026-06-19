using System.Collections.Generic;
using FluentAssertions;
using Sorting;
using Xunit;

namespace SortingTests;

public class SorterTests
{
    // given a pair of numeric strings, sorts the smaller one first
    [Fact]
    public void Given_A_Pair_Of_Numeric_Strings_Sorts_The_Smaller_One_First()
    {
        Sorter.Sort(new List<string> { "2", "1" }).Should().Equal("1", "2");
    }
}
