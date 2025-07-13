using GameFoo.Core.Primitives;

namespace GameFoo.Core.Interfaces;

public interface ISceneEntity
{
    string RenderId { get; }
    PixelSize Size { get; }
    PixelPosition Position { get; set; }

    BoundingBox Bounds
    {
        get => new(Position.X, Position.Y, Size.Width, Size.Height);
    }
}
