using GameFoo.Core;
using GameFoo.Core.Primitives;

namespace GameFoo.Application.FrameSets;

public class JabFrameSet : FrameSet
{
    private static readonly List<FrameData> s_frames =
    [
        new(
            0,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 11, 8, 8) }, { "Body", new BoundingBox(14, 20, 13, 20) }
            },
            null,
            0.04f),
        new(
            1,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(25, 14, 8, 8) }, { "Body", new BoundingBox(16, 20, 12, 20) }
            },
            new BoundingBox(39, 20, 5, 5),
            0.04f),
        new(
            2,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(25, 14, 8, 8) }, { "Body", new BoundingBox(16, 20, 12, 20) }
            },
            new BoundingBox(39, 20, 5, 5),
            0.04f),
        new(
            3,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(22, 13, 8, 8) }, { "Body", new BoundingBox(16, 20, 12, 20) }
            },
            new BoundingBox(33, 20, 5, 5),
            0.05f),
        new(
            4,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(19, 12, 8, 8) }, { "Body", new BoundingBox(15, 20, 13, 20) }
            },
            null,
            0.05f),
        new(
            5,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(19, 12, 8, 8) }, { "Body", new BoundingBox(15, 20, 13, 20) }
            },
            null,
            0.05f),
        new(
            6,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 11, 8, 8) }, { "Body", new BoundingBox(14, 20, 13, 20) }
            },
            null,
            0.05f),
        new(
            7,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 11, 8, 8) }, { "Body", new BoundingBox(14, 20, 13, 20) }
            },
            null,
            0.05f),
        new(
            8,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 11, 8, 8) }, { "Body", new BoundingBox(14, 20, 13, 20) }
            },
            null,
            0.05f),
        new(
            9,
            new PixelSize(48, 48),
            new Dictionary<string, BoundingBox>
            {
                { "Head", new BoundingBox(17, 11, 8, 8) }, { "Body", new BoundingBox(14, 20, 13, 20) }
            },
            null,
            0.05f)
    ];

    public JabFrameSet() : base(s_frames)
    {
    }
}
