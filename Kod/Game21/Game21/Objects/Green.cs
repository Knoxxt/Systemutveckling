using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21.Utilities;

namespace Game21 // Created by Anton.
{
    class Green : Monster
    {
        
        ReadManager rm;
        Random rnd = new Random();
        public Green(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, ReadManager rm) : base(texture, position, isWall, hitbox)
        {
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            this.rm = rm;
            CurrentPosition = position;

            // Stats
            Health = 100;
            Mana = 100;
            Damage = rnd.Next(5, 15);
            Defense = 2;
        }

        public new void Update(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (IsAlive)
            {
                MovementUpdate(gameTime, rm);
            }
        }

        public void UpdateCombat(GameTime gameTime)
        {
            AttackUpdate(gameTime, rm, rnd);
        }

    }
}
