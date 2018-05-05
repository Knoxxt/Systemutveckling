using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21.Utilities;

namespace Game21
{
    class CollisionManager // Created by Anton.
    {
        ReadManager rm;
        Game1 g;
        CombatManager com;
        MonsterRace mRace = new MonsterRace();
        public CollisionManager(ReadManager rm, CombatManager com, Game1 g)
        {
            this.rm = rm;
            this.g = g;
            this.com = com;
            mRace = MonsterRace.None;
        }

        public void Update(GameTime gameTime)
        {
            foreach(Hero h in rm.heroes)
            {
                foreach (Tile t in rm.tileList) // Collision checking between hero and tiles.
                {
                    t.Update(gameTime);
                    if (h.hitbox.Intersects(t.hitbox))
                    {
                        if (g.IntersectPixel(h.hitbox, h.textureData, t.hitbox, t.textureData))
                        {
                            h.hasJumped = false;
                            h.velocity.Y = 0f;
                            break;
                        }
                        else
                        {
                            h.hasJumped = true;
                        }
                    }
                    else
                    {
                        h.hasJumped = true;
                    }
                }
                foreach(Monster m in rm.monsters)
                {
                    if (h.hitbox.Intersects(m.hitbox))
                    {
                        if (g.IntersectPixel(h.hitbox, h.textureData, m.hitbox, m.textureData))
                        {
                            if (!com.cmRepeat)
                            {
                                rm.monstersInCombat.Add(m);
                                h.positionBeforeCombat = h.position;
                                if (rm.monstersInCombat.Contains(rm.green))
                                {
                                    mRace = MonsterRace.Goblin;
                                }
                                else if (rm.monstersInCombat.Contains(rm.devil))
                                {
                                    mRace = MonsterRace.Devil;
                                }
                                h.position = h.savedCombatPos;
                                foreach(Monster n in rm.monstersInCombat)
                                {
                                    m.position = m.savedCombatPos;
                                }
                                com.cmRepeat = true;
                            }
                            g.state = GameState.Combat;
                        }
                    }
                }
            }
        }
    }
}
