using GameFoo.Core.Primitives;

namespace GameFoo.Core.Behaviors;

public interface IMoveable
{
    Velocity Velocity { get; set; }
    int Speed { get; }

    void Advance(TimeDelta deltaTime);
}
