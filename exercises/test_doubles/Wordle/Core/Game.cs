using System;
using System.Collections.Generic;
using System.Linq;

namespace Wordle.Core;

#nullable enable

public enum Status
{
    Won,
    Lost,
    Playing
}

public class Game(string id, string CorrectAnswer, List<string> Guesses, string? Error)
{
    public string Id { get; } = id;
    public string CorrectAnswer { get; } = CorrectAnswer;
    public List<string> Guesses { get; } = Guesses;
    public string? Error { get; set; } = Error;
    public List<List<string>> Evaluations => GetEvaluations();
    public Status Status => GetStatus();

    public static Game CreateNewGame(string correctAnswer)
    {
        var id = Guid.NewGuid().ToString();
        return new Game(id, correctAnswer, new List<string>(), null);
    }

    public void MakeGuess(string guess)
    {
        var validationResult = ValidateGuess(guess);

        if (validationResult != null)
        {
            Error = validationResult;
            return;
        }

        Error = null;
        Guesses.Add(guess);
    }

    private Status GetStatus()
    {
        const int MaxAttempts = 6;

        if (Guesses.LastOrDefault() == CorrectAnswer)
        {
            return Status.Won;
        }
        else if (Guesses.Count >= MaxAttempts)
        {
            return Status.Lost;
        }
        else
        {
            return Status.Playing;
        }
    }

    private static string? ValidateGuess(string guess)
    {
        if (string.IsNullOrEmpty(guess))
        {
            return "Guess cannot be empty.";
        }
        
        if (guess.Length != 5)
        {
            return "Guesses must be 5 characters long.";
        }
        
        if (!Dictionary.IsValidWord(guess))
        {
            return "Guess is not in the dictionary.";
        }

        return null;
    }

    private List<List<string>> GetEvaluations()
    {
        return [.. Guesses.Select(EvaluateGuess)];
    }

    private List<string> EvaluateGuess(string guess)
    {
        var answerChars = CorrectAnswer.ToCharArray();
        var guessChars = guess.ToCharArray();
        var accountedFor = new bool[5];

        // initialise the result assuming all letters are incorrect
        var result = new List<string> { "-", "-", "-", "-", "-" };

        // first pass: check for correct letters in correct positions
        for (int i = 0; i < 5; i++)
        {
            if (guessChars[i] == answerChars[i])
            {
                result[i] = "+";
                accountedFor[i] = true;
            }
        }

        // second pass: check for correct letters in wrong positions
        for (int i = 0; i < 5; i++)
        {
            if (result[i] == "-") // Skip letters already marked as correct
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!accountedFor[j] && guessChars[i] == answerChars[j])
                    {
                        result[i] = "?";
                        accountedFor[j] = true;
                        break;
                    }
                }
            }
        }

        return result;
    }
}
