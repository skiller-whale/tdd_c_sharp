using Xunit;

namespace WordleCheat.Tests.Tests;

public class ExampleTests
{
    [Fact]
    public void ComparesValuesAndCollections()
    {
        var values = new List<int> { 2, 4, 6 };
        Assert.Equal(6, values[2]);
        Assert.Equal(new List<int> { 2, 4, 6 }, values);
    }

    [Fact]
    public void ChecksContainmentAndLength()
    {
        var values = new List<int> { 2, 4, 6 };
        Assert.Contains(4, values);
        Assert.NotEmpty(values);
    }

    // Here's an example of how to get the path to a fixture file:
    [Fact]
    public void ReadsFixtureFile()
    {
        var path = Path.Combine("TestFixtures", "good-words.json");
        var json = File.ReadAllText(path);
        Assert.NotEmpty(json);
    }

    public class ErrorCases
    {
        [Fact]
        public void ChecksFailures()
        {
            Action throwsException = () => throw new ArgumentException("bad input");
            Assert.Throws<ArgumentException>(throwsException);
        }
    }
}
