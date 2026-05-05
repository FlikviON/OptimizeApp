using Microsoft.EntityFrameworkCore;
using OptimizationApp.Domain.Entities;
using OptimizationApp.Domain.Interfaces;
using OptimizationApp.Infrastructure.Data;

namespace OptimizationApp.Infrastructure.Repositories;

public class BiomeSettingRepository : IBiomeSettingRepository
{
    private readonly AppDbContext _db;

    public BiomeSettingRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<BiomeSetting>> GetAllAsync() =>
        await _db.BiomeSettings.ToListAsync();

    public async Task<BiomeSetting?> GetByIdAsync(int id) =>
        await _db.BiomeSettings.FindAsync(id);

    public async Task<BiomeSetting> CreateAsync(BiomeSetting setting)
    {
        _db.BiomeSettings.Add(setting);
        await _db.SaveChangesAsync();
        return setting;
    }

    public async Task<bool> UpdateAsync(BiomeSetting setting)
    {
        _db.Entry(setting).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var setting = await _db.BiomeSettings.FindAsync(id);
        if (setting == null) return false;
        _db.BiomeSettings.Remove(setting);
        await _db.SaveChangesAsync();
        return true;
    }
}
