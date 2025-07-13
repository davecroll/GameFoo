using GameFoo.Core.Actors;
using GameFoo.Core.Environment;
using GameFoo.Core.Primitives;

namespace GameFoo.Core;

public struct CollisionResult
{
    public float Time; // Time of collision in seconds
    public Velocity Normal; // Direction of surface collided with
}

public static class CollisionSystem
{
    public static CollisionResult? SweptAABB_Collision(IActor actor, Tile tile, TimeDelta deltaTime)
    {
        Velocity actorVelocity = actor.Velocity;
        BoundingBox movingBox = actor.GetRoughBoundingBox();
        BoundingBox staticBox = tile.Bounds;

        // Frame velocity
        Velocity frameVelocity = actorVelocity * deltaTime.TotalSeconds;
        float dx = frameVelocity.X;
        float dy = frameVelocity.Y;

        float xInvEntry, xInvExit;
        float yInvEntry, yInvExit;

        // X axis entry and exit distances
        if (dx > 0)
        {
            xInvEntry = staticBox.Left - movingBox.Right;
            xInvExit = staticBox.Right - movingBox.Left;
        }
        else
        {
            xInvEntry = staticBox.Right - movingBox.Left;
            xInvExit = staticBox.Left - movingBox.Right;
        }

        // Y axis entry and exit distances
        if (dy > 0)
        {
            yInvEntry = staticBox.Top - movingBox.Bottom;
            yInvExit = staticBox.Bottom - movingBox.Top;
        }
        else
        {
            yInvEntry = staticBox.Bottom - movingBox.Top;
            yInvExit = staticBox.Top - movingBox.Bottom;
        }

        // Entry and exit times
        float xEntryTime = dx == 0 ? float.NegativeInfinity : xInvEntry / dx;
        float xExitTime = dx == 0 ? float.PositiveInfinity : xInvExit / dx;

        float yEntryTime = dy == 0 ? float.NegativeInfinity : yInvEntry / dy;
        float yExitTime = dy == 0 ? float.PositiveInfinity : yInvExit / dy;

        // Find earliest/latest times of collision
        float entryTime = Math.Max(xEntryTime, yEntryTime);
        float exitTime = Math.Min(xExitTime, yExitTime);

        // No collision
        if (entryTime > exitTime || entryTime < 0 || entryTime > 1)
            return null;

        // Determine collision normal
        Velocity normal;

        if (xEntryTime > yEntryTime)
            normal = dx > 0 ? new Velocity(-1, 0) : new Velocity(1, 0); // Hit left or right
        else
            normal = dy > 0 ? new Velocity(0, -1) : new Velocity(0, 1); // Hit top or bottom

        return new CollisionResult { Time = entryTime * deltaTime.TotalSeconds, Normal = normal };
    }
}
