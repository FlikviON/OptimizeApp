using OptimizationApp.Domain.Entities;

namespace OptimizationApp.Application.DTOs;

public class PathResponse
{
    public double Distance { get; set; }
    public List<Point> Path { get; set; } = new();
}
