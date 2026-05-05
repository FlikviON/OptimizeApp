using OptimizationApp.Application.DTOs;

namespace OptimizationApp.Application.Interfaces;

public interface IMapService
{
    int[][] GenerateMap(int size);
    Task<PathResponse?> FindShortestPathAsync(PathRequest request);
}
