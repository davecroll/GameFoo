using GameFoo.Application.Commands;
using GameFoo.Application.FrameSets;
using GameFoo.Core.Actors;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Application;

public class JabState : ActorStateBase
{
    public JabState() : base(new JabFrameSet())
    {
    }

    public override string RenderId => "Player_Jab";

    public override void OnEnter(IActor actor)
    {
        actor.Velocity = new Velocity(0, 0);
    }

    public override bool CanHandleCommand(IActor actor, ICommand command)
    {
        return false;
    }

    public override void HandleCommand(IActor actor, ICommand command)
    {
        // Do nothing.
    }

    public override void Advance(IActor actor, TimeDelta deltaTime)
    {
        base.Advance(actor, deltaTime);

        if (ElapsedSeconds > CycleDuration)
        {
            actor.SetState(new IdleState());
        }
    }

    public override void TakeDamage(IActor actor, IActor other)
    {
        if (other.Position.X < actor.Position.X)
        {
            actor.Orientation = ActorOrientation.Left;
        }
        else
        {
            actor.Orientation = ActorOrientation.Right;
        }

        actor.SetState(new HurtState());
    }
}
