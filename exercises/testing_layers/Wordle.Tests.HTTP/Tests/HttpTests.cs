using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Wordle.Tests.HTTP.Framework;
using Xunit;

namespace Wordle.Tests.HTTP.Tests;

[Collection("HttpTest")]
public class HttpTests(HttpTestFixture fixture)
{
    private readonly HttpTestFixture _fixture = fixture;

    [Fact]
    public async Task Get_index_returns_some_HTML()
    {
        // arrange
        var client = _fixture.CreateClient();

        // act
        var response = await client.GetAsync("/");

        // assert
        // NOTE: this is a very basic test with minimal assertions
        // testing that the HTML is interactive in the right way is done higher up, in the Worldle.Tests.E2E project
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("<h1>Skiller Wordle</h1>", content);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Post_index_with_valid_guess_in_FormData_redirects_with_guess_added_to_the_URL()
    {
        // arrange
        var client = _fixture.CreateClientWithoutRedirects();
        
        // act
        var formData = _fixture.CreateGuessFormData("whale");
        var response = await client.PostAsync("/?guesses=fishy,shark", formData);

        // assert
        Assert.NotNull(response.Headers.Location);
        Assert.Equal("/?guesses=fishy,shark,whale", response.Headers.Location!.ToString());
    }

    [Fact(Skip = "TODO")]
    public async Task Post_index_with_invalid_guess_in_FormData_redirects_with_guess_not_added_to_the_URL()
    {
        // arrange
        var client = _fixture.CreateClientWithoutRedirects();
        
        // act

        // assert
    }

    [Fact(Skip = "TODO")]
    public async Task Post_index_with_invalid_guess_in_FormData_redirects_with_err_message_added_to_the_URL()
    {
        // arrange
        var client = _fixture.CreateClientWithoutRedirects();
        
        // act

        // assert
    }
}
