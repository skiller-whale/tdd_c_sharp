using System;

namespace FailingWell;

public static class GuessEvaluator
{
    /// <summary>
    /// Determines which letters in a Wordle guess are correct (green), present but in the wrong
    /// place (yellow), or not present at all (grey).
    /// </summary>
    /// <param name="guess">The guessed word.</param>
    /// <param name="correctAnswer">The correct word.</param>
    /// <returns>A string representing the evaluation of each letter: 'g' for green, 'y' for yellow, '-' for grey.</returns>
    public static string EvaluateGuess(string guess, string correctAnswer)
    {
        var evaluationArray = new char[] { '-', '-', '-', '-', '-' };
        var remainingArray = correctAnswer.ToCharArray();

        // check for greens
        for (int i = 0; i < 5; i++)
        {
            if (guess[i] == correctAnswer[i])
            {
                evaluationArray[i] = 'g';
                remainingArray[i] = '\0';
            }
        }

        // check for yellows
        for (int i = 0; i < 5; i++)
        {
            if (guess[i] != correctAnswer[i])
            {
                var remainingIndex = Array.IndexOf(remainingArray, guess[i]);
                if (remainingIndex >= 0)
                {
                    evaluationArray[i] = 'y';
                    remainingArray[remainingIndex] = '\0';
                }
            }
        }

        return new string(evaluationArray);
    }
}
