namespace GameFoo.Core.Primitives;

public readonly struct PixelPosition(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public PixelPosition AddY(int _y) => new(X, Y + _y);

    public PixelPosition WithY(int _y) => new(X, _y);

    public static PixelPosition operator +(PixelPosition position, Velocity velocity)
    {
        return new PixelPosition(position.X + velocity.X, position.Y + velocity.Y);
    }
}
