using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFoo.Primitives;

public class AnimatedSprite : ISprite
{
    private readonly int _frameCount;
    private readonly Vector2 _frameSize;
    private readonly Texture2D _spriteSheet;

    public AnimatedSprite(Texture2D spriteSheet, Vector2 frameSize, int frameCount)
    {
        _spriteSheet = spriteSheet;
        _frameSize = frameSize;
        _frameCount = frameCount;
    }

    public (Texture2D, Rectangle) GetFrame(int index) => (_spriteSheet,
        new Rectangle((int)(index * _frameSize.X), 0, (int)_frameSize.X, (int)_frameSize.Y));
}
