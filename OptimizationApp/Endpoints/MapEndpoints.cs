using OptimizationApp.Application.DTOs;
using OptimizationApp.Application.Interfaces;

namespace OptimizationApp.Endpoints;

public static class MapEndpoints
{
    public static void MapMapEndpoints(this WebApplication app)
    {
        app.MapPost("/api/generate", (GenerateMapRequest request, IMapService service) =>
        {
            if (request.Size < 1 || request.Size > 100)
                return Results.BadRequest("Размер от 1 до 100");
            return Results.Ok(service.GenerateMap(request.Size));
        });

        app.MapPost("/api/shortest-path", async (PathRequest request, IMapService service) =>
        {
            if (request.Size <= 0 || request.Size > 100)
                return Results.BadRequest("Неверный размер");
            if (request.Map.Length != request.Size || request.Map[0].Length != request.Size)
                return Results.BadRequest("Карта не соответствует размеру");
            if (request.A.Row < 0 || request.A.Row >= request.Size ||
                request.A.Col < 0 || request.A.Col >= request.Size ||
                request.B.Row < 0 || request.B.Row >= request.Size ||
                request.B.Col < 0 || request.B.Col >= request.Size)
                return Results.BadRequest("Точки вне карты");

            var result = await service.FindShortestPathAsync(request);
            return result is null
                ? Results.NotFound("Путь не найден")
                : Results.Ok(result);
        });
    }
}
