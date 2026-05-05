using OptimizationApp.Application.DTOs;
using OptimizationApp.Application.Interfaces;
using OptimizationApp.Domain.Entities;
using OptimizationApp.Domain.Interfaces;

namespace OptimizationApp.Application.Services;

public class MapService : IMapService
{
    private readonly IBiomeSettingRepository _repository;
    private readonly Random _random = new();

    public MapService(IBiomeSettingRepository repository)
    {
        _repository = repository;
    }

    public int[][] GenerateMap(int size)
    {
        var map = new int[size][];
        for (int i = 0; i < size; i++)
        {
            map[i] = new int[size];
            for (int j = 0; j < size; j++)
                map[i][j] = _random.Next(0, 3);
        }
        return map;
    }

    public async Task<PathResponse?> FindShortestPathAsync(PathRequest request)
    {
        var settings = await _repository.GetAllAsync();
        var costByBiome = new double[3];

        foreach (var s in settings)
        {
            int idx = s.Biome switch
            {
                "луг" => 0,
                "болото" => 1,
                "горы" => 2,
                _ => -1
            };
            if (idx >= 0)
                costByBiome[idx] = 1.0 / s.Speed;
        }

        int n = request.Size;
        double[,] dist = new double[n, n];
        (int, int)?[,] prev = new (int, int)?[n, n];
        bool[,] visited = new bool[n, n];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                dist[i, j] = double.PositiveInfinity;

        int startRow = request.A.Row, startCol = request.A.Col;
        int goalRow = request.B.Row, goalCol = request.B.Col;
        dist[startRow, startCol] = 0;

        var pq = new SortedSet<(double, int, int)>(
            Comparer<(double, int, int)>.Create((x, y) =>
            {
                int cmp = x.Item1.CompareTo(y.Item1);
                if (cmp != 0) return cmp;
                if (x.Item2 != y.Item2) return x.Item2.CompareTo(y.Item2);
                return x.Item3.CompareTo(y.Item3);
            })
        );
        pq.Add((0, startRow, startCol));

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        while (pq.Count > 0)
        {
            var (d, r, c) = pq.Min;
            pq.Remove(pq.Min);

            if (visited[r, c]) continue;
            visited[r, c] = true;
            if (r == goalRow && c == goalCol) break;

            for (int k = 0; k < 4; k++)
            {
                int nr = r + dr[k];
                int nc = c + dc[k];
                if (nr < 0 || nr >= n || nc < 0 || nc >= n) continue;
                double newDist = d + costByBiome[request.Map[nr][nc]];
                if (newDist < dist[nr, nc] - 1e-9)
                {
                    dist[nr, nc] = newDist;
                    prev[nr, nc] = (r, c);
                    pq.Add((newDist, nr, nc));
                }
            }
        }

        if (double.IsInfinity(dist[goalRow, goalCol]))
            return null;

        var path = new List<Point>();
        var cur = (goalRow, goalCol);
        while (cur != (startRow, startCol))
        {
            path.Add(new Point { Row = cur.Item1, Col = cur.Item2 });
            var p = prev[cur.Item1, cur.Item2];
            if (!p.HasValue) break;
            cur = p.Value;
        }
        path.Add(new Point { Row = startRow, Col = startCol });
        path.Reverse();

        return new PathResponse { Distance = dist[goalRow, goalCol], Path = path };
    }
}
