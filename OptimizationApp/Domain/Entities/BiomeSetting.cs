using System.ComponentModel.DataAnnotations;

namespace OptimizationApp.Domain.Entities;

public class BiomeSetting
{
    [Key]
    public int Id { get; set; }
    public string Biome { get; set; } = "";
    public double Speed { get; set; }
}
