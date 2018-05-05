using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Green : Monster
    {
        private float startPosition;
        public Green(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox) : base(texture, position, isWall, hitbox)
        {
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            startPosition = position.X;
        }

        public new void Update(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            position += velocity;

            if(startPosition == position.X)
            {
                velocity.X = -2;
            }
            if(position.X <= startPosition - (texture.Width * 3))
            {
                velocity.X = 2;
            }
            if(position.X >= startPosition + (texture.Width * 3))
            {
                velocity.X = -2;
            }
        }
    }
}
