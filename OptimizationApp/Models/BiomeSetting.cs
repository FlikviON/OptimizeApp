using System.ComponentModel.DataAnnotations;

namespace OptimizationApp.Models;

public class BiomeSetting
{
    [Key]
    public int Id { get; set; }
    public string Biome { get; set; } = "";
    public double Speed { get; set; }
}