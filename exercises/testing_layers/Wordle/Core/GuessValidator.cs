using System.Collections.Generic;

namespace Wordle.Core;

#nullable enable

public class GuessValidator
{
    public class ValidationResult
    {
        public bool Valid { get; set; }
        public string? Reason { get; set; }
    }

    public static void ValidateGuess(string guess)
    {
        // TODO
    }
}
