using OptimizationApp.Application.Interfaces;
using OptimizationApp.Domain.Entities;
using OptimizationApp.Domain.Interfaces;

namespace OptimizationApp.Application.Services;

public class BiomeSettingService : IBiomeSettingService
{
    private readonly IBiomeSettingRepository _repository;

    public BiomeSettingService(IBiomeSettingRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<BiomeSetting>> GetAllAsync() =>
        _repository.GetAllAsync();

    public Task<BiomeSetting?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);

    public Task<BiomeSetting> CreateAsync(BiomeSetting setting) =>
        _repository.CreateAsync(setting);

    public async Task<bool> UpdateAsync(int id, BiomeSetting setting)
    {
        if (id != setting.Id) return false;
        return await _repository.UpdateAsync(setting);
    }

    public Task<bool> DeleteAsync(int id) =>
        _repository.DeleteAsync(id);
}
