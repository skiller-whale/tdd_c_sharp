using System.Text.Json;
using WordleStats;

namespace WordleStats.Cli;

public static class Commands
{
    private static List<GameResult>? _gameResults;

    private static List<GameResult> GameResults
    {
        get
        {
            if (_gameResults == null)
            {
                // Look for data in /app/data (Docker) or relative to base directory (local)
                var containerDataPath = "/app/data/sample-results.json";
                var localDataPath = Path.Combine(AppContext.BaseDirectory, "data", "sample-results.json");
                var dataPath = File.Exists(containerDataPath) ? containerDataPath : localDataPath;
                
                var json = File.ReadAllText(dataPath);
                _gameResults = JsonSerializer.Deserialize<List<GameResult>>(json) ?? new List<GameResult>();
            }
            return _gameResults;
        }
    }

    public static List<string> GetPlayerNames()
    {
        return GameResults.Select(r => r.PlayerName).Distinct().ToList();
    }

    public static List<GameResult> GetPlayerGames(string playerName)
    {
        return GameResults.Where(r => r.PlayerName == playerName).ToList();
    }

    public static void ListGames()
    {
        Console.WriteLine("\n=== All Game Results ===\n");
        var maxIndexWidth = GameResults.Count.ToString().Length;
        for (int i = 0; i < GameResults.Count; i++)
        {
            var result = GameResults[i];
            var index = (i + 1).ToString().PadLeft(maxIndexWidth);
            Console.WriteLine($"{index}. {GameResultExtensions.SummarizeGame(result)} [{result.Date}]");
        }
        Console.WriteLine($"\nTotal: {GameResults.Count} games\n");
    }

    public static void ShowPlayerStats(string playerName)
    {
        var playerGames = GetPlayerGames(playerName);
        if (playerGames.Count == 0)
        {
            Console.WriteLine($"\nNo games found for player: {playerName}\n");
            return;
        }

        var stats = PlayerStats.CalculatePlayerStats(playerGames);
        Console.WriteLine($"\n=== Stats for {playerName} ===\n");
        Console.WriteLine($"Games Played: {stats.GamesPlayed}");
        Console.WriteLine($"Games Won: {stats.GamesWon}");
        Console.WriteLine($"Win Rate: {(stats.WinRate * 100):F1}%");
        Console.WriteLine($"Average Attempts: {stats.AverageAttempts:F1}\n");
    }

    public static void ShowPlayerReport(string playerName)
    {
        var playerGames = GetPlayerGames(playerName);
        if (playerGames.Count == 0)
        {
            Console.WriteLine($"\nNo games found for player: {playerName}\n");
            return;
        }

        var stats = PlayerStats.CalculatePlayerStats(playerGames);
        
        // Calculate rankings
        var allPlayerStats = GetPlayerNames()
            .Select(name => (PlayerName: name, Stats: PlayerStats.CalculatePlayerStats(GetPlayerGames(name))))
            .ToList();
        var ranked = RankPlayers(allPlayerStats);
        var playerRank = ranked.First(p => p.PlayerName == playerName);

        // Calculate additional stats
        var wins = playerGames.Where(g => g.Guesses[^1] == g.Answer).ToList();
        var fastestWin = wins.Count > 0 ? wins.Min(g => g.Guesses.Count) : 0;
        var dates = playerGames.Select(g => g.Date).OrderBy(d => d).ToList();
        var percentile = (int)Math.Round((double)playerRank.Rank / ranked.Count * 100);

        // Generate sample achievements
        var achievements = new List<string>();
        if (stats.WinRate == 1.0) achievements.Add("Perfect Record");
        if (fastestWin == 1) achievements.Add("Hole-in-One");
        if (stats.GamesPlayed >= 5) achievements.Add("Dedicated Player");
        if (stats.AverageAttempts < 3) achievements.Add("Speed Solver");

        var report = Report.GeneratePlayerReport(new PlayerReportData
        {
            PlayerId = $"player-{playerName.ToLower()}",
            Email = $"{playerName.ToLower()}@example.com",
            PlayerName = playerName,
            GamesPlayed = stats.GamesPlayed,
            GamesWon = stats.GamesWon,
            WinRate = stats.WinRate,
            AverageAttempts = stats.AverageAttempts,
            FastestWin = fastestWin,
            FirstPlayed = dates[0],
            LastPlayed = dates[^1],
            Rank = playerRank.Rank,
            Percentile = percentile,
            Achievements = achievements
        });

        Console.WriteLine("\n" + report + "\n");
    }

    private static List<(string PlayerName, int Rank)> RankPlayers(
        List<(string PlayerName, PlayerStatsData Stats)> playerStatsList)
    {
        var sorted = playerStatsList
            .OrderByDescending(p => p.Stats.WinRate)
            .ThenByDescending(p => p.Stats.GamesWon)
            .ToList();

        return sorted.Select((p, index) => (p.PlayerName, Rank: index + 1)).ToList();
    }

    public static void ShowHelp()
    {
        Console.WriteLine(@"
Wordle Stats CLI
================

Commands:
  dotnet run --project WordleStats.Cli list              List all game results
  dotnet run --project WordleStats.Cli stats <player>    Show stats for a specific player
  dotnet run --project WordleStats.Cli report <player>   Generate full player report
  dotnet run --project WordleStats.Cli help              Show this help message

Interactive mode:
  dotnet run --project WordleStats.Cli                   Start interactive menu

Available players: " + string.Join(", ", GetPlayerNames()) + @"
");
    }

    public static void ShowMenu()
    {
        Console.WriteLine(@"
╔═════════════════════════════════════╗
║     Wordle Stats - Main Menu        ║
╠═════════════════════════════════════╣
║  1. List all games                  ║
║  2. Show player stats               ║
║  3. Generate player report          ║
║  0. Exit                            ║
╚═════════════════════════════════════╝
");
    }

    public static void RunInteractive()
    {
        // Set console output encoding to UTF-8 for emoji support
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            ShowMenu();
            Console.Write("Select an option: ");
            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    ListGames();
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    break;
                case "2":
                    {
                        Console.WriteLine($"\nAvailable players: {string.Join(", ", GetPlayerNames())}");
                        Console.Write("Enter player name: ");
                        var player = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(player))
                        {
                            ShowPlayerStats(player);
                        }
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine($"\nAvailable players: {string.Join(", ", GetPlayerNames())}");
                        Console.Write("Enter player name: ");
                        var player = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(player))
                        {
                            ShowPlayerReport(player);
                        }
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        break;
                    }
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
