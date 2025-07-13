using GameFoo.Core.Behaviors;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Core.Actors;

public interface IActor : ISceneEntity, IMoveable, ICollidable, IDamageable
{
    IActorState State { get; }
    ActorOrientation Orientation { get; set; }
    bool IsOnGround { get; set; }

    void SetState(IActorState state);
    void ApplyGravity(int gravity);
}
