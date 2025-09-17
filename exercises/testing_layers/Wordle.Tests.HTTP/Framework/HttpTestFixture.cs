using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Wordle.Tests.HTTP.Framework;

public class HttpTestFixture
{
    public WebApplicationFactory<Program> Factory { get; } = new WebApplicationFactory<Program>();

    public HttpClient CreateClient()
    {
        return Factory.CreateClient();
    }

    public HttpClient CreateClientWithoutRedirects()
    {
        return Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    public MultipartFormDataContent CreateGuessFormData(string guess)
    {
        return new MultipartFormDataContent
        {
            { new StringContent(guess), "latestGuess" }
        };
    }
}
