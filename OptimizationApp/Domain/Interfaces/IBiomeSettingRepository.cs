using OptimizationApp.Domain.Entities;

namespace OptimizationApp.Domain.Interfaces;

public interface IBiomeSettingRepository
{
    Task<IEnumerable<BiomeSetting>> GetAllAsync();
    Task<BiomeSetting?> GetByIdAsync(int id);
    Task<BiomeSetting> CreateAsync(BiomeSetting setting);
    Task<bool> UpdateAsync(BiomeSetting setting);
    Task<bool> DeleteAsync(int id);
}
