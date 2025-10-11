using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Wordle.Core;

namespace Wordle.Services;

#nullable enable

public interface IDatabase
{
    void Initialize();
    Game? GetGame(string id);
    void SaveGame(Game game);
}

public class GameData
{
    public string Id { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> Guesses { get; set; } = [];
    public string? Error { get; set; } = null;
}

public class Database(string dataFilePath = "data.json") : IDatabase
{
    private readonly string _dataFilePath = dataFilePath;
    private Dictionary<string, GameData> _games = [];

    public async void Initialize()
    {
        // pretend this takes 1 second to initialise ...
        await Task.Delay(1000);

        if (File.Exists(_dataFilePath))
        {
            try
            {
                var json = await File.ReadAllTextAsync(_dataFilePath);
                var data = JsonSerializer.Deserialize<DatabaseData>(json);
                if (data != null)
                {
                    _games = data.Games ?? [];
                }
            }
            catch
            {
                // If there's an error reading the file, start with empty data
                _games = [];
            }
        }
    }

    public Game? GetGame(string id)
    {
        return _games.TryGetValue(id, out var gameData)
            ? new Game(gameData.Id, gameData.CorrectAnswer, gameData.Guesses, gameData.Error)
            : null;
    }

    public async void SaveGame(Game game)
    {
        _games[game.Id] = new GameData
        {
            Id = game.Id,
            CorrectAnswer = game.CorrectAnswer,
            Guesses = game.Guesses,
            Error = game.Error
        };
        await SaveToFile();
    }

    private async Task SaveToFile()
    {
        var data = new DatabaseData
        {
            Games = _games
        };

        var json = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(_dataFilePath, json);
    }

    private class DatabaseData
    {
        public Dictionary<string, GameData>? Games { get; set; }
    }
}
