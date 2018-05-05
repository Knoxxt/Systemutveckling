using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class Devil : Monster
    {
        ReadManager rm;
        Random rnd = new Random();
        public Devil(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, ReadManager rm) : base(texture, position, isWall, hitbox, rm)
        {
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            this.rm = rm;
            CurrentPosition = position;

            // Stats
            Health = 150;
            Mana = 100;
            Damage = rnd.Next(5, 35);
            Defense = 3;
            ExpGiven = 400;
            MaxSpeed = 4;
        }

        public void UpdateCombat(GameTime gameTime)
        {

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            AttackUpdate(gameTime, rm);
        }
    }
}
