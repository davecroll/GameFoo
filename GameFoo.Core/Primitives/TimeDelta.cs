namespace GameFoo.Core.Primitives;

public readonly struct TimeDelta
{
    public readonly float TotalSeconds;

    // Private constructor to enforce use of a factory method
    private TimeDelta(float totalSeconds) => TotalSeconds = totalSeconds;

    // Factory method
    public static TimeDelta FromSeconds(float seconds) => new(seconds);
}
