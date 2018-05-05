using Game21.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Managers
{
    class InventoryManager
    {
        public List<Item> itemsInInventory = new List<Item>();
        public List<Item> itemsEquipped = new List<Item>();
        public bool inventoryFull, equipmentFull;
        ReadManager rm;

        public InventoryManager(ReadManager rm)
        {
            this.rm = rm;
        }

        public void Update(GameTime gameTime)
        {
            foreach(Item i in itemsEquipped)
            {

            }
        }

        public void AddItem(Item item, bool inventory)
        {
            if (inventory)
            {
                if (itemsInInventory.Count <= 10)
                {
                    itemsInInventory.Add(item);
                    inventoryFull = false;
                }
                else
                {
                    inventoryFull = true;
                }
            }
            else
            {
                if (itemsEquipped.Count <= 4)
                {
                    itemsEquipped.Add(item);
                    equipmentFull = false;
                    foreach(Hero h in rm.heroes)
                    {
                        h.agility += item.agility;
                        h.strength += item.strength;
                        h.intelligence += item.intelligence;
                        h.damage += item.damage;
                    }
                }
                else
                {
                    equipmentFull = true;
                }
            }
        }

        public void RemoveItem(Item item, bool inventory)
        {
            if (inventory)
            {
                if (inventoryFull)
                {
                    if (itemsInInventory.Contains(item))
                    {
                        itemsInInventory.Remove(item);
                    }
                }
            }
            else
            {
                if (equipmentFull)
                {
                    if (itemsEquipped.Contains(item))
                    {
                        foreach(Hero h in rm.heroes)
                        {
                            h.agility -= item.agility;
                            h.strength -= item.strength;
                            h.intelligence -= item.intelligence;
                            h.damage -= item.damage;
                        }
                        itemsEquipped.Remove(item);
                    }
                }
            }
        }
    }
}
