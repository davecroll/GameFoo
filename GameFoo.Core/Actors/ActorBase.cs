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

    public void SetState(IActorState state)
    {
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
        List<BoundingBox> boxes = State.GetCollisionBoxes(Position);
        int minX = boxes.Min(b => b.X);
        int minY = boxes.Min(b => b.Y);
        int maxX = boxes.Max(b => b.X + b.Width);
        int maxY = boxes.Max(b => b.Y + b.Height);

        return new BoundingBox(minX, minY, Math.Abs(maxX - minX), Math.Abs(maxY - minY));
    }

    public List<BoundingBox> GetCollisionBoxes(BoundingBox? areaOfConcern = null)
    {
        // TODO: Do we need to implement this?
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
