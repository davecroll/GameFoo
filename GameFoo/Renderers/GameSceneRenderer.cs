using System.Collections.Generic;
using GameFoo.Core.Actors;
using GameFoo.Core.Interfaces;
using GameFoo.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BoundingBox = GameFoo.Core.Primitives.BoundingBox;

namespace GameFoo.Renderers;

public class GameSceneRenderer
{
    public void Render(IEnumerable<ISceneEntity> sceneEntities, SpriteBatch spriteBatch,
        Dictionary<string, ISprite> spriteDictionary)
    {
        foreach (ISceneEntity entity in sceneEntities)
        {
            string renderId = entity.RenderId;
            ISprite sprite = spriteDictionary[renderId];

            // using the Bounds property since they are in pixel space for all entities.
            Vector2 position = new(entity.Bounds.X, entity.Bounds.Y);

            int frameIndex = 0;
            SpriteEffects effects = SpriteEffects.None;
            if (entity is IActor actor)
            {
                frameIndex = actor.State.FrameIndex;
                effects = actor.Orientation == ActorOrientation.Left
                    ? SpriteEffects.FlipHorizontally
                    : SpriteEffects.None;
            }

            (Texture2D, Rectangle) frame = sprite.GetFrame(frameIndex);

            spriteBatch.Draw(
                frame.Item1,
                position,
                frame.Item2,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                effects,
                0f
            );

            // Debug code below
            if (entity is ActorBase actorFoo)
            {
                BoundingBox? box = actorFoo.GetHitbox();
                if (box == null)
                {
                    continue;
                }

                Rectangle rect = new(box.Value.X, box.Value.Y, box.Value.Width, box.Value.Height);

                // using tile texture because it works.
                ISprite spriteFoo = spriteDictionary["Tile_Dirt"];

                spriteBatch.Draw(spriteFoo.GetFrame(0).Item1, rect, Color.Red);
            }
        }
    }
}
