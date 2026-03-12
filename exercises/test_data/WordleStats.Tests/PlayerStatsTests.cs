using Xunit;
using WordleStats;

namespace WordleStats.Tests;

public class PlayerStatsTests
{
    [Fact]
    public void CalculatePlayerStats_Returns0sForEmptyArray()
    {
        var stats = PlayerStats.CalculatePlayerStats(new List<GameResult>());

        Assert.Equal(0, stats.GamesPlayed);
        Assert.Equal(0, stats.GamesWon);
    }

    [Fact]
    public void CalculatePlayerStats_CountsGamesPlayedAndWonCorrectly()
    {
        var games = new List<GameResult>
        {
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "whale",
                Guesses = new List<string> { "whale" },
                Date = "2026-02-01"
            },
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "crane",
                Guesses = new List<string> { "stilt", "plumb", "vigor", "kayak", "monks", "brand" },
                Date = "2026-02-02"
            }
        };

        var stats = PlayerStats.CalculatePlayerStats(games);

        Assert.Equal(2, stats.GamesPlayed);
        Assert.Equal(1, stats.GamesWon);
    }

    [Fact]
    public void CalculatePlayerStats_Returns1WhenAllGamesAreWon()
    {
        var games = new List<GameResult>
        {
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "whale",
                Guesses = new List<string> { "whale" },
                Date = "2026-02-01"
            },
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "crane",
                Guesses = new List<string> { "crane" },
                Date = "2026-02-02"
            }
        };

        var stats = PlayerStats.CalculatePlayerStats(games);

        Assert.Equal(1, stats.WinRate);
    }

    [Fact]
    public void CalculatePlayerStats_ReturnsCorrectRatioForMixOfWinsAndLosses()
    {
        var games = new List<GameResult>
        {
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "whale",
                Guesses = new List<string> { "whale" },
                Date = "2026-02-01"
            },
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "crane",
                Guesses = new List<string> { "stilt", "plumb", "vigor", "kayak", "monks", "brand" },
                Date = "2026-02-02"
            },
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "flint",
                Guesses = new List<string> { "flint" },
                Date = "2026-02-03"
            }
        };

        var stats = PlayerStats.CalculatePlayerStats(games);

        Assert.Equal(2.0 / 3.0, stats.WinRate, precision: 5);
    }

    [Fact]
    public void CalculatePlayerStats_Returns0AverageAttemptsWhenThereAreNoWins()
    {
        var games = new List<GameResult>
        {
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "whale",
                Guesses = new List<string> { "stilt", "plumb", "vigor", "kayak", "monks", "brand" },
                Date = "2026-02-01"
            }
        };

        var stats = PlayerStats.CalculatePlayerStats(games);

        Assert.Equal(0, stats.AverageAttempts);
    }

    [Fact]
    public void CalculatePlayerStats_CalculatesMeanAttemptsForWinningGames()
    {
        var games = new List<GameResult>
        {
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "whale",
                Guesses = new List<string> { "crane", "slate", "whale" },
                Date = "2026-02-01"
            },
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "crane",
                Guesses = new List<string> { "stilt", "plumb", "vigor", "kayak", "monks", "brand" },
                Date = "2026-02-02"
            },
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "flint",
                Guesses = new List<string> { "flint" },
                Date = "2026-02-03"
            }
        };

        var stats = PlayerStats.CalculatePlayerStats(games);

        Assert.Equal((3 + 1) / 2.0, stats.AverageAttempts);
    }

    [Fact]
    public void CalculatePlayerStats_IgnoresLostGamesWhenCalculatingAverage()
    {
        var games = new List<GameResult>
        {
            new GameResult
            {
                PlayerName = "Alice",
                Answer = "whale",
                Guesses = new List<string> { "stilt", "plumb", "vigor", "kayak", "monks", "brand" },
                Date = "2026-02-01"
            }
        };

        var stats = PlayerStats.CalculatePlayerStats(games);

        Assert.Equal(0, stats.AverageAttempts);
    }
}
