using System.Collections.Generic;
using GameFoo.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameFoo.Utils;

public class SpriteDictionary : Dictionary<string, ISprite>
{
    private readonly List<SpriteDefinition> _spriteDefinitions =
    [
        new("Player_Idle", new Vector2(48, 48), 10),
        new("Player_Run", new Vector2(48, 48), 8),
        new("Player_Jab", new Vector2(48, 48), 10),
        new("Player_Jump", new Vector2(48, 48), 6),
        new("Player_Hurt", new Vector2(48, 48), 4),
        new("Player_Death", new Vector2(48, 48), 10),
        new("Tile_Grassy", new Vector2(16, 16), 1),
        new("Tile_Dirt", new Vector2(16, 16), 1)
    ];

    public void LoadTextures(ContentManager content)
    {
        foreach (SpriteDefinition definition in _spriteDefinitions)
        {
            Texture2D texture = content.Load<Texture2D>(definition.ContentName);

            if (definition.FrameCount > 1)
            {
                this[definition.ContentName] = new AnimatedSprite(texture, definition.FrameSize, definition.FrameCount);
                continue;
            }

            this[definition.ContentName] = new StaticSprite(texture, definition.FrameSize);
        }
    }
}
