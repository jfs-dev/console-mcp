using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Services;

public class HeroService(AppDbContext dbContext)
{
    public async Task<List<Hero>> GetHeroes() =>
        await dbContext.Heroes.AsNoTracking().ToListAsync();

    public async Task<Hero?> GetHeroById(int id) =>
        await dbContext.Heroes.FindAsync(id);
}
