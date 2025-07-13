using System.Collections.Generic;
using GameFoo.Application;
using GameFoo.Core.Actors;
using GameFoo.Core.Interfaces;
using GameFoo.Core.Primitives;
using GameFoo.Renderers;
using GameFoo.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFoo;

public class Game1 : Game
{
    private readonly Scene1Controller _controller;
    private readonly GameSceneRenderer _gameSceneRenderer = new();
    private readonly GraphicsDeviceManager _graphics;

    private readonly SpriteDictionary _spriteDictionary = new();
    private Camera _camera;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Set initial window size
        _graphics.PreferredBackBufferWidth = 1280; // width in pixels
        _graphics.PreferredBackBufferHeight = 720; // height in pixels
        _graphics.ApplyChanges();

        _controller = new Scene1Controller();
    }

    protected override void Initialize()
    {
        _camera = new Camera(_graphics.GraphicsDevice.Viewport);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load sprites
        _spriteDictionary.LoadTextures(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
        KeyboardState keyboardState = Keyboard.GetState();

        if (gamePadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        ActorControls playerControls = new()
        {
            MoveLeft = keyboardState.IsKeyDown(Keys.Left),
            MoveRight = keyboardState.IsKeyDown(Keys.Right),
            Jump = keyboardState.IsKeyDown(Keys.Space),
            Jab = keyboardState.IsKeyDown(Keys.Q)
        };
        _controller.ApplyInputs(playerControls);

        TimeDelta deltaTime = TimeDelta.FromSeconds((float)gameTime.ElapsedGameTime.TotalSeconds);

        _controller.UpdateScene(deltaTime);

        _camera.Position = new Vector2(_controller.FocusPoint.Item1, _controller.FocusPoint.Item2);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Move this to the Update method? I think Draw should only draw.
        IEnumerable<ISceneEntity> sceneEntities = _controller.GetSceneEntities();

        _graphics.GraphicsDevice.Clear(Color.Black);

        // TODO: ChatGPT says there's a better "pixel-perfect" way to transform
        // the world position into the "camera" position.
        // See: https://chatgpt.com/c/6810fbae-1658-8004-b511-e6db33f4aa24
        _spriteBatch.Begin(
            transformMatrix: _camera.GetViewMatrix(),
            samplerState: SamplerState.PointClamp);

        _gameSceneRenderer.Render(sceneEntities, _spriteBatch, _spriteDictionary);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
