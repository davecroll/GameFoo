using System.Collections.Generic;
using GameFoo.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFoo.Renderers;

public interface IRenderer
{
    public void Update(GameTime gameTime);
    public void Render(SpriteBatch spriteBatch, Dictionary<string, ISprite> spriteDictionary);
}
