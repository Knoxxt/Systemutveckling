using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class DynamicSpikeTrap : DynamicGameObject
    {
        public int Damage { get; set; }
        ReadManager rm;
        public DynamicSpikeTrap(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, Texture2D fakeHitbox, ReadManager rm) : base(texture, position, isWall, hitbox, fakeHitbox)
        {
            textureData = new Color[fakeHitbox.Width * fakeHitbox.Height];
            fakeHitbox.GetData(textureData);
            frameTimer = 500;
            frameInterval = 500;
            rotation = 0;
            frame = 6;
            Damage = 0;
            this.rm = rm;
            this.
            sourceRectangle = new Rectangle(0, 0, 32, 32);
        }

        public void Update(GameTime gameTime)
        {
            Damage = damage;
            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            UpdateAnimation(gameTime, 6, 32, true);
        }
    }
}
