namespace WordleStats;

/// <summary>
/// Data for generating a player report.
/// </summary>
public record PlayerReportData
{
    public string PlayerId { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PlayerName { get; init; } = string.Empty;
    public int GamesPlayed { get; init; }
    public int GamesWon { get; init; }
    public double WinRate { get; init; }
    public double AverageAttempts { get; init; }
    public int FastestWin { get; init; }
    public string FirstPlayed { get; init; } = string.Empty;
    public string LastPlayed { get; init; } = string.Empty;
    public int Rank { get; init; }
    public int Percentile { get; init; }
    public List<string> Achievements { get; init; } = new();
}

/// <summary>
/// Functions for generating player reports.
/// </summary>
public static class Report
{
    /// <summary>
    /// Generates a multi-section text report for a player profile.
    ///
    /// The report includes these sections:
    ///   === PlayerName ===
    ///   ID: playerId
    ///   Email: email
    ///
    ///   --- Stats ---
    ///   Games Played: N
    ///   Games Won: N
    ///   Win Rate: N.N%
    ///
    ///   --- Performance ---
    ///   Average Attempts: N.N
    ///   Fastest Win: N guess(es)
    ///
    ///   --- Activity ---
    ///   First Played: YYYY-MM-DD
    ///   Last Played: YYYY-MM-DD
    ///
    ///   --- Ranking ---
    ///   Rank: #N
    ///   Percentile: Top N%
    ///
    ///   --- Achievements ---
    ///   🏆 Achievement 1
    ///   🏆 Achievement 2
    /// </summary>
    public static string GeneratePlayerReport(PlayerReportData data)
    {
        return $@"=== {data.PlayerName} ===
ID: {data.PlayerId}
Email: {data.Email}

--- Stats ---
Games Played: {data.GamesPlayed}
Games Won: {data.GamesWon}
Win Rate: {(data.WinRate * 100):F1}%

--- Performance ---
Average Attempts: {data.AverageAttempts:F1}
Fastest Win: {data.FastestWin} guess(es)

--- Activity ---
First Played: {data.FirstPlayed[..10]}
Last Played: {data.LastPlayed[..10]}

--- Ranking ---
Rank: #{data.Rank}
Percentile: Top {data.Percentile}%

--- Achievements ---
{FormatAchievements(data.Achievements)}".Trim();
    }

    /// <summary>
    /// Formats an array of achievement names into a display string.
    ///
    /// Returns "No achievements yet." for an empty array, or each
    /// achievement on its own line with a trophy emoji prefix.
    /// </summary>
    private static string FormatAchievements(List<string> achievements)
    {
        if (achievements.Count == 0)
        {
            return "No achievements yet.";
        }

        return string.Join("\n", achievements.Select(a => $"🏆 {a}"));
    }
}
