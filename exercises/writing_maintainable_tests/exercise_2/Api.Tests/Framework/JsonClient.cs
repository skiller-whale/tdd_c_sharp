using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Framework;

public interface IJsonClient : IDisposable
{
    HttpClient Client { get; }
    Task<(HttpResponseMessage, TResponse)> GetAsync<TResponse>(string uri);
}

public class JsonClient : IJsonClient
{
    public HttpClient Client { get; private set; }
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonClient(HttpClient client)
    {
        Client = client;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
    }

    public async Task<(HttpResponseMessage, TResponse)> GetAsync<TResponse>(string uri)
    {
        var response = await Client.GetAsync(uri);
        var payload = await GetResponseBody<TResponse>(response);

        return (response, payload);
    }

    private async Task<TResponse> GetResponseBody<TResponse>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(content, _jsonOptions)!;
    }

    public void Dispose()
    {
        Client?.Dispose();
    }
}
