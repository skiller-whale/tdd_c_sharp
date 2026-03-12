using Xunit;
using WordleStats;

namespace WordleStats.Tests;

public class GameResultTests
{
    [Fact]
    public void ValidateGameResult_AcceptsAValidGameResult()
    {
        var result = new GameResult
        {
            PlayerName = "Alice",
            Answer = "whale",
            Guesses = new List<string> { "crane", "slate", "whale" },
            Date = "2026-02-01"
        };

        var validation = GameResultExtensions.ValidateGameResult(result);

        Assert.True(validation.Valid);
    }

    [Fact]
    public void ValidateGameResult_RejectsNull()
    {
        var validation = GameResultExtensions.ValidateGameResult(null);

        Assert.False(validation.Valid);
        Assert.Equal("Result must be an object", validation.Reason);
    }

    [Fact]
    public void ValidateGameResult_RejectsResultWithMissingPlayerName()
    {
        var result = new GameResult
        {
            PlayerName = "",
            Answer = "whale",
            Guesses = new List<string> { "crane", "slate", "whale" },
            Date = "2026-02-01"
        };

        var validation = GameResultExtensions.ValidateGameResult(result);

        Assert.False(validation.Valid);
        Assert.Equal("playerName must be a non-empty string", validation.Reason);
    }

    [Fact]
    public void ValidateGameResult_RejectsResultWithMissingAnswer()
    {
        var result = new GameResult
        {
            PlayerName = "Alice",
            Answer = "",
            Guesses = new List<string> { "crane", "slate", "whale" },
            Date = "2026-02-01"
        };

        var validation = GameResultExtensions.ValidateGameResult(result);

        Assert.False(validation.Valid);
        Assert.Equal("answer must be a 5-letter lowercase word", validation.Reason);
    }

    [Theory]
    [InlineData("whales")]
    [InlineData("WHALE")]
    [InlineData("wh4le")]
    [InlineData("whal")]
    [InlineData("")]
    public void ValidateGameResult_RejectsResultWithAnswerOfWrongFormat(string invalidAnswer)
    {
        var result = new GameResult
        {
            PlayerName = "Alice",
            Answer = invalidAnswer,
            Guesses = new List<string> { "crane", "slate", "whale" },
            Date = "2026-02-01"
        };

        var validation = GameResultExtensions.ValidateGameResult(result);

        Assert.False(validation.Valid);
        Assert.Equal("answer must be a 5-letter lowercase word", validation.Reason);
    }

    [Fact]
    public void ValidateGameResult_RejectsResultWithMissingGuesses()
    {
        var result = new GameResult
        {
            PlayerName = "Alice",
            Answer = "whale",
            Guesses = new List<string>(),
            Date = "2026-02-01"
        };

        var validation = GameResultExtensions.ValidateGameResult(result);

        Assert.False(validation.Valid);
        Assert.Equal("guesses must be a non-empty array", validation.Reason);
    }

    [Fact]
    public void ValidateGameResult_RejectsResultWithInvalidDateFormat()
    {
        var result = new GameResult
        {
            PlayerName = "Alice",
            Answer = "whale",
            Guesses = new List<string> { "crane", "slate", "whale" },
            Date = "02-01-2026"
        };

        var validation = GameResultExtensions.ValidateGameResult(result);

        Assert.False(validation.Valid);
        Assert.Equal("date must be a YYYY-MM-DD string", validation.Reason);
    }

    [Fact]
    public void ValidateGameResult_RejectsResultThatIsNotInCompletedState()
    {
        var result = new GameResult
        {
            PlayerName = "Alice",
            Answer = "whale",
            Guesses = new List<string> { "crane", "slate" },
            Date = "2026-02-01"
        };

        var validation = GameResultExtensions.ValidateGameResult(result);

        Assert.False(validation.Valid);
        Assert.Equal("Game must be in a completed state (won or lost)", validation.Reason);
    }

    [Theory]
    [InlineData("Alice", new[] { "whale" }, "Alice: WHALE 1/6 ✓")]
    [InlineData("Bob", new[] { "crane", "slate", "flint" }, "Bob: FLINT 3/6 ✓")]
    [InlineData("Charlie", new[] { "stilt", "plumb", "vigor", "kayak", "monks", "crane" }, "Charlie: CRANE 6/6 ✓")]
    public void SummarizeGame_FormatsWinningGameCorrectly(string playerName, string[] guesses, string expected)
    {
        var answer = guesses[^1];
        var result = new GameResult
        {
            PlayerName = playerName,
            Answer = answer,
            Guesses = guesses.ToList(),
            Date = "2026-02-01"
        };

        var summary = GameResultExtensions.SummarizeGame(result);

        Assert.Equal(expected, summary);
    }

    [Theory]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("Charlie")]
    public void SummarizeGame_FormatsLosingGameCorrectly(string playerName)
    {
        var result = new GameResult
        {
            PlayerName = playerName,
            Answer = "whale",
            Guesses = new List<string> { "crane", "slate", "flame", "blame", "frame", "grape" },
            Date = "2026-02-01"
        };

        var summary = GameResultExtensions.SummarizeGame(result);

        Assert.Equal($"{playerName}: WHALE X/6 ✗", summary);
    }
}
