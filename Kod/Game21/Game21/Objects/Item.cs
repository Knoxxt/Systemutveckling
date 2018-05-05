using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class Item
    {
        public List<Item> itemsInWorld = new List<Item>();
        protected Texture2D texture;
        protected Vector2 position;
        public int damage, defense, intelligence, agility, strength;
        public string name, desc;

        public Item(Texture2D texture, Vector2 position, string name)
        {
            this.texture = texture;
            this.position = position;
            this.name = name;
            
        }
    }
}
