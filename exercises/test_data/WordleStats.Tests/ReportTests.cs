using Xunit;
using WordleStats;

namespace WordleStats.Tests;

public class ReportTests
{
    [Fact]
    public void GeneratePlayerReport_IncludesPlayersDisplayNameAsHeader()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string>()
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("=== TestPlayer ===", report);
    }

    [Fact]
    public void GeneratePlayerReport_IncludesPlayersIdAndEmail()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string>()
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("ID: 123", report);
        Assert.Contains("Email: test@example.com", report);
    }

    [Fact]
    public void GeneratePlayerReport_IncludesStatsSectionWithGamesPlayedAndWinRate()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string>()
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("--- Stats ---", report);
        Assert.Contains("Games Played: 50", report);
        Assert.Contains("Games Won: 25", report);
        Assert.Contains("Win Rate: 50.0%", report);
    }

    [Fact]
    public void GeneratePlayerReport_IncludesPerformanceSectionWithAverageAttemptsAndFastestWin()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string>()
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("--- Performance ---", report);
        Assert.Contains("Average Attempts: 3.5", report);
        Assert.Contains("Fastest Win: 2 guess(es)", report);
    }

    [Fact]
    public void GeneratePlayerReport_IncludesActivitySectionWithFirstAndLastPlayedDates()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string>()
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("--- Activity ---", report);
        Assert.Contains("First Played: 2024-01-01", report);
        Assert.Contains("Last Played: 2024-06-01", report);
    }

    [Fact]
    public void GeneratePlayerReport_IncludesRankingSectionWithRankAndPercentile()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string>()
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("--- Ranking ---", report);
        Assert.Contains("Rank: #10", report);
        Assert.Contains("Percentile: Top 90%", report);
    }

    [Fact]
    public void GeneratePlayerReport_IncludesAchievementsSection()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string>()
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("--- Achievements ---", report);
        Assert.Contains("No achievements yet.", report);
    }

    [Fact]
    public void GeneratePlayerReport_FormatsEachAchievementWithTrophyEmoji()
    {
        var playerStats = new PlayerReportData
        {
            PlayerId = "123",
            Email = "test@example.com",
            PlayerName = "TestPlayer",
            GamesPlayed = 50,
            GamesWon = 25,
            WinRate = 0.5,
            AverageAttempts = 3.5,
            FastestWin = 2,
            FirstPlayed = "2024-01-01",
            LastPlayed = "2024-06-01",
            Rank = 10,
            Percentile = 90,
            Achievements = new List<string> { "First Win", "10-Game Streak" }
        };

        var report = Report.GeneratePlayerReport(playerStats);

        Assert.Contains("🏆 First Win", report);
        Assert.Contains("🏆 10-Game Streak", report);
    }
}
