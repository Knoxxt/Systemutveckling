using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class RestorationBlock : Tile //Created by Oscar
    {
        ReadManager rm;
        public int Health { get; set; }
        public double coolDown{ get; set; }
        
        public RestorationBlock(Texture2D texture, Vector2 position, bool isWall, Rectangle hitBox, ReadManager rm) :base(texture, position, isWall, hitBox)
        {
            this.rm = rm;
            Health = 10;
            coolDown = 0;
            
        }

        public void Update(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            coolDown += gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
