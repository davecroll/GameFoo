using Microsoft.Xna.Framework;

namespace GameFoo.Primitives;

public readonly struct SpriteDefinition(string contentName, Vector2 frameSize, int frameCount)
{
    public string ContentName { get; } = contentName;
    public Vector2 FrameSize { get; } = frameSize;
    public int FrameCount { get; } = frameCount;
}
