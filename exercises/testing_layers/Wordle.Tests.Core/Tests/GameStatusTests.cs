using FluentAssertions;
using System.Collections.Generic;
using Wordle.Core;
using Xunit;

namespace Wordle.Tests.Core.Tests;

[Collection("GameStatusTests")]
public class GameStatusTests
{
    [Fact]
    public void Returns_Playing_when_there_are_no_guesses()
    {
        var guesses = new List<string>();
        var status = GameStatus.GetGameStatus(guesses);
        Assert.Equal(GameStatus.Status.Playing, status);
    }

    [Fact]
    public void Returns_Playing_when_there_are_fewer_than_six_guesses()
    {
        var guesses = new List<string> { "fishy", "shark", "shell", "trout", "salty" };
        var status = GameStatus.GetGameStatus(guesses);
        Assert.Equal(GameStatus.Status.Playing, status);
    }

    [Fact]
    public void Returns_Won_when_the_last_guess_is_correct()
    {
        var guesses = new List<string> { "fishy", "shark", "shell", "whale" };
        var status = GameStatus.GetGameStatus(guesses);
        Assert.Equal(GameStatus.Status.Won, status);
    }

    [Fact]
    public void Returns_Lost_when_there_are_six_incorrect_guesses()
    {
        var guesses = new List<string> { "fishy", "shark", "shell", "trout", "salty", "ocean" };
        var status = GameStatus.GetGameStatus(guesses);
        Assert.Equal(GameStatus.Status.Lost, status);
    }
}
