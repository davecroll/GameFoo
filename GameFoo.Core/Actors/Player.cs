using GameFoo.Core.Interfaces;

namespace GameFoo.Core.Actors;

public class Player(IActorState initialState) : ActorBase(initialState)
{
    public virtual void HandleCommand(ICommand command)
    {
        if (State.CanHandleCommand(this, command))
        {
            State.HandleCommand(this, command);
        }
    }
}
