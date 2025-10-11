using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Services;
using Api.Tests.Framework;
using Api.Tests.Mocks;
using Api.Tests.TestData;
using Xunit;

namespace Api.Tests;

[Collection("ApiTest")]
public class ApiTests(ApiTestFixture fixture)
{
    private readonly ApiTestFixture _fixture = fixture;

    [Fact]
    public async Task Get_0_Users_OK()
    {
        var testUsers = new List<User>();
        _fixture.MockDatabase.SetUsers(testUsers);
        var client = _fixture.CreateClient();
        var (response, payload) = await client.GetAsync<UsersResponse>("/users");
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        Assert.NotNull(payload);
        Assert.Equal(testUsers.Count, payload.Total);
        Assert.Equivalent(testUsers, payload.Data);
        Assert.Null(payload.NextPage);
        Assert.Null(payload.PreviousPage);
    }

    [Fact]
    public async Task Get_10_Users_OK()
    {
        var testUsers = TestUsers.Users.GetRange(0, 10);
        _fixture.MockDatabase.SetUsers(testUsers);
        var client = _fixture.CreateClient();
        var (response, payload) = await client.GetAsync<UsersResponse>("/users");
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        Assert.NotNull(payload);
        Assert.Equal(testUsers.Count, payload.Total);
        Assert.Equivalent(testUsers, payload.Data);
        Assert.Null(payload.NextPage);
        Assert.Null(payload.PreviousPage);
    }

    [Fact]
    public async Task Get_20_Users_OK()
    {
        var testUsers = TestUsers.Users.GetRange(0, 20);
        _fixture.MockDatabase.SetUsers(testUsers);
        var client = _fixture.CreateClient();
        var (response, payload) = await client.GetAsync<UsersResponse>("/users");
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        Assert.NotNull(payload);
        Assert.Equal(testUsers.Count, payload.Total);
        Assert.Equivalent(testUsers.Take(10).ToList(), payload.Data);
        Assert.Null(payload.NextPage);
        Assert.Null(payload.PreviousPage);
    }
}
