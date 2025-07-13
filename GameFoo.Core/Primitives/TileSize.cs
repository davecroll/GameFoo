namespace GameFoo.Core.Primitives;

public readonly struct TileSize(int width, int height)
{
    public int Width { get; } = width;
    public int Height { get; } = height;

    public PixelSize ToPixelSize() => new(Width * 16, Height * 16);
}
