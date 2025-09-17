using FluentAssertions;
using Microsoft.Playwright;
using Wordle.Tests.E2E.Framework;
using Xunit;

namespace Wordle.Tests.E2E.Tests;

public class WordleGameTests : E2ETestBase
{
    public WordleGameTests(RealServerTestFixture serverFixture) : base(serverFixture)
    {
    }

    [Fact]
    public async Task WordleGame_shows_success_when_the_correct_answer_is_guessed_the_first_time()
    {
        // arrange
        await Browser.Visit(ServerFixture.WebBaseUrl);

        // act
        await Browser.EnterGuess("whale");

        // assert
        Assert.Equal("You won!", await Browser.GetStatus());
    }

    [Fact]
    public async Task WordleGame_shows_failure_and_the_correct_answer_when_the_game_is_lost()
    {
        // arrange
        await Browser.Visit(ServerFixture.WebBaseUrl);

        // act
        await Browser.EnterGuess("fishy");
        await Browser.EnterGuess("shark");
        await Browser.EnterGuess("shell");
        await Browser.EnterGuess("trout");
        await Browser.EnterGuess("salty");
        await Browser.EnterGuess("ocean");

        // assert
        Assert.Equal("You lost!", await Browser.GetStatus());
        Assert.Equal("WHALE", await Browser.GetCorrectAnswer());
    }

    [Fact]
    public async Task WordleGame_shows_the_previous_guesses_in_the_game()
    {
        // arrange
        await Browser.Visit(ServerFixture.WebBaseUrl);

        // act
        await Browser.EnterGuess("fishy");
        await Browser.EnterGuess("shark");
        await Browser.EnterGuess("shell");
        await Browser.EnterGuess("trout");
        await Browser.EnterGuess("salty");
        await Browser.EnterGuess("ocean");

        // assert
        Assert.Equal("FISHY", await Browser.GetGuess(0));
        Assert.Equal("SHARK", await Browser.GetGuess(1));
        Assert.Equal("SHELL", await Browser.GetGuess(2));
        Assert.Equal("TROUT", await Browser.GetGuess(3));
        Assert.Equal("SALTY", await Browser.GetGuess(4));
        Assert.Equal("OCEAN", await Browser.GetGuess(5));
    }

    [Fact(Skip = "TODO")]
    public async Task WordleGame_shows_error_message_for_invalid_guesses()
    {
        // arrange
        await Browser.Visit(ServerFixture.WebBaseUrl);

        // act

        // assert
        // NOTE: you can use `await Browser.GetError()` to get the text content of
        // the first element on the page with the class "error"
    }

    [Fact(Skip = "TODO")]
    public async Task WordleGame_shows_colour_coded_feedback_for_previous_guesses()
    {
        // arrange
        await Browser.Visit(ServerFixture.WebBaseUrl);

        // act
        // a useful guess to try here would be "water", which should be reported as:
        //   - green for the first character (W)
        //   - yellow for the second character (A)
        //   - gray for the third character (T)
        //   - yellow for the fourth character (E)
        //   - gray for the fifth character (R)

        // assert
        // NOTE: you can use `await Browser.GetGuessCharClass(n, m)` to get the class
        // of the mth character of the nth guess - and then check this is either
        // "green", "yellow", or "gray" (as appropriate)
    }
}
