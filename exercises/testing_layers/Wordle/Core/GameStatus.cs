using System.Collections.Generic;

namespace Wordle.Core;

public class GameStatus
{
    private const int MaxAttempts = 6;
    private const string Answer = "whale";

    public enum Status
    {
        Won,
        Lost,
        Playing
    }

    public static Status GetGameStatus(List<string> guesses)
    {
        if (guesses.Contains(Answer))
        {
            return Status.Won;
        }
        else if (guesses.Count >= MaxAttempts)
        {
            return Status.Lost;
        }
        else
        {
            return Status.Playing;
        }
    }
}
