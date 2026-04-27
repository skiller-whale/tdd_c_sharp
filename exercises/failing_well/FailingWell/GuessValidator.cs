using System.Linq;
using System.Text.RegularExpressions;

namespace FailingWell;

public record GuessResult(bool Valid, string? Reason = null);

public static class GuessValidator
{
    /// <summary>
    /// Validates a word before accepting it as a Wordle guess.
    /// </summary>
    /// <param name="word">The word to validate.</param>
    /// <param name="wordList">The list of valid 5-letter words.</param>
    /// <returns>A GuessResult indicating whether the guess is valid, and if not, why.</returns>
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
