using GameFoo.Application.Commands;
using GameFoo.Application.FrameSets;
using GameFoo.Core.Actors;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Application;

public class JumpState : ActorStateBase
{
    public JumpState() : base(new JumpFrameSet())
    {
    }

    public override string RenderId => "Player_Jump";

    public override void OnEnter(IActor actor)
    {
        int velocityY = -2000;
        actor.Velocity = new Velocity(actor.Velocity.X, velocityY);
        actor.IsOnGround = false;
    }

    public override bool CanHandleCommand(IActor actor, ICommand command)
    {
        return command is MoveLeftCommand or MoveRightCommand;
    }

    public override void HandleCommand(IActor actor, ICommand command)
    {
        const int jumpDrift = 50;

        if (command is MoveLeftCommand)
        {
            actor.Orientation = ActorOrientation.Left;

            if (actor.Velocity.X > -jumpDrift)
            {
                int nextX = actor.Velocity.X - jumpDrift;
                actor.Velocity = actor.Velocity.WithX(nextX);
            }
        }

        if (command is MoveRightCommand)
        {
            actor.Orientation = ActorOrientation.Right;

            if (actor.Velocity.X < jumpDrift)
            {
                int nextX = actor.Velocity.X + jumpDrift;
                actor.Velocity = actor.Velocity.WithX(nextX);
            }
        }
    }

    public override void Advance(IActor actor, TimeDelta deltaTime)
    {
        if (actor.IsOnGround)
        {
            actor.SetState(new IdleState());
        }

        base.Advance(actor, deltaTime);
    }

    public override void TakeDamage(IActor actor, IActor other)
    {
        actor.SetState(new HurtState());
    }
}
