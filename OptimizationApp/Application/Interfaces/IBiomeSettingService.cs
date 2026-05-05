using OptimizationApp.Domain.Entities;

namespace OptimizationApp.Application.Interfaces;

public interface IBiomeSettingService
{
    Task<IEnumerable<BiomeSetting>> GetAllAsync();
    Task<BiomeSetting?> GetByIdAsync(int id);
    Task<BiomeSetting> CreateAsync(BiomeSetting setting);
    Task<bool> UpdateAsync(int id, BiomeSetting setting);
    Task<bool> DeleteAsync(int id);
}
