using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Core.Actors;

public interface IActorState
{
    string RenderId { get; }
    public int FrameIndex { get; }
    float ElapsedSeconds { get; }
    PixelSize Size { get; }
    BoundingBox? Hitbox { get; }

    void OnEnter(IActor actor);
    void OnExit(IActor actor);
    bool CanHandleCommand(IActor actor, ICommand command);
    void HandleCommand(IActor actor, ICommand command);
    void Advance(IActor actor, TimeDelta deltaTime);
    List<BoundingBox> GetCollisionBoxes(PixelPosition position);
    void TakeDamage(IActor actor, IActor other);
}
