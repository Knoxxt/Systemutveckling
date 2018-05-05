﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Tile : GameObject
    {
        public Color[] textureData;
        public bool Iswall => isWall;

        public Tile(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox) : base(texture, position, isWall, hitbox)
        {
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            
        }

        public new void Update(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }


        public new void Draw(SpriteBatch spriteBatch)
        {
            if (!isWall)
            {
                spriteBatch.Draw(texture, position, Color.Transparent);
            }
            if (isWall)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
