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
        public List<Item> itemsInWorld = new List<Item>();
        public List<Item> itemsInInventory = new List<Item>();
        public List<Item> itemsEquipped = new List<Item>();
        public bool inventoryFull, equipmentFull;
        private ReadManager rm;

        // Items
        public Item runeOfWind;
        public Item runeOfFire;
        public Item runeOfEarth;

        public InventoryManager(ReadManager rm)
        {
            this.rm = rm;
            equipmentFull = false;
            inventoryFull = false;

            // Items
            runeOfEarth = new Item("Rune of Earth", "A rune molded from the deepest part of the earth itself. Grants the user 10 strength", 0, 0, 0, 0, 10);
            runeOfFire = new Item("Rune of Fire", "A rune molded from the fire of an active volcano. Grants the user 10 intelligence", 0, 0, 10, 0, 0);
            runeOfWind = new Item("Rune of Wind", "A rune that drifts in the wind, with zero resistance. Grants the user 10 agility", 0, 0, 0, 10, 0);
            itemsInWorld.Add(runeOfEarth);
            itemsInWorld.Add(runeOfFire);
            itemsInWorld.Add(runeOfWind);
        }

        public void Update(GameTime gameTime)
        {
            if(itemsEquipped.Count != 0)
            {
                if(itemsEquipped.Count <= 4)
                {
                    equipmentFull = true;
                }
            }
        }

        public void AddItem(Item item, bool inventory, Interface interfaces)
        {
            if (inventory)
            {
                itemsInInventory.Add(item);
                interfaces.inventoryItems.AddItem(item.name);
            }
            else
            {
                if (!equipmentFull)
                {
                    itemsEquipped.Add(item);
                    interfaces.equipmentItems.AddItem(item.name);
                    foreach (Hero h in rm.heroes)
                    {
                        h.agility += item.agility;
                        h.strength += item.strength;
                        h.intelligence += item.intelligence;
                        h.damage += item.damage;
                    }
                }
                else
                {
                    itemsInInventory.Add(item);
                    interfaces.inventoryItems.AddItem(item.name);
                }
            }
        }

        public void RemoveItem(Item item, bool inventory)
        {
            if (inventory)
            {
                if (itemsInInventory.Contains(item))
                {
                    itemsInInventory.Remove(item);
                }
            }
            else
            {
                if (itemsEquipped.Contains(item))
                {
                    foreach (Hero h in rm.heroes)
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
