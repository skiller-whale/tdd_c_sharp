using WordleCheat;
using Xunit;

namespace WordleCheat.Tests.Tests;

public class WordleAppTests
{
    [Fact]
    public void RunReturnsEmptyListForNow()
    {
        var path = Path.Combine("TestFixtures", "good-words.json");
        var app = new WordleApp(path);

        var result = app.Run(["_____", "FTH", "ISYCA"]);

        Assert.Empty(result);
    }
}
