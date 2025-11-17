using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFoo.Utils;

public class Camera
{
    private readonly Viewport _viewport;

    public Camera(Viewport viewport) => _viewport = viewport;

    public Vector2 Position { get; set; }
    public float Zoom { get; set; } = 2f;
    public float Rotation { get; set; } = 0f;

    // Smoothing factor (0..1). Higher values snap faster.
    public float FollowLerp { get; set; } = 0.2f;

    public void Follow(Vector2 target)
    {
        // Smoothly move towards target to reduce jitter
        Position = Vector2.Lerp(Position, target, FollowLerp);
    }

    public Matrix GetViewMatrix()
    {
        // Snap to the nearest pixel by rounding the camera position *after* applying smoothing
        Vector2 roundedPosition = new(
            (float)Math.Floor(Position.X),
            (float)Math.Floor(Position.Y)
        );

        return
            Matrix.CreateTranslation(new Vector3(-roundedPosition, 0)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(Zoom, Zoom, 1f) *
            Matrix.CreateTranslation(new Vector3(_viewport.Width / 2f, _viewport.Height / 2f, 0));
    }
}
