using GameFoo.Core.Actors;
using GameFoo.Core.Behaviors;
using GameFoo.Core.Environment;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Core;

public class GameScene
{
    private readonly List<IActor> _actors = [];

    private readonly int _gravity = 100;
    private readonly List<ISceneEntity> _sceneEntities = [];

    public GameScene(Stage stage, Player player)
    {
        Stage = stage;
        Player = player;
        Player.Position = Stage.SpawnPosition.ToPixelPosition();
        _actors.Add(player);
    }

    public Stage Stage { get; }
    public Player Player { get; }

    public void AddNpc(NonPlayerCharacter nonPlayerCharacter)
    {
        TilePosition spawnPosition = Stage.SpawnPosition.AddX(30);
        nonPlayerCharacter.Position = spawnPosition.ToPixelPosition();
        _actors.Add(nonPlayerCharacter);
    }

    public void ApplyCommands(List<ICommand> commands)
    {
        foreach (ICommand command in commands)
        {
            Player.HandleCommand(command);
        }
    }

    public void Advance(TimeDelta deltaTime)
    {
        // Advance actors
        foreach (IActor actor in _actors)
        {
            if (!actor.IsOnGround)
                actor.ApplyGravity(_gravity);

            HandleMovement(actor, deltaTime);
        }

        HandleDamage();
    }

    private void HandleMovement(IActor actor, TimeDelta deltaTime)
    {
        CollisionResult? collisionResult = Stage.DetectNextCollision(actor, deltaTime);
        if (collisionResult != null)
        {
            float t = collisionResult.Value.Time;
            // Clamp to frame duration just in case
            float frame = deltaTime.TotalSeconds;
            if (t < 0f) t = 0f;
            if (t > frame) t = frame;

            // Move up to the collision time
            if (t > 0f)
            {
                actor.Advance(TimeDelta.FromSeconds(t));
            }

            // Collision response: stop movement along the collision normal
            Velocity n = collisionResult.Value.Normal;
            Velocity v = actor.Velocity;
            if (n.X != 0)
            {
                v = v.WithX(0);
            }
            if (n.Y != 0)
            {
                v = v.WithY(0);
                if (n.Y == -1)
                {
                    // Collided with floor (surface normal pointing up)
                    actor.IsOnGround = true;
                }
            }
            actor.Velocity = v;

            // If we started overlapped (t == 0), nudge out 1 pixel along normal to avoid sticking
            if (collisionResult.Value.Time == 0f && (n.X != 0 || n.Y != 0))
            {
                actor.Position += new Velocity(n.X, n.Y);
            }

            // Advance the remainder of the frame for sliding along tangent
            float remaining = frame - t;
            if (remaining > 0f)
            {
                actor.Advance(TimeDelta.FromSeconds(remaining));
            }
        }
        else
        {
            actor.Advance(deltaTime);
        }

        // Post-move grounded check: if there's no support beneath, clear grounded so gravity applies next frame
        bool supported = Stage.IsActorSupported(actor);
        if (!supported)
        {
            actor.IsOnGround = false;
        }
    }

    public List<ISceneEntity> GetSceneEntities()
    {
        _sceneEntities.Clear();
        _sceneEntities.AddRange(Stage.Tiles);
        _sceneEntities.AddRange(_actors);
        return _sceneEntities;
    }

    private void HandleDamage()
    {
        // Hit detection
        BoundingBox? playerHitbox = Player.GetHitbox();
        if (playerHitbox == null)
        {
            return;
        }

        List<IDamageable> damageableActors = _actors.OfType<IDamageable>().ToList();
        foreach (IDamageable actor in damageableActors)
        {
            if (actor is Player)
            {
                continue;
            }

            List<BoundingBox> hurtBoxes = actor.GetHurtBoxes();
            if (hurtBoxes.Any(x => x.Intersects(playerHitbox.Value)))
            {
                actor.TakeDamage(Player);
            }
        }
    }
}
