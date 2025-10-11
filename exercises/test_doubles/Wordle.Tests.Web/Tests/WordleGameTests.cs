using Microsoft.Playwright;
using Wordle.Core;
using Wordle.Tests.Web.Framework;
using System.Threading.Tasks;
using Xunit;

namespace Wordle.Tests.Web.Wordle;

public class WordleGameTests(RealServerTestFixture serverFixture) : WebTestBase(serverFixture)
{
    [Fact]
    public async Task WordleGame_shows_success_when_the_correct_answer_is_guessed_the_first_time()
    {
        // act
        await Browser.Visit(ServerFixture.WebBaseUrl);
        await Browser.ClickNewGameButton();
        await Browser.EnterGuess("whale");

        // assert
        Assert.Equal("You won!", await Browser.GetStatus());
    }

    [Fact]
    public async Task WordleGame_shows_failure_and_the_correct_answer_when_the_game_is_lost()
    {
        // act
        await Browser.Visit(ServerFixture.WebBaseUrl);
        await Browser.ClickNewGameButton();
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
        // act
        await Browser.Visit(ServerFixture.WebBaseUrl);
        await Browser.ClickNewGameButton();
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

    [Fact]
    public async Task WordleGame_shows_error_message_for_invalid_guesses()
    {
        // act
        await Browser.Visit(ServerFixture.WebBaseUrl);
        await Browser.ClickNewGameButton();
        await Browser.EnterGuess("abcde");

        // assert
        Assert.Equal("Guess is not in the dictionary.", await Browser.GetError());
    }

    [Fact]
    public async Task WordleGame_shows_colour_coded_feedback_for_previous_guesses()
    {
        // act
        await Browser.Visit(ServerFixture.WebBaseUrl);
        await Browser.ClickNewGameButton();
        await Browser.EnterGuess("water");

        // assert
        Assert.Equal("green", await Browser.GetGuessCharClass(0, 0));
        Assert.Equal("yellow", await Browser.GetGuessCharClass(0, 1));
        Assert.Equal("gray", await Browser.GetGuessCharClass(0, 2));
        Assert.Equal("yellow", await Browser.GetGuessCharClass(0, 3));
        Assert.Equal("gray", await Browser.GetGuessCharClass(0, 4));
    }
    
    [Fact]
    public async Task WordleGame_shows_new_game_button_when_the_game_is_over()
    {
        // act
        await Browser.Visit(ServerFixture.WebBaseUrl);
        await Browser.ClickNewGameButton();
        await Browser.EnterGuess("whale");

        // assert
        Assert.True(await Browser.IsNewGameButtonVisible());
    }
}
