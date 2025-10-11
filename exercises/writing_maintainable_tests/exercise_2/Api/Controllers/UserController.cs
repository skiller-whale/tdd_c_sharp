using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Api.Services;

#nullable enable

namespace Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(IDatabase database) : ControllerBase
{
    private readonly IDatabase _database = database;

    [HttpGet("/users")]
    public UsersResponse GetUsers()
    {
        var users = _database.GetUsers();

        return new UsersResponse
        {
            Data = users.Take(10).ToList(),
            Total = users.Count
        };
    }
}

public class UsersResponse
{
    public List<User> Data { get; set; } = new();
    public int Total { get; set; } = 0;
    public string? NextPage { get; set; }
    public string? PreviousPage { get; set; }
}
