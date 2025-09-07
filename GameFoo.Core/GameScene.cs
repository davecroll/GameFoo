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
            TimeDelta timeUntilCollision = TimeDelta.FromSeconds(collisionResult.Value.Time);
            actor.Advance(timeUntilCollision);
            actor.IsOnGround = true;
        }
        else
        {
            actor.Advance(deltaTime);
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
