using GameFoo.Core;
using GameFoo.Core.Primitives;

namespace GameFoo.Application.FrameSets;

public class HurtFrameSet : FrameSet
{
    private static readonly List<FrameData> s_frames =
    [
        new(
            0,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(20, 11, 8, 8) }, { "Body", new BoundingBox(17, 18, 10, 22) }
            },
            null,
            0.1f),
        new(
            1,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 11, 8, 8) }, { "Body", new BoundingBox(16, 18, 8, 22) }
            },
            null,
            0.1f),
        new(
            2,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(18, 11, 8, 8) }, { "Body", new BoundingBox(15, 18, 9, 22) }
            },
            null,
            0.1f),
        new(
            3,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(19, 11, 8, 8) }, { "Body", new BoundingBox(15, 18, 11, 22) }
            },
            null,
            0.1f)
    ];

    public HurtFrameSet() : base(s_frames)
    {
    }
}
