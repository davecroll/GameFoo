using GameFoo.Core.Primitives;

namespace GameFoo.Core.Actors;

public abstract class ActorBase : IActor
{
    protected ActorBase(IActorState initialState)
    {
        State = initialState;
    }

    public string RenderId => State.RenderId;
    public PixelSize Size => State.Size;
    public PixelPosition Position { get; set; } = new(0, 0);
    public Velocity Velocity { get; set; }
    public int Speed { get; } = 300;
    public ActorOrientation Orientation { get; set; }
    public bool IsOnGround { get; set; }

    public IActorState State { get; private set; }

    // Fixed physics collider (relative to Position). Tune as needed for your character.
    // Chosen so bottom aligns near Position.Y + 40 across frames.
    protected virtual PixelSize PhysicsColliderSize { get; } = new(12, 22);
    protected virtual PixelPosition PhysicsColliderOffset { get; } = new(18, 18);

    protected BoundingBox GetPhysicsBounds() =>
        new(Position.X + PhysicsColliderOffset.X, Position.Y + PhysicsColliderOffset.Y,
            PhysicsColliderSize.Width, PhysicsColliderSize.Height);

    public void SetState(IActorState state)
    {
        // No position anchoring: physics uses a fixed collider independent of animation
        State.OnExit(this);
        State = state;
        State.OnEnter(this);
    }

    public void ApplyGravity(int gravity)
    {
        Velocity = Velocity.WithY(Math.Min(Velocity.Y + gravity, gravity));
    }

    public virtual void Advance(TimeDelta deltaTime)
    {
        State.Advance(this, deltaTime);

        Position += Velocity * deltaTime.TotalSeconds;
    }

    public BoundingBox GetRoughBoundingBox()
    {
        // Use fixed physics collider for movement/collision
        return GetPhysicsBounds();
    }

    public List<BoundingBox> GetCollisionBoxes(BoundingBox? areaOfConcern = null)
    {
        // For now, return animation collision boxes for rendering/combat debug only.
        if (areaOfConcern != null)
        {
            throw new ArgumentException("Area of concern not supported yet.");
        }

        return State.GetCollisionBoxes(Position);
    }

    public List<BoundingBox> GetHurtBoxes() => State.GetCollisionBoxes(Position);

    public void TakeDamage(IActor other)
    {
        State.TakeDamage(this, other);
    }

    public BoundingBox? GetHitbox()
    {
        BoundingBox? relativeHitbox = State.Hitbox;
        if (relativeHitbox == null)
        {
            return null;
        }

        return new BoundingBox(Position.X + relativeHitbox.Value.X, Position.Y + relativeHitbox.Value.Y,
            relativeHitbox.Value.Width, relativeHitbox.Value.Height);
    }
}
