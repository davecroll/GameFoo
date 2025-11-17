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
    public static CollisionResult? SweptAABB(BoundingBox movingBox, Velocity velocity, BoundingBox staticBox, TimeDelta deltaTime)
    {
        // Compute frame displacement as floats to avoid integer truncation
        float dt = deltaTime.TotalSeconds;
        float dx = velocity.X * dt;
        float dy = velocity.Y * dt;

        // Early-out: if already overlapping at t=0, return immediate collision with MTV-based normal
        if (movingBox.Intersects(staticBox))
        {
            // Compute minimal translation vector (axis of least penetration)
            int overlapLeft = movingBox.Right - staticBox.Left;   // moving penetrates into left side of static
            int overlapRight = staticBox.Right - movingBox.Left;   // moving penetrates into right side of static
            int overlapTop = movingBox.Bottom - staticBox.Top;     // moving penetrates into top side of static
            int overlapBottom = staticBox.Bottom - movingBox.Top;  // moving penetrates into bottom side of static

            // Choose the smallest absolute overlap along axes
            int minXPen = Math.Min(overlapLeft, overlapRight);
            int minYPen = Math.Min(overlapTop, overlapBottom);

            Velocity normal;
            if (minXPen < minYPen)
            {
                // Resolve horizontally; choose normal pointing outwards from static surface
                normal = (overlapLeft < overlapRight) ? new Velocity(-1, 0) : new Velocity(1, 0);
            }
            else
            {
                normal = (overlapTop < overlapBottom) ? new Velocity(0, -1) : new Velocity(0, 1);
            }

            return new CollisionResult { Time = 0f, Normal = normal };
        }

        // Broad-phase check: if the swept AABB of moving box over the frame doesn't intersect static, no collision
        int startLeft = movingBox.Left;
        int startRight = movingBox.Right;
        int startTop = movingBox.Top;
        int startBottom = movingBox.Bottom;
        int endLeft = (int)Math.Floor(movingBox.Left + dx);
        int endRight = (int)Math.Ceiling(movingBox.Right + dx);
        int endTop = (int)Math.Floor(movingBox.Top + dy);
        int endBottom = (int)Math.Ceiling(movingBox.Bottom + dy);
        int broadLeft = Math.Min(startLeft, endLeft);
        int broadRight = Math.Max(startRight, endRight);
        int broadTop = Math.Min(startTop, endTop);
        int broadBottom = Math.Max(startBottom, endBottom);
        BoundingBox broadPhase = new(broadLeft, broadTop, Math.Max(0, broadRight - broadLeft), Math.Max(0, broadBottom - broadTop));
        if (!broadPhase.Intersects(staticBox))
        {
            return null;
        }

        float xInvEntry, xInvExit;
        float yInvEntry, yInvExit;

        // X axis entry and exit distances
        if (dx > 0f)
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
        if (dy > 0f)
        {
            yInvEntry = staticBox.Top - movingBox.Bottom;
            yInvExit = staticBox.Bottom - movingBox.Top;
        }
        else
        {
            yInvEntry = staticBox.Bottom - movingBox.Top;
            yInvExit = staticBox.Top - movingBox.Bottom;
        }

        // Entry and exit times (normalized 0..1 of the frame)
        float xEntryTime, xExitTime;
        if (dx == 0f)
        {
            // If no movement on X this frame, ensure there is overlap along X for a collision
            if (movingBox.Right <= staticBox.Left || movingBox.Left >= staticBox.Right)
                return null;
            xEntryTime = float.NegativeInfinity;
            xExitTime = float.PositiveInfinity;
        }
        else
        {
            xEntryTime = xInvEntry / dx;
            xExitTime = xInvExit / dx;
        }

        float yEntryTime, yExitTime;
        if (dy == 0f)
        {
            // If no movement on Y this frame, ensure there is overlap along Y for a collision
            if (movingBox.Bottom <= staticBox.Top || movingBox.Top >= staticBox.Bottom)
                return null;
            yEntryTime = float.NegativeInfinity;
            yExitTime = float.PositiveInfinity;
        }
        else
        {
            yEntryTime = yInvEntry / dy;
            yExitTime = yInvExit / dy;
        }

        // Find earliest/latest times of collision
        float entryTime = Math.Max(xEntryTime, yEntryTime);
        float exitTime = Math.Min(xExitTime, yExitTime);

        // No collision within the frame interval [0,1]
        if (entryTime > exitTime || entryTime < 0f || entryTime > 1f)
            return null;

        // Determine collision normal (the axis where entry occurred)
        Velocity surfaceNormal;
        if (xEntryTime > yEntryTime)
        {
            surfaceNormal = dx > 0f ? new Velocity(-1, 0) : new Velocity(1, 0); // Hit left or right
        }
        else if (yEntryTime > xEntryTime)
        {
            surfaceNormal = dy > 0f ? new Velocity(0, -1) : new Velocity(0, 1); // Hit top or bottom
        }
        else
        {
            // Perfect tie: prefer the axis with larger absolute displacement; fallback vertical
            if (Math.Abs(dx) > Math.Abs(dy))
                surfaceNormal = dx > 0f ? new Velocity(-1, 0) : new Velocity(1, 0);
            else
                surfaceNormal = dy > 0f ? new Velocity(0, -1) : new Velocity(0, 1);
        }

        return new CollisionResult { Time = entryTime * dt, Normal = surfaceNormal };
    }

    public static CollisionResult? SweptAABB_Collision(IActor actor, Tile tile, TimeDelta deltaTime)
    {
        Velocity actorVelocity = actor.Velocity;
        BoundingBox movingBox = actor.GetRoughBoundingBox();
        BoundingBox staticBox = tile.Bounds;
        return SweptAABB(movingBox, actorVelocity, staticBox, deltaTime);
    }
}
