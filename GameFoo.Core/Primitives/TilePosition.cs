namespace GameFoo.Core.Primitives;

public readonly struct TilePosition(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public TilePosition AddX(int x) => new(X + x, Y);

    // TODO: Can we remove hardcoded tile-to-pixel conversion?
    public PixelPosition ToPixelPosition() => new(X * 16, Y * 16);
}
