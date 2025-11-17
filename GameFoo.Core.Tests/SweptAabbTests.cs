using GameFoo.Core;
using GameFoo.Core.Primitives;
using Xunit;

namespace GameFoo.Core.Tests;

public class SweptAabbTests
{
    [Fact]
    public void FallingOntoTile_HitsTop_WithUpNormal()
    {
        // Moving box above the tile, falling down 60 px/s over 1s should collide
        BoundingBox moving = new(0, 0, 16, 16);
        BoundingBox tile = new(0, 30, 16, 16);
        Velocity v = new(0, 60);
        TimeDelta dt = TimeDelta.FromSeconds(1f);

        CollisionResult? res = CollisionSystem.SweptAABB(moving, v, tile, dt);
        Assert.NotNull(res);
        Assert.True(res!.Value.Time >= 0f && res.Value.Time <= 1f);
        Assert.Equal(0, res.Value.Normal.X);
        Assert.Equal(-1, res.Value.Normal.Y); // Upwards normal (floor)
    }

    [Fact]
    public void InitialOverlap_ReturnsImmediateCollision()
    {
        BoundingBox moving = new(0, 0, 16, 16);
        BoundingBox tile = new(8, 8, 16, 16); // overlapping at t0
        Velocity v = new(0, 0);
        TimeDelta dt = TimeDelta.FromSeconds(1f);

        CollisionResult? res = CollisionSystem.SweptAABB(moving, v, tile, dt);
        Assert.NotNull(res);
        Assert.Equal(0f, res!.Value.Time);
    }

    [Fact]
    public void NoCollision_ReturnsNull()
    {
        BoundingBox moving = new(0, 0, 16, 16);
        BoundingBox tile = new(100, 100, 16, 16);
        Velocity v = new(10, 0);
        TimeDelta dt = TimeDelta.FromSeconds(1f);

        CollisionResult? res = CollisionSystem.SweptAABB(moving, v, tile, dt);
        Assert.Null(res);
    }
}

