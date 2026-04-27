using WordleCheat;
using Xunit;

namespace WordleCheat.Tests.Tests;

public class WordleAppTests
{
    [Fact]
    public void RunReturnsEmptyListForNow()
    {
        var app = new WordleApp();

        var result = app.Run(["_____", "FTH", "ISYCA"]);

        Assert.Empty(result);
    }
}
