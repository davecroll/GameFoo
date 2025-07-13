using System;
using System.Collections.Generic;
using GameFoo.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameFoo.Renderers;

public class DebugRenderer : IRenderer
{
    private readonly SpriteFont _font;

    private readonly Texture2D _redPixel;
    private readonly Texture2D _whitePixel;
    private TimeSpan _elapsedTime = TimeSpan.Zero;
    private int _fps;
    private int _frameCounter;

    public DebugRenderer(GraphicsDevice graphicsDevice, ContentManager content)
    {
        _whitePixel = new Texture2D(graphicsDevice, 1, 1);
        _whitePixel.SetData([Color.White]);

        _redPixel = new Texture2D(graphicsDevice, 1, 1);
        _redPixel.SetData([Color.Red]);

        _font = content.Load<SpriteFont>("DefaultFont"); // Add a SpriteFont to your Content pipeline
    }

    public void Update(GameTime gameTime)
    {
        _elapsedTime += gameTime.ElapsedGameTime;
        _frameCounter++;

        if (_elapsedTime < TimeSpan.FromSeconds(1))
        {
            return;
        }

        _fps = _frameCounter;
        _frameCounter = 0;
        _elapsedTime -= TimeSpan.FromSeconds(1);
    }

    public void Render(SpriteBatch spriteBatch, Dictionary<string, ISprite> spriteDictionary)
    {
        // Debug grid
        int spacing = 50; // space between lines
        for (int i = -1000; i <= 1000; i += spacing)
        {
            Texture2D texture = i == 0 ? _redPixel : _whitePixel;
            spriteBatch.Draw(texture, new Rectangle(i, -1000, 1, 2000), Color.Gray);
            spriteBatch.Draw(texture, new Rectangle(-1000, i, 2000, 1), Color.Gray);
        }

        // TODO: This should be placed relative to the screen/view
        spriteBatch.DrawString(_font, $"FPS: {_fps}", new Vector2(10, 10), Color.White);
    }
}
