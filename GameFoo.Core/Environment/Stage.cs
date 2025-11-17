using GameFoo.Core.Actors;
using GameFoo.Core.Primitives;

namespace GameFoo.Core.Environment;

public class Stage
{
    // matrix of terrain tiles
    private readonly Tile?[,] _tiles;
    private List<Tile> _tileCache = [];

    public Stage(int width, int height)
    {
        _tiles = new Tile[width, height];
    }

    public List<Tile> Tiles => _tileCache;
    public TilePosition SpawnPosition { get; private set; } = new(1, 1);

    public void AddTileSpan(string renderId, int x, int y, int length, int height)
    {
        for (int i = x; i < x + length; i++)
        {
            _tiles[i, y] = new Tile(renderId, new TilePosition(i, y));
        }

        RefreshCache();
    }

    public void SetSpawnPosition(int x, int y) => SpawnPosition = new TilePosition(x, y);

    public CollisionResult? DetectNextCollision(IActor actor, TimeDelta deltaTime)
    {
        CollisionResult? earliestCollision = null;
        float earliestTime = float.MaxValue;

        foreach (Tile tile in _tileCache)
        {
            CollisionResult? result = CollisionSystem.SweptAABB_Collision(actor, tile, deltaTime);
            // if result != null, there's going to be a collision
            if (result != null && result.Value.Time < earliestTime)
            {
                earliestCollision = result;
                earliestTime = result.Value.Time;
            }
        }

        return earliestCollision;
    }

    private List<BoundingBox> GetCollisionBoxes()
    {
        return _tileCache
            .Select(t => t.Bounds)
            .ToList();
    }

    private void RefreshCache()
    {
        List<Tile> tileList = [];
        for (int x = 0; x < _tiles.GetLength(0); x++)
        for (int y = 0; y < _tiles.GetLength(1); y++)
        {
            if (_tiles[x, y] != null)
            {
                tileList.Add(_tiles[x, y]!);
            }
        }

        _tileCache = tileList;
    }
}
