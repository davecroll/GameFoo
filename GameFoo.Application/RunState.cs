using GameFoo.Application.Commands;
using GameFoo.Application.FrameSets;
using GameFoo.Core.Actors;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Application;

public class RunState : ActorStateBase
{
    public RunState() : base(new RunFrameSet())
    {
    }

    public override string RenderId => "Player_Run";

    public override void OnEnter(IActor actor)
    {
        int horizontalVelocity = actor.Orientation == ActorOrientation.Right
            ? actor.Speed
            : -actor.Speed;

        actor.Velocity = new Velocity(horizontalVelocity, 0);
    }

    public override bool CanHandleCommand(IActor actor, ICommand command)
    {
        return command is MoveLeftCommand or MoveRightCommand or JumpCommand or JabCommand or StopCommand;
    }

    public override void HandleCommand(IActor actor, ICommand command)
    {
        if (command is MoveLeftCommand)
        {
            actor.Orientation = ActorOrientation.Left;
            actor.Velocity = actor.Velocity.WithX(-actor.Speed);
        }

        if (command is MoveRightCommand)
        {
            actor.Orientation = ActorOrientation.Right;
            actor.Velocity = actor.Velocity.WithX(actor.Speed);
        }

        if (command is JumpCommand)
        {
            actor.SetState(new JumpState());
        }

        if (command is JabCommand)
        {
            actor.SetState(new JabState());
        }

        if (command is StopCommand)
        {
            actor.SetState(new IdleState());
        }
    }

    public override void TakeDamage(IActor actor, IActor other)
    {
        actor.SetState(new HurtState());
    }
}
