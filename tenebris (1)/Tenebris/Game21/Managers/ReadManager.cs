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
using Game21.Managers;

namespace Game21
{
    class ReadManager // Created by Anton
    {
        public Warrior warrior;
        public Mage mage;
        public Goblin green;
        public Devil devil;
        public Chests chest;
        public DynamicSpikeTrap dSpikeTrap;
        public RestorationBlock rBlock;
        public Lever lever;

        public Tile[,] tiles;
        public List<Tile> tileList = new List<Tile>();
        public List<Tile> invisibleTiles = new List<Tile>();
        public List<Lever> leverList = new List<Lever>();
        public List<RestorationBlock> restoList = new List<RestorationBlock>(); 
        public List<Hero> heroes = new List<Hero>();
        public List<Monster> monsters = new List<Monster>();
        public List<Monster> monstersInCombat = new List<Monster>();
        public List<DynamicSpikeTrap> spikeList = new List<DynamicSpikeTrap>();
        public List<DynamicGameObject> dGameObjects = new List<DynamicGameObject>();
        public List<GameObject> gameObjects = new List<GameObject>();
        public List<Chests> chestList = new List<Chests>();
        public InventoryManager im;
        private Enums enums;
        PlayerClass tempPClass = new PlayerClass();
        public ReadManager(Variables var, Game1 g, Enums enums) // Placement of created instances of object.
        {
            this.enums = enums;
            enums.pClass = PlayerClass.Mage;
            im = new InventoryManager(this);
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
                        if (enums.pClass == PlayerClass.Warrior)
                        {
                            warrior = new Warrior(var.hero, new Vector2(32 * i, 32 * j), false, var.heroRec, this, g.Content, var.fakeTextureHero);
                            heroes.Add(warrior);
                        }
                        else if(enums.pClass == PlayerClass.Mage)
                        {
                            mage = new Mage(var.hero, new Vector2(32 * i, 32 * j), false, var.heroRec, this, g.Content, var.fakeTextureHero);
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
                    else if (strings[j][i] == 'c')
                    {
                        chest = new Chests(var.chestClosed, new Vector2(32 * i, 32 * j), false, var.chestRec, im, false, false, var.chestOpen, g);
                        chestList.Add(chest);
                    }
                    else if (strings[j][i] == 'i')
                    {
                        tiles[i, j] = new Tile(var.floor, new Vector2(32 * i, 32 * j), false, var.floorRec);
                        invisibleTiles.Add(tiles[i, j]);
                    }
                    else if (strings[j][i] == 's')
                    {
                        dSpikeTrap = new DynamicSpikeTrap(var.spikeTrapSS, new Vector2(32 * i, (32 * j) + 15), false, var.spikeRec, var.fakeTextureSpike, this);
                        spikeList.Add(dSpikeTrap);
                        dGameObjects.Add(dSpikeTrap);
                    }
                    else if (strings[j][i] == 'r')
                    {
                        rBlock = new RestorationBlock(var.restoFloor, new Vector2(32 * i, 32 * j), true, var.floorRec, this);
                        restoList.Add(rBlock);
                        gameObjects.Add(rBlock);
                    }
                    else if (strings[j][i] == 'l')
                    {
                        lever = new Lever(var.leverTex, new Vector2(32 * i, (32 * j) + 15), false, var.leverRec);
                        leverList.Add(lever);
                        gameObjects.Add(lever);
                    }
                }
            }
        }
    }
}
