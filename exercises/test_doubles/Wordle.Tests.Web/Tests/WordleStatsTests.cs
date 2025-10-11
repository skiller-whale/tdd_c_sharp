using Microsoft.Playwright;
using Wordle.Tests.Web.Framework;
using System.Threading.Tasks;
using Xunit;

namespace Wordle.Tests.Web.Tests;

public class WordleStatsTests(RealServerTestFixture serverFixture) : WebTestBase(serverFixture)
{
    [Fact]
    public async Task WordleStats_shows_stats_at_end_of_game()
    {
        // TODO
    }

    private async Task WinGame(TestBrowser browser)
    {
        await browser.Visit(ServerFixture.WebBaseUrl);
        await browser.ClickNewGameButton();
        await browser.EnterGuess("whale");
    }

    private async Task LoseGame(TestBrowser browser)
    {
        await browser.Visit(ServerFixture.WebBaseUrl);
        await browser.ClickNewGameButton();
        await browser.EnterGuess("fishy");
        await browser.EnterGuess("shark");
        await browser.EnterGuess("shell");
        await browser.EnterGuess("trout");
        await browser.EnterGuess("salty");
        await browser.EnterGuess("ocean");
    }
}
