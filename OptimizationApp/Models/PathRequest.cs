namespace OptimizationApp.Models;

public class PathRequest
{
    public int Size { get; set; }
    public int[][] Map { get; set; } = null!;
    public Point A { get; set; } = null!;
    public Point B { get; set; } = null!;
}