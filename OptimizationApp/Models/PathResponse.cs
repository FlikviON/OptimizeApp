using System.Collections.Generic;

namespace OptimizationApp.Models;

public class PathResponse
{
    public double Distance { get; set; }
    public List<Point> Path { get; set; } = new();
}