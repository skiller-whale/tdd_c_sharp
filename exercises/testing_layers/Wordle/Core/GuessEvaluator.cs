using System.Collections.Generic;

namespace Wordle.Core;

public class GuessEvaluator
{
    private const string CorrectAnswer = "whale";

    public static List<string> EvaluateGuess(string guess)
    {
        var answerChars = CorrectAnswer.ToCharArray();
        var guessChars = guess.ToCharArray();

        // initialise the result assuming all letters are incorrect
        var result = new List<string> { "-", "-", "-", "-", "-" };

        // first pass: check for correct letters in correct positions
        for (int i = 0; i < 5; i++)
        {
            // TODO: if the letter is in the correct position, mark it as correct
            // TODO: mark the letter as accounted for
        }

        // second pass: check for correct letters in wrong positions
        for (int i = 0; i < 5; i++)
        {
            // TODO: if the letter is in the correct answer but not in the right place, mark it as such
            // TODO: _only_ mark the letter as such if it hasn't already been marked accounted for
            // TODO: mark the letter as accounted for
        }

        return result;
    }
}
