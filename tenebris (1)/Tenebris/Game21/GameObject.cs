using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    abstract class GameObject
    {
        public Vector2 position;
        public Texture2D texture;
        protected bool isWall;
        public Rectangle hitbox;


        protected GameObject(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox)
        {
            this.texture = texture;
            this.position = position;
            this.isWall = isWall;
            this.hitbox = hitbox;
        }

        protected void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
