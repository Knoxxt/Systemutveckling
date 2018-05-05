using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class Lever : GameObject
    {
        public bool pushed { get; set; }
        public Color[] textureData;

        public Lever(Texture2D texture, Vector2 position, bool isWall, Rectangle hitBox) : base(texture, position, isWall, hitBox)
        {
            pushed = false;
            textureData = new Color[hitbox.Width * hitbox.Height];
        }

        public void Update(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }

        //protected void Draw(SpriteBatch spriteBatch)
        //{
        //    if(pushed == true)
        //    {
        //        spriteBatch.Draw(texture, position, hitbox, Color.White, 0f, new Vector2(16, 16), 1, SpriteEffects.FlipHorizontally, 1);
        //    }
        //    else
        //    {
        //        spriteBatch.Draw(texture, position, Color.White);
        //    }
        //}
    }
}
