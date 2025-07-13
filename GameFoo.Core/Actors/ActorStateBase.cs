using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Core.Actors;

public abstract class ActorStateBase : IActorState

{
    protected readonly FrameSet _frameSet;
    protected FrameData _currentFrame;

    // TODO: set _currentFrame
    protected ActorStateBase(FrameSet frameSet)
    {
        _frameSet = frameSet;
        _currentFrame = _frameSet[0];
    }

    protected float CycleDuration => _frameSet.Duration;

    public abstract string RenderId { get; }
    public float ElapsedSeconds { get; private set; }
    public int FrameIndex => _currentFrame.Index;
    public PixelSize Size => _currentFrame.Size;
    public BoundingBox? Hitbox => _currentFrame.Hitbox;

    public virtual void OnEnter(IActor actor)
    {
    }

    public virtual void OnExit(IActor actor)
    {
    }

    public virtual bool CanHandleCommand(IActor actor, ICommand command)
    {
        return true;
    }

    public abstract void HandleCommand(IActor actor, ICommand command);

    public virtual void Advance(IActor actor, TimeDelta deltaTime)
    {
        ElapsedSeconds += deltaTime.TotalSeconds;

        bool invertFrame = actor.Orientation == ActorOrientation.Left;
        FrameData activeFrame = _frameSet.GetFrameData(ElapsedSeconds, invertFrame);
        _currentFrame = activeFrame;
    }

    public List<BoundingBox> GetCollisionBoxes(PixelPosition position)
    {
        return _currentFrame.CollisionBoxes.Values
            .Select(box => box + position)
            .ToList();
    }

    public virtual void TakeDamage(IActor actor, IActor other)
    {
        // Override
    }
}
