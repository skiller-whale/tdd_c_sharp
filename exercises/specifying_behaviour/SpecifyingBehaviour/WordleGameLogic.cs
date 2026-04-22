using System.Linq;
using System.Text.RegularExpressions;

namespace SpecifyingBehaviour;

public record GuessResult(bool Valid, string? Reason = null);

public static class GameStatus
{
    public static string GetGameStatus(string[] guesses, string target)
    {
        if (guesses.Length > 0 && guesses[^1] == target) return "won";
        if (guesses.Length >= 6) return "lost";
        return "in_progress";
    }
}

public static class GuessValidator
{
    public static GuessResult ValidateGuess(string word, string[] wordList)
    {
        if (word.Length != 5)
            return new GuessResult(false, "Guess must be 5 letters");
        if (!Regex.IsMatch(word, @"^[a-z]+$"))
            return new GuessResult(false, "Guess must only contain letters");
        if (!wordList.Contains(word))
            return new GuessResult(false, "Not a recognised word");
        return new GuessResult(true);
    }
}
