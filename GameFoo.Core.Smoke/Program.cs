using System;
using GameFoo.Core;
using GameFoo.Core.Primitives;

class Program
{
    static int Main()
    {
        Console.WriteLine("SweptAABB smoke:");
        var moving = new BoundingBox(0, 0, 16, 16);
        var tile = new BoundingBox(0, 30, 16, 16);
        var v = new Velocity(0, 60);
        var dt = TimeDelta.FromSeconds(1);
        var res = CollisionSystem.SweptAABB(moving, v, tile, dt);
        Console.WriteLine($"fall res: {(res == null ? "null" : $"t={res.Value.Time:F3}, n=({res.Value.Normal.X},{res.Value.Normal.Y})")} ");

        var overlap = CollisionSystem.SweptAABB(new BoundingBox(0,0,16,16), new Velocity(0,0), new BoundingBox(8,8,16,16), dt);
        Console.WriteLine($"overlap res: {(overlap == null ? "null" : $"t={overlap.Value.Time:F3}, n=({overlap.Value.Normal.X},{overlap.Value.Normal.Y})")} ");

        var none = CollisionSystem.SweptAABB(new BoundingBox(0,0,16,16), new Velocity(10,0), new BoundingBox(100,100,16,16), dt);
        Console.WriteLine($"none res: {(none == null ? "null" : $"t={none.Value.Time:F3}, n=({none.Value.Normal.X},{none.Value.Normal.Y})")} ");
        return 0;
    }
}

