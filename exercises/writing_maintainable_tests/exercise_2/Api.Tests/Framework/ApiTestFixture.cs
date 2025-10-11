using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Api.Services;
using Api.Tests.Mocks;

namespace Api.Tests.Framework;

public class ApiTestFixture : WebApplicationFactory<Startup>
{
    public MockDatabase MockDatabase { get; } = new MockDatabase();

    public IServiceScope CreateScope()
    {
        return Services.CreateScope();
    }
    
    public new IJsonClient CreateClient()
    {
        return new JsonClient(base.CreateClient());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var testConfig = new Dictionary<string, string>();

        builder.ConfigureAppConfiguration(b =>
        {
            b.AddInMemoryCollection(testConfig.Select(t =>
                new KeyValuePair<string, string>(t.Key, t.Value)));
        });
        
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IDatabase>(MockDatabase);
        });
    }
}
