namespace GameFoo.Core.Primitives;

public enum VelocityUnit
{
    PixelsPerSecond = 1
}

public readonly struct Velocity(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public VelocityUnit Unit { get; } = VelocityUnit.PixelsPerSecond;

    public Velocity WithX(int x) => new(x, Y);

    public Velocity WithY(int y) => new(X, y);

    public static Velocity operator *(Velocity velocity, float scalar) =>
        new((int)(velocity.X * scalar), (int)(velocity.Y * scalar));

    public static Velocity operator *(Velocity velocity, Velocity other) =>
        new(velocity.X * other.X, velocity.Y * other.Y);

    public static Velocity operator -(Velocity velocity, Velocity other) =>
        new(velocity.X - other.X, velocity.Y - other.Y);

    public static int Dot(Velocity velocity, Velocity other) => velocity.X * other.X + velocity.Y * other.Y;
}
