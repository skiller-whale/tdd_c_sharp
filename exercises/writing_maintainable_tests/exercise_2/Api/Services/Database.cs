using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Services;

#nullable enable

public interface IDatabase
{
    List<User> GetUsers();
    void SetUsers(List<User> users);
}

public class User
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}

public class Database(string dataFilePath = "data.json") : IDatabase
{
    private readonly string _dataFilePath = dataFilePath;
    private List<User> _users = new();

    public async void Initialize()
    {
        if (File.Exists(_dataFilePath))
        {
            try
            {
                var json = await File.ReadAllTextAsync(_dataFilePath);
                var data = JsonSerializer.Deserialize<DatabaseData>(json);
                if (data != null)
                {
                    _users = data.Users ?? new List<User>();
                }
            }
            catch
            {
                // If there's an error reading the file, start with empty data
                _users = new List<User>();
            }
        }
    }

    public List<User> GetUsers()
    {
        return _users;
    }

    public async void SetUsers(List<User> users)
    {
        _users = users;
        await SaveToFile();
    }

    private async Task SaveToFile()
    {
        var data = new DatabaseData
        {
            Users = _users
        };

        var json = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(_dataFilePath, json);
    }

    private class DatabaseData
    {
        public List<User>? Users { get; set; }
    }
}
