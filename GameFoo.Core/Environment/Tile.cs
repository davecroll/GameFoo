using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Core.Environment;

public class Tile : ISceneEntity
{
    public Tile(string renderId, TilePosition position)
    {
        RenderId = renderId;
        Position = position.ToPixelPosition();
        TilePosition = position;
    }

    public string RenderId { get; }
    public PixelSize Size { get; } = new(16, 16);
    public PixelPosition Position { get; set; }
    public BoundingBox Bounds => new(Position.X, Position.Y, Size.Width, Size.Height);

    // Tile coordinate properties
    public TileSize TileSize { get; } = new(1, 1);
    public TilePosition TilePosition { get; }
}
