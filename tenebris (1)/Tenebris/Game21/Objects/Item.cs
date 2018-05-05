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
        public int damage, defense, intelligence, agility, strength;
        public string name, desc;

        public Item(string name, string desc, int damage, int defense, int intelligence, int agility, int strength)
        {
            this.name = name;
            this.desc = desc;
            this.damage = damage;
            this.defense = defense;
            this.intelligence = intelligence;
            this.agility = agility;
            this.strength = strength;
            
        }
    }
}
