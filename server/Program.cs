using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using server.Data;
using server.Models;
using server.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("console-mcp-server"));

builder.Logging.AddConsole(consoleLogOptions =>
{
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddSingleton<HeroService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    dbContext.Heroes.AddRange(
        new Hero { HeroName = "Superman", RealName = "Clark Kent" },
        new Hero { HeroName = "Batman", RealName = "Bruce Wayne" },
        new Hero { HeroName = "Capitão América", RealName = "Steve Rogers" },
        new Hero { HeroName = "Mulher Maravilha", RealName = "Diana de Themyscira" },
        new Hero { HeroName = "Homem de Ferro", RealName = "Tony Stark" }
    );

    dbContext.SaveChanges();
}

app.Run();
