using System;
using System.Linq;
using System.Collections.Generic;

namespace FailingWell;

public class WordleGame
{
    public string CorrectAnswer { get; }
    public string[] WordList { get; }
    public List<string> Guesses { get; } = new();
    public List<string> Evaluations { get; } = new();

    /// <summary>
    /// Creates a new game of Wordle.
    /// </summary>
    /// <param name="correctAnswer">The correct answer for this game of Wordle.</param>
    /// <param name="wordList">The list of valid words that can be guessed in this game.</param>
    /// <exception cref="ArgumentException">If the correct answer is not in the word list.</exception>
    public WordleGame(string correctAnswer, string[] wordList)
    {
        if (!wordList.Contains(correctAnswer))
            throw new ArgumentException("Answer must be in the word list");

        CorrectAnswer = correctAnswer;
        WordList = wordList;
    }

    /// <summary>
    /// Submits a guess for this game of Wordle. If the guess is valid, it will be evaluated and
    /// added to the game state. If the guess is invalid, an exception will be thrown.
    /// </summary>
    /// <param name="guess">The guess to submit.</param>
    /// <exception cref="ArgumentException">If the guess is invalid.</exception>
    public void SubmitGuess(string guess)
    {
        if ((Guesses.Count > 0 && Guesses[^1] == CorrectAnswer) || Guesses.Count >= 6)
            return;

        var validationResult = GuessValidator.ValidateGuess(guess, WordList);
        if (!validationResult.Valid)
            throw new ArgumentException(validationResult.Reason);

        var evaluation = GuessEvaluator.EvaluateGuess(guess, CorrectAnswer);
        Guesses.Add(guess);
        Evaluations.Add(evaluation);
    }
}
