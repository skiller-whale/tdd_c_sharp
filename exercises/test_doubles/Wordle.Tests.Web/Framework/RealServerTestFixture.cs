using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wordle.Services;
using Wordle.Tests.Web.Mocks;
using Xunit;

namespace Wordle.Tests.Web.Framework;

public class RealServerTestFixture : IAsyncLifetime
{
    private IHost _webHost;
    private readonly int _webPort;
    public string WebBaseUrl => $"http://localhost:{_webPort}";
    // TODO: Replace the real database with a mock
    // public MockDatabase MockDatabase { get; } = new();

    public RealServerTestFixture()
    {
        _webPort = GetAvailablePort();
    }

    public async Task InitializeAsync()
    {
        var webArgs = new[] { $"--urls={WebBaseUrl}" };
        _webHost = Program.CreateHostBuilder(webArgs)
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ApiBaseUrl"] = WebBaseUrl
                });
            })
            // TODO: Replace the real database with a mock
            // .ConfigureServices(services =>
            // {
            //     var descriptor = services.SingleOrDefault(
            //         d => d.ServiceType == typeof(IDatabase));
                    
            //     if (descriptor != null)
            //     {
            //         services.Remove(descriptor);
            //     }
                
            //     services.AddSingleton<IDatabase>(MockDatabase);
            // })
            .Build();
        await _webHost.StartAsync();
    }

    public async Task DisposeAsync()
    {
        if (_webHost != null)
        {
            await _webHost.StopAsync();
            _webHost.Dispose();
        }
    }

    private static int GetAvailablePort()
    {
        using var socket = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, 0);
        socket.Start();
        var port = ((System.Net.IPEndPoint)socket.LocalEndpoint).Port;
        socket.Stop();
        return port;
    }
}
