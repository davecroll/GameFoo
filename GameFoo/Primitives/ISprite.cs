using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFoo.Primitives;

public interface ISprite
{
    public (Texture2D, Rectangle) GetFrame(int index);
}
