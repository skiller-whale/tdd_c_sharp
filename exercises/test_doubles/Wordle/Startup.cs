using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using Wordle.Services;

namespace Wordle;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddHttpContextAccessor();
        services.AddLogging(builder => builder.AddConsole());
        
        // Add session support
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        
        // Register services
        services.AddSingleton<IDatabase, Database>();
        services.AddSingleton<Random>();
        services.AddSingleton<IAnswerService, RandomAnswerService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseSession();

        // Initialize the database at startup
        var database = app.ApplicationServices.GetService<IDatabase>();
        database?.Initialize();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapControllerRoute(
                name: "home",
                pattern: "/",
                defaults: new { controller = "Home", action = "Index" });

            endpoints.MapControllerRoute(
                name: "newGame",
                pattern: "/",
                defaults: new { controller = "Home", action = "NewGame" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") });

            endpoints.MapControllerRoute(
                name: "game",
                pattern: "games/{id}",
                defaults: new { controller = "Home", action = "Game" });

            endpoints.MapControllerRoute(
                name: "makeGuess",
                pattern: "games/{id}",
                defaults: new { controller = "Home", action = "MakeGuess" },
                constraints: new { httpMethod = new HttpMethodRouteConstraint("POST") });
        });
    }
}
