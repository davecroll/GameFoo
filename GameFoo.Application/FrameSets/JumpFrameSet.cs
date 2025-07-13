using GameFoo.Core;
using GameFoo.Core.Primitives;

namespace GameFoo.Application.FrameSets;

public class JumpFrameSet : FrameSet
{
    private static readonly List<FrameData> s_frames =
    [
        new(
            0,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 10, 8, 8) }, { "Body", new BoundingBox(15, 19, 11, 25) }
            },
            null,
            0.125f),
        new(
            1,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 10, 8, 8) }, { "Body", new BoundingBox(15, 19, 11, 25) }
            },
            null,
            0.125f),
        new(
            2,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 10, 8, 8) }, { "Body", new BoundingBox(15, 19, 11, 20) }
            },
            null,
            0.125f),
        new(
            3,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(18, 10, 8, 8) }, { "Body", new BoundingBox(15, 19, 11, 20) }
            },
            null,
            0.125f),
        new(
            4,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(18, 10, 8, 8) }, { "Body", new BoundingBox(15, 19, 11, 20) }
            },
            null,
            0.125f),
        new(
            5,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(18, 10, 8, 8) }, { "Body", new BoundingBox(15, 19, 11, 25) }
            },
            null,
            0.125f)
    ];

    public JumpFrameSet() : base(s_frames)
    {
    }
}
