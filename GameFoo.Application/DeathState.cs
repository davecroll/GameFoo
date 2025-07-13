using GameFoo.Application.FrameSets;
using GameFoo.Core.Actors;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Application;

public class DeathState : ActorStateBase
{
    public DeathState() : base(new DeathFrameSet())
    {
    }

    public override string RenderId => "Player_Death";

    public override bool CanHandleCommand(IActor actor, ICommand command) => false;

    public override void HandleCommand(IActor actor, ICommand command)
    {
        // Do nothing
    }

    public override void Advance(IActor actor, TimeDelta deltaTime)
    {
        base.Advance(actor, deltaTime);
        if (ElapsedSeconds > CycleDuration)
        {
            _currentFrame = _frameSet[9];
        }
    }
}
