namespace WordleStats;

/// <summary>
/// Aggregate player statistics from arrays of GameResult objects.
/// </summary>
public record PlayerStatsData
{
    public int GamesPlayed { get; init; }
    public int GamesWon { get; init; }
    public double WinRate { get; init; }
    public double AverageAttempts { get; init; }
}

/// <summary>
/// Functions for calculating player statistics.
/// </summary>
public static class PlayerStats
{
    /// <summary>
    /// Computes aggregate player statistics from arrays of GameResult objects.
    /// </summary>
    public static PlayerStatsData CalculatePlayerStats(List<GameResult> gameResults)
    {
        if (gameResults.Count == 0)
        {
            return new PlayerStatsData
            {
                GamesPlayed = 0,
                GamesWon = 0,
                WinRate = 0,
                AverageAttempts = 0
            };
        }

        var gamesPlayed = gameResults.Count;
        var gamesWon = gameResults.Count(IsGameWon);
        var winRate = (double)gamesWon / gamesPlayed;
        var averageAttempts = CalculateAverageAttempts(gameResults);

        return new PlayerStatsData
        {
            GamesPlayed = gamesPlayed,
            GamesWon = gamesWon,
            WinRate = winRate,
            AverageAttempts = averageAttempts
        };
    }

    /// <summary>
    /// Calculates the average number of attempts for winning games only.
    /// Returns 0 if there are no wins.
    /// </summary>
    private static double CalculateAverageAttempts(List<GameResult> gameResults)
    {
        var winningGames = gameResults.Where(IsGameWon).ToList();
        if (winningGames.Count == 0)
        {
            return 0;
        }

        var totalAttempts = winningGames.Sum(r => r.Guesses.Count);
        return (double)totalAttempts / winningGames.Count;
    }

    /// <summary>
    /// Determines whether a game is won or lost based on the guesses and answer.
    /// </summary>
    private static bool IsGameWon(GameResult gameResult)
    {
        return gameResult.Guesses[^1] == gameResult.Answer;
    }
}
