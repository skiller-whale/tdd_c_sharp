using System.Collections.Generic;
using Api.Services;
using Api.Tests.TestData;

namespace Api.Tests.Mocks;

public class MockDatabase : IDatabase
{
    private List<User> _users = new();

    public List<User> GetUsers()
    {
        return _users;
    }

    public void SetUsers(List<User> users)
    {
        _users = users;
    }
}
