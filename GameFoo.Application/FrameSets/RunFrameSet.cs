using GameFoo.Core;
using GameFoo.Core.Primitives;

namespace GameFoo.Application.FrameSets;

public class RunFrameSet : FrameSet
{
    private static readonly List<FrameData> s_frames =
    [
        new(
            0,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(23, 9, 8, 8) }, { "Body", new BoundingBox(19, 18, 13, 22) }
            },
            null,
            0.08f),
        new(
            1,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(24, 12, 8, 8) }, { "Body", new BoundingBox(19, 19, 9, 21) }
            },
            null,
            0.08f),
        new(
            2,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(24, 11, 8, 8) }, { "Body", new BoundingBox(19, 19, 9, 21) }
            },
            null,
            0.08f),
        new(
            3,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(24, 9, 8, 8) }, { "Body", new BoundingBox(19, 18, 9, 18) }
            },
            null,
            0.08f),
        new(
            4,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(24, 9, 8, 8) }, { "Body", new BoundingBox(19, 18, 9, 22) }
            },
            null,
            0.08f),
        new(
            5,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(24, 12, 8, 8) }, { "Body", new BoundingBox(19, 18, 9, 22) }
            },
            null,
            0.08f),
        new(
            6,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(24, 11, 8, 8) }, { "Body", new BoundingBox(20, 18, 9, 22) }
            },
            null,
            0.08f),
        new(
            7,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(23, 9, 8, 8) }, { "Body", new BoundingBox(20, 18, 9, 19) }
            },
            null,
            0.08f)
    ];

    public RunFrameSet() : base(s_frames)
    {
    }
}
