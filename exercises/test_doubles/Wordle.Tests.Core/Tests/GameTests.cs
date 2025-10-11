using System.Collections.Generic;
using Wordle.Core;
using Xunit;

namespace Wordle.Tests.Core.Tests;

[Collection("GameTests")]
public class GameTests
{
    [Fact]
    public void Game_Returns_The_Correct_Game_Status()
    {
        var game1 = Game.CreateNewGame("whale");
        Assert.Equal(Status.Playing, game1.Status);

        var game2 = Game.CreateNewGame("whale");
        game2.MakeGuess("whale");
        Assert.Equal(Status.Won, game2.Status);

        var game3 = Game.CreateNewGame("whale");
        game3.MakeGuess("shark");
        game3.MakeGuess("shell");
        game3.MakeGuess("trout");
        game3.MakeGuess("salty");
        game3.MakeGuess("ocean");
        Assert.Equal(Status.Playing, game3.Status);

        var game4 = Game.CreateNewGame("whale");
        game4.MakeGuess("shark");
        game4.MakeGuess("shell");
        game4.MakeGuess("trout");
        game4.MakeGuess("salty");
        game4.MakeGuess("ocean");
        game4.MakeGuess("fishy");
        Assert.Equal(Status.Lost, game4.Status);
    }

    [Fact]
    public void Game_Rejects_Invalid_Guesses_With_An_Error_Message()
    {
        var game = Game.CreateNewGame("whale");

        game.MakeGuess("abc");
        Assert.Equal("Guesses must be 5 characters long.", game.Error);

        game.MakeGuess("abcdefg");
        Assert.Equal("Guesses must be 5 characters long.", game.Error);

        game.MakeGuess("abcde");
        Assert.Equal("Guess is not in the dictionary.", game.Error);
    }

    [Fact]
    public void Game_Adds_Valid_Guesses_To_The_Guesses_Array()
    {
        var game = Game.CreateNewGame("whale");

        game.MakeGuess("fishy");
        Assert.Equivalent(new[] { "fishy" }, game.Guesses);
        Assert.Null(game.Error);

        game.MakeGuess("shark");
        Assert.Equivalent(new[] { "fishy", "shark" }, game.Guesses);
        Assert.Null(game.Error);

        game.MakeGuess("shell");
        Assert.Equivalent(new[] { "fishy", "shark", "shell" }, game.Guesses);
        Assert.Null(game.Error);
    }

    [Fact]
    public void Game_Evaluates_Guesses_Correctly()
    {
        var game = Game.CreateNewGame("whale");

        game.MakeGuess("trout");
        Assert.Equivalent(new[] { "-", "-", "-", "-", "-" }, game.Evaluations[0]);

        game.MakeGuess("water");
        Assert.Equivalent(new[] { "+", "?", "-", "?", "-" }, game.Evaluations[1]);

        game.MakeGuess("shell");
        Assert.Equivalent(new[] { "-", "+", "?", "+", "-" }, game.Evaluations[2]);
    }
}
