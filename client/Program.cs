using client.Models;
using ModelContextProtocol.Client;
using System.Text.Json;

var clientTransport = new StdioClientTransport(new StdioClientTransportOptions
{
    Name = "console-mcp-server",
    Command = "dotnet",
    Arguments = ["run", "--project", "C:\\dev\\console-mcp\\server\\server.csproj"]
});

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nTools");
Console.WriteLine("-----");
Console.ForegroundColor = ConsoleColor.Magenta;

var client = await McpClientFactory.CreateAsync(clientTransport);

foreach (var tool in await client.ListToolsAsync())
{
    Console.WriteLine($"{tool.Name} ({tool.Description})");
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nEcho");
Console.WriteLine("----");
Console.ForegroundColor = ConsoleColor.Magenta;

var result = await client.CallToolAsync("Echo", new Dictionary<string, object?>
{
    ["message"] = "Olá, MCP!"
}, cancellationToken: CancellationToken.None);

Console.WriteLine(result.Content.First(c => c.Type == "text").Text);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nReverseEcho");
Console.WriteLine("-----------");
Console.ForegroundColor = ConsoleColor.Magenta;

result = await client.CallToolAsync("ReverseEcho", new Dictionary<string, object?>
{
    ["message"] = "Olá, MCP!"
}, cancellationToken: CancellationToken.None);

Console.WriteLine(result.Content.First(c => c.Type == "text").Text);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nGetHeroes");
Console.WriteLine("---------");
Console.ForegroundColor = ConsoleColor.Magenta;

result = await client.CallToolAsync("GetHeroes", new Dictionary<string, object?>(), cancellationToken: CancellationToken.None);

if (result.Content.FirstOrDefault()?.Type == "text")
{
    var jsonHero = result.Content.First().Text ?? "";
    var heroes = JsonSerializer.Deserialize<List<Hero>>(jsonHero) ?? [];

    foreach (var hero in heroes)
    {
        Console.WriteLine($"Id: {hero.Id}, Nome: {hero.RealName}");
    }
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nGetHeroById");
Console.WriteLine("-----------");
Console.ForegroundColor = ConsoleColor.Magenta;

result = await client.CallToolAsync("GetHeroById", new Dictionary<string, object?>
{
    ["id"] = "4"
}, cancellationToken: CancellationToken.None);

if (result.Content.FirstOrDefault()?.Type == "text")
{
    var jsonHero = result.Content.First().Text ?? "";
    var hero = JsonSerializer.Deserialize<Hero>(jsonHero) ?? new();
    
    Console.WriteLine($"Id: {hero.Id}, Nome: {hero.HeroName}");
}
