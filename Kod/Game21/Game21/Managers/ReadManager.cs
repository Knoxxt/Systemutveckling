using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21.Utilities;
using Game21.Objects;

namespace Game21
{
    class ReadManager // Created by Anton
    {
        public Warrior warrior;
        public Mage mage;
        public Goblin green;
        public Devil devil;
        public Tile[,] tiles;
        public List<Tile> tileList = new List<Tile>();
        public List<Hero> heroes = new List<Hero>();
        public List<Monster> monsters = new List<Monster>();
        public List<Monster> monstersInCombat = new List<Monster>();
        public List<GameObject> gameObjects = new List<GameObject>();
        PlayerClass pClass = new PlayerClass();
        
        public ReadManager(Variables var, Game1 g) // Placement of created instances of object.
        {
            pClass = PlayerClass.Mage;
            List<string> strings = new List<string>();
            StreamReader sr = new StreamReader("map.txt");
            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            sr.Close();

            tiles = new Tile[strings[0].Length, strings.Count];
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (strings[j][i] == 'f')
                    {
                        tiles[i, j] = new Tile(var.floor, new Vector2(32 * i, 32 * j), true, var.floorRec);
                        tileList.Add(tiles[i, j]);
                        gameObjects.Add(tiles[i, j]);
                    }
                    else if (strings[j][i] == '-')
                    {

                    }
                    else if (strings[j][i] == 'h')
                    {
                        if(pClass == PlayerClass.Warrior)
                        {
                            warrior = new Warrior(var.hero, new Vector2(32 * i, 32 * j), false, var.heroRec, this, g.Content);
                            heroes.Add(warrior);
                        }
                        else if(pClass == PlayerClass.Mage)
                        {
                            mage = new Mage(var.hero, new Vector2(32 * i, 32 * j), false, var.heroRec, this, g.Content);
                            heroes.Add(mage);
                        }
                    }
                    else if (strings[j][i] == 'g')
                    {
                        green = new Goblin(var.greenMonster, new Vector2(32 * i, 32 * j), false, var.greenMonsterRec, this);
                        monsters.Add(green);
                    }
                    else if (strings[j][i] == 'd')
                    {
                        devil = new Devil(var.greenMonster, new Vector2(32 * i, 32 * j), false, var.greenMonsterRec, this);
                        monsters.Add(devil);
                    }
                }
            }
        }
    }
}
