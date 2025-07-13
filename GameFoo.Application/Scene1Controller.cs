using GameFoo.Application.Commands;
using GameFoo.Core;
using GameFoo.Core.Actors;
using GameFoo.Core.Environment;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;

namespace GameFoo.Application;

public class Scene1Controller
{
    private readonly GameScene _scene;

    public Scene1Controller()
    {
        // build/create the scene here.
        Stage stage = MakeStage();
        Player player = new(new IdleState());
        GameScene scene = new(stage, player);

        NonPlayerCharacter nonPlayerCharacter = new(new IdleState());
        scene.AddNpc(nonPlayerCharacter);

        _scene = scene;
    }

    public (int, int) FocusPoint => (_scene.Player.Position.X, _scene.Player.Position.Y);

    public void ApplyInputs(ActorControls controls)
    {
        List<ICommand> commands = [];
        if (controls.MoveLeft)
        {
            commands.Add(new MoveLeftCommand());
        }

        if (controls.MoveRight)
        {
            commands.Add(new MoveRightCommand());
        }

        if (controls.Jump)
        {
            commands.Add(new JumpCommand());
        }

        if (controls.Jab)
        {
            commands.Add(new JabCommand());
        }

        if (commands.Count == 0)
        {
            commands.Add(new StopCommand());
        }

        _scene.ApplyCommands(commands);
    }

    public void UpdateScene(TimeDelta deltaTime) => _scene.Advance(deltaTime);

    public IEnumerable<ISceneEntity> GetSceneEntities() => _scene.GetSceneEntities();

    private Stage MakeStage()
    {
        StageBuilder builder = new();
        builder.AddDimensions(100, 50)
            .AddTileSpan("Tile_Dirt", 0, 49, 100, -99)
            .AddTileSpan("Tile_Grassy", 0, 48, 100, -99)
            .AddTileSpan("Tile_Dirt", 50, 48, 2, -99)
            .AddTileSpan("Tile_Grassy", 50, 47, 2, -99)
            .AddSpawnPosition(1, 40);
        return builder.Build();
    }
}
