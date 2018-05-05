using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    abstract class GameObject // Created by Anton.
    {
        public Vector2 position;
        public Texture2D texture;
        protected bool isWall; // Is it walkable or not.
        public Rectangle hitbox;


        protected GameObject(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox)
        {
            this.texture = texture;
            this.position = position;
            this.isWall = isWall;
            this.hitbox = hitbox;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
