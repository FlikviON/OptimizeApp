using OptimizationApp.Application.Interfaces;
using OptimizationApp.Domain.Entities;

namespace OptimizationApp.Endpoints;

public static class BiomeSettingEndpoints
{
    public static void MapBiomeSettingEndpoints(this WebApplication app)
    {
        app.MapGet("/api/settings", async (IBiomeSettingService service) =>
            Results.Ok(await service.GetAllAsync()));

        app.MapGet("/api/settings/{id}", async (int id, IBiomeSettingService service) =>
        {
            var setting = await service.GetByIdAsync(id);
            return setting is null ? Results.NotFound() : Results.Ok(setting);
        });

        app.MapPost("/api/settings", async (BiomeSetting setting, IBiomeSettingService service) =>
        {
            var created = await service.CreateAsync(setting);
            return Results.Created($"/api/settings/{created.Id}", created);
        });

        app.MapPut("/api/settings/{id}", async (int id, BiomeSetting setting, IBiomeSettingService service) =>
        {
            var success = await service.UpdateAsync(id, setting);
            return success ? Results.NoContent() : Results.BadRequest();
        });

        app.MapDelete("/api/settings/{id}", async (int id, IBiomeSettingService service) =>
        {
            var success = await service.DeleteAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}
