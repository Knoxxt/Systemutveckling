using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21.Utilities;
using Game21.Objects;
using Microsoft.Xna.Framework.Graphics;

namespace Game21
{
    class CollisionManager // Created by Anton.
    {
        ReadManager rm;
        Game1 g;
        CombatManager com;
        private Enums enums;
        

        public CollisionManager(ReadManager rm, CombatManager com, Game1 g, Enums enums)
        {
            this.rm = rm;
            this.g = g;
            this.com = com;
            this.enums = enums;
        }

        public void Update(GameTime gameTime)
        {
            foreach(Hero h in rm.heroes)
            {
                foreach(DynamicSpikeTrap dpt in rm.spikeList)
                {
                    if(g.IntersectPixel(h.hitbox, h.textureData, dpt.hitbox, dpt.textureData))
                    {
                        h.health -= dpt.Damage;
                    }
                }
                foreach(Tile it in rm.invisibleTiles)
                {
                    if (g.IntersectPixel(h.hitbox, h.textureData, it.hitbox, it.textureData))
                    {
                        it.isWall = true;
                        h.hasJumped = false;
                        h.velocity.Y = 0f;
                        break;
                    }
                }
                foreach(RestorationBlock r in rm.restoList)
                {
                    r.Update(gameTime);         
                    if(g.IntersectPixel(h.hitbox, h.textureData, r.hitbox, r.textureData))
                    {                        
                        h.hasJumped = false;
                        h.velocity.Y = 0f;

                        if(r.coolDown > 10 & (g.IntersectPixel(h.hitbox, h.textureData, r.hitbox, r.textureData)) & h.health > h.maxHealth) //Checking cd, if the hero intersects with the tile and if the heroes hp is under hero max hp
                        {                                                                                                                   // and if the heroes hp is under hero max hp

                            h.health += r.Health;
                            r.coolDown = 0;
                        }
                        break;
                    }
                }
                foreach(Lever l in rm.leverList)
                {
                    if(g.IntersectPixel(h.hitbox, h.textureData, l.hitbox, l.textureData))
                    {
                        l.pushed = true;
                    }
                }
                foreach (Tile t in rm.tileList) // Collision checking between hero and tiles.
                {
                    t.Update(gameTime);
                    if (g.IntersectPixel(h.hitbox, h.textureData, t.hitbox, t.textureData))
                    {
                        h.hasJumped = false;
                        h.velocity.Y = 0f;
                        break;
                    }
                }
                foreach(Monster m in rm.monsters)
                {
                        if (g.IntersectPixel(h.hitbox, h.textureData, m.hitbox, m.textureData))
                        {
                            if (!com.cmRepeat)
                            {
                                rm.monstersInCombat.Add(m);
                                h.positionBeforeCombat = h.position;
                                if (rm.monstersInCombat.Contains(rm.green))
                                {
                                    enums.mRace = MonsterRace.Goblin;
                                }
                                else if (rm.monstersInCombat.Contains(rm.devil))
                                {
                                    enums.mRace = MonsterRace.Devil;
                                }
                                h.position = h.savedCombatPos;
                                foreach(Monster n in rm.monstersInCombat)
                                {
                                    m.position = m.savedCombatPos;
                                }
                                com.cmRepeat = true;
                            }
                            enums.gState = GameState.Combat;
                        }
                }

                foreach(Chests c in rm.chestList)
                {
                    if (h.hitbox.Intersects(c.hitbox))
                    {
                        if(g.IntersectPixel(h.hitbox, h.textureData, c.hitbox, c.textureData))
                        {
                            c.intersectWithHero = true;
                            c.isOpen = true;
                        }
                    }
                }
            }
        }
    }
}
