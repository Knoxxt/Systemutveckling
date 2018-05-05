using Game21.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class Chests : GameObject
    {
        public Color[] textureData;
        private Texture2D texture;
        private Texture2D texture2;
        private Vector2 position;
        private InventoryManager im;
        private Game1 g;
        public bool isOpen;
        public bool intersectWithHero, giveRewards;
        
        public Chests(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, InventoryManager im, bool isOpen, bool intersectWithHero, Texture2D texture2, Game1 g) : base(texture, position, isWall, hitbox)
        {
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            this.g = g;
            this.texture = texture;
            this.position = position;
            this.im = im;
            this.isOpen = isOpen;
            this.texture2 = texture2;
            this.intersectWithHero = intersectWithHero;
            isOpen = false;
            intersectWithHero = false;
            giveRewards = true;
        }

        public void Update(GameTime gameTime, Interface interfaces)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (intersectWithHero)
            {
                if (giveRewards)
                {
                    int index = g.rnd.Next(im.itemsInWorld.Count);
                    im.AddItem(im.itemsInWorld[index], false, interfaces);
                    giveRewards = false;
                }
                intersectWithHero = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isOpen)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture2, position, Color.White);
            }
        }
    }
}
