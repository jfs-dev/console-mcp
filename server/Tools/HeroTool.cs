using ModelContextProtocol.Server;
using server.Services;
using System.ComponentModel;
using System.Text.Json;

namespace server.Tools;

[McpServerToolType]
public static class HeroTool
{
    [McpServerTool, Description("Obter lista de heróis")]
    public static async Task<string> GetHeroes(HeroService heroService)
    {
        var heroes = await heroService.GetHeroes();
        return JsonSerializer.Serialize(heroes);
    }

    [McpServerTool, Description("Obter herói pelo id")]
    public static async Task<string> GetHeroById(HeroService heroService, int id)
    {
        var hero = await heroService.GetHeroById(id);
        return JsonSerializer.Serialize(hero);
    }
}
