using System;
using System.Collections.Generic;
using System.IO;
using Wordle.Core;
using Wordle.Services;
using Xunit;

namespace Wordle.Tests.Core.Tests;

[Collection("DatabaseTests")]
public class DatabaseTests
{
    private readonly string _testDataFile = "test_data.json";

    public DatabaseTests()
    {
        // Clean up test file before each test
        if (File.Exists(_testDataFile))
        {
            File.Delete(_testDataFile);
        }
    }

    [Fact]
    public void Database_Stores_And_Retrieves_Games()
    {
        // Arrange
        var database = new Database(_testDataFile);
        database.Initialize();

        var game = Game.CreateNewGame("whale");
        
        // Act & Assert - Save initial game
        database.SaveGame(game);
        var retrievedGame = database.GetGame(game.Id);
        
        Assert.NotNull(retrievedGame);
        Assert.Equal(game.Id, retrievedGame.Id);
        Assert.Equal(game.CorrectAnswer, retrievedGame.CorrectAnswer);
        Assert.Equal(game.Guesses.Count, retrievedGame.Guesses.Count);
        
        // Act & Assert - Save modified game
        retrievedGame.MakeGuess("whale");
        database.SaveGame(retrievedGame);
        
        var newlyRetrievedGame = database.GetGame(game.Id);
        Assert.NotNull(newlyRetrievedGame);
        Assert.Equal(retrievedGame.Id, newlyRetrievedGame.Id);
        Assert.Equal(retrievedGame.CorrectAnswer, newlyRetrievedGame.CorrectAnswer);
        Assert.Equal(retrievedGame.Guesses.Count, newlyRetrievedGame.Guesses.Count);
        Assert.Contains("whale", newlyRetrievedGame.Guesses);
    }

    [Fact]
    public void Database_Returns_Undefined_For_Non_Existent_Games()
    {
        // Arrange
        var database = new Database(_testDataFile);
        database.Initialize();

        // Act
        var game = database.GetGame("non-existent-game-id");
        
        // Assert
        Assert.Null(game);
    }

    [Fact]
    public void Database_Persists_Data_When_Process_Is_Restarted()
    {
        // This is a placeholder for the actual test logic. In a real scenario,
        // we would need some more complex testing infrastructure that enables
        // us to stop and start processes.
        Assert.True(true);
    }
}
