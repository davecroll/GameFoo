using GameFoo.Application.Commands;
using GameFoo.Application.FrameSets;
using GameFoo.Core.Actors;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Application;

public class IdleState : ActorStateBase
{
    public IdleState() : base(new IdleFrameSet())
    {
    }

    public override string RenderId => "Player_Idle";

    public override void OnEnter(IActor actor)
    {
        actor.Velocity = new Velocity(0, 0);
    }

    public override bool CanHandleCommand(IActor actor, ICommand command)
    {
        return command is MoveLeftCommand or MoveRightCommand or JumpCommand or JabCommand;
    }

    public override void HandleCommand(IActor actor, ICommand command)
    {
        if (command is MoveLeftCommand)
        {
            actor.Orientation = ActorOrientation.Left;
            actor.SetState(new RunState());
        }

        if (command is MoveRightCommand)
        {
            actor.Orientation = ActorOrientation.Right;
            actor.SetState(new RunState());
        }

        if (command is JumpCommand)
        {
            actor.SetState(new JumpState());
        }

        if (command is JabCommand)
        {
            actor.SetState(new JabState());
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
