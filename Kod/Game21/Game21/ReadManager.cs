using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class ReadManager
    {
        public Hero hero;
        public Green green;
        public Tile[,] tiles;
        public List<Tile> tileList = new List<Tile>();
        public List<GameObject> gameObjects = new List<GameObject>();

        public ReadManager(Variables var)
        {
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
                        hero = new Hero(var.hero, new Vector2(32 * i, 32 * j), false, var.heroRec);
                    }
                    else if (strings[j][i] == 'g')
                    {
                        green = new Green(var.greenMonster, new Vector2(32 * i, 32 * j), false, var.greenMonsterRec);
                    }
                }
            }
        }
    }
}
