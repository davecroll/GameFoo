using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFoo.Primitives;

public class StaticSprite : ISprite
{
    private readonly Vector2 _frameSize;
    private readonly Texture2D _texture;

    public StaticSprite(Texture2D texture, Vector2 frameSize)
    {
        _texture = texture;
        _frameSize = frameSize;
    }

    public (Texture2D, Rectangle) GetFrame(int index) =>
        (_texture, new Rectangle(0, 0, (int)_frameSize.X, (int)_frameSize.Y));
}
