using GameFoo.Core;
using GameFoo.Core.Primitives;

namespace GameFoo.Application.FrameSets;

public class DeathFrameSet : FrameSet
{
    private static readonly List<FrameData> s_frames =
    [
        new(
            0,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(8, 20, 14, 20) } },
            null,
            0.11f),
        new(
            1,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(11, 20, 11, 20) } },
            null,
            0.11f),
        new(
            2,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(13, 23, 9, 17) } },
            null,
            0.11f),
        new(
            3,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(13, 24, 9, 16) } },
            null,
            0.11f),
        new(
            4,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(13, 24, 9, 16) } },
            null,
            0.11f),
        new(
            5,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(13, 24, 9, 16) } },
            null,
            0.11f),
        new(
            6,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(15, 27, 13, 13) } },
            null,
            0.11f),
        new(
            7,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(15, 36, 15, 4) } },
            null,
            0.11f),
        new(
            8,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(15, 36, 15, 4) } },
            null,
            0.11f),
        new(
            9,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox> { { "Body", new BoundingBox(15, 36, 15, 4) } },
            null,
            0.11f),
    ];

    public DeathFrameSet() : base(s_frames)
    {
    }
}
