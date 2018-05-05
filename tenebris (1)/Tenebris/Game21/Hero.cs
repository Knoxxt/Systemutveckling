using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Hero : GameObject
    {
        public Color[] textureData;
        public Vector2 velocity;
        public bool hasJumped;

        public Hero(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox) : base(texture, position, isWall, hitbox)
        {
            hasJumped = true;
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

        public new void Update(GameTime gameTime)
        {
            position += velocity;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (KeymouseReader.KeyPressed(Keys.Left) || KeymouseReader.KeyPressed(Keys.A))
            {
                velocity.X = -2;
            }
            else if (KeymouseReader.KeyPressed(Keys.Right) || KeymouseReader.KeyPressed(Keys.D))
            {
                velocity.X = 2;
            }
            else if ((KeymouseReader.KeyPressed(Keys.Up) || KeymouseReader.KeyPressed(Keys.W)) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }
            else
            {
                velocity.X = 0;
            }
            float i = 1;
            velocity.Y += 0.15f * 1;


        }
    }
}
