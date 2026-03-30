using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace WordleStats;

/// <summary>
/// GameResult represents a single completed Wordle game.
/// </summary>
public record GameResult
{
    [JsonPropertyName("playerName")]
    public string PlayerName { get; init; } = string.Empty;

    [JsonPropertyName("answer")]
    public string Answer { get; init; } = string.Empty;

    [JsonPropertyName("guesses")]
    public List<string> Guesses { get; init; } = new();

    [JsonPropertyName("date")]
    public string Date { get; init; } = string.Empty;
}

/// <summary>
/// Result of validating a GameResult.
/// </summary>
public record ValidationResult
{
    public bool Valid { get; init; }
    public string? Reason { get; init; }

    public static ValidationResult Success() => new() { Valid = true };
    public static ValidationResult Failure(string reason) => new() { Valid = false, Reason = reason };
}

/// <summary>
/// Functions for working with GameResult objects.
/// </summary>
public static class GameResultExtensions
{
    private static readonly Regex WordPattern = new(@"^[a-z]{5}$", RegexOptions.Compiled);
    private static readonly Regex DatePattern = new(@"^\d{4}-\d{2}-\d{2}$", RegexOptions.Compiled);

    /// <summary>
    /// Validates that a value is a well-formed GameResult.
    ///
    /// A valid GameResult must have:
    /// - a non-empty string PlayerName
    /// - a 5-letter lowercase alphabetic Answer
    /// - Guesses as a non-empty array of 5-letter lowercase alphabetic strings
    /// - Date as a string matching YYYY-MM-DD
    /// - the game must be in a completed state (won or lost, i.e. last guess
    ///   matches answer or there are 6 guesses)
    ///
    /// Returns ValidationResult with Valid=true or Valid=false with a Reason.
    /// </summary>
    public static ValidationResult ValidateGameResult(GameResult? result)
    {
        if (result == null)
        {
            return ValidationResult.Failure("Result must be an object");
        }

        if (string.IsNullOrEmpty(result.PlayerName))
        {
            return ValidationResult.Failure("playerName must be a non-empty string");
        }

        if (!WordPattern.IsMatch(result.Answer))
        {
            return ValidationResult.Failure("answer must be a 5-letter lowercase word");
        }

        if (result.Guesses == null || result.Guesses.Count == 0)
        {
            return ValidationResult.Failure("guesses must be a non-empty array");
        }

        foreach (var guess in result.Guesses)
        {
            if (!WordPattern.IsMatch(guess))
            {
                return ValidationResult.Failure("Each guess must be a 5-letter lowercase word");
            }
        }

        if (!DatePattern.IsMatch(result.Date))
        {
            return ValidationResult.Failure("date must be a YYYY-MM-DD string");
        }

        var lastGuess = result.Guesses[^1];
        if (lastGuess != result.Answer && result.Guesses.Count < 6)
        {
            return ValidationResult.Failure("Game must be in a completed state (won or lost)");
        }

        return ValidationResult.Success();
    }

    /// <summary>
    /// Returns a one-line summary of a game result.
    ///
    /// Format: "PlayerName: ANSWER n/6 ✓" (for wins)
    ///     or: "PlayerName: ANSWER X/6 ✗" (for losses)
    ///
    /// The answer is displayed in uppercase.
    /// </summary>
    public static string SummarizeGame(GameResult result)
    {
        var answer = result.Answer.ToUpperInvariant();
        var isWin = result.Guesses[^1] == result.Answer;
        var status = isWin ? "✓" : "✗";
        var attempts = isWin ? result.Guesses.Count.ToString() : "X";
        return $"{result.PlayerName}: {answer} {attempts}/6 {status}";
    }
}
