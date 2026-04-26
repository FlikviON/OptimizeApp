using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizationApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptimizationApp.Controllers;

[ApiController]
[Route("api/settings")]
public class BiomeSettingsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BiomeSettingsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BiomeSetting>>> GetSettings()
    {
        return Ok(await _db.BiomeSettings.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BiomeSetting>> GetSetting(int id)
    {
        var setting = await _db.BiomeSettings.FindAsync(id);
        if (setting == null)
        {
            return NotFound();
        }
        return Ok(setting);
    }

    [HttpPost]
    public async Task<ActionResult<BiomeSetting>> CreateSetting(BiomeSetting setting)
    {
        _db.BiomeSettings.Add(setting);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSetting), new { id = setting.Id }, setting);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSetting(int id, BiomeSetting setting)
    {
        if (id != setting.Id)
        {
            return BadRequest();
        }
        _db.Entry(setting).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSetting(int id)
    {
        var setting = await _db.BiomeSettings.FindAsync(id);
        if (setting == null)
        {
            return NotFound();
        }
        _db.BiomeSettings.Remove(setting);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}