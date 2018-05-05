using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21;
using Game21.Utilities;
using Game21.Objects;

namespace Game21
{
    class CombatManager
    {
        ReadManager rm;
        Game1 g;
        public bool doThisOnce, doThisOnce1, cmRepeat, levelRepeat;
        private Vector2 positionBeforeCombat;
        private Enums enums;

        public bool DoThisOnce1
        {
            get
            {
                return doThisOnce1;
            }
            set
            {
                doThisOnce1 = value;
            }
        }

        public Vector2 PositionBeforeCombat
        {
            get
            {
                return positionBeforeCombat;
            }
            set
            {
                positionBeforeCombat = value;
            }
        }
        public CombatManager(ReadManager rm, Game1 g, Enums enums)
        {
            this.rm = rm;
            this.g = g;
            this.enums = enums;
            doThisOnce = true;
            doThisOnce1 = true;
            levelRepeat = true;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Hero h in rm.heroes)
            {
                if (h.yourTurn)
                {
                    doThisOnce1 = true;
                    foreach(Monster m in rm.monstersInCombat)
                    {
                        if (m.IsAlive)
                        {
                            if (enums.pClass == PlayerClass.Warrior)
                            {
                                rm.warrior.UpdateCombat(gameTime);
                            }
                            else if(enums.pClass == PlayerClass.Mage)
                            {
                                rm.mage.UpdateCombat(gameTime);
                            }
                        }
                    }
                }
            }
            foreach(Hero h in rm.heroes)
            {
                if (!h.yourTurn)
                {
                    foreach (Monster m in rm.monstersInCombat)
                    {
                        if (m.IsAlive)
                        {
                            if (enums.mRace == MonsterRace.Goblin)
                            {
                                foreach(Goblin g in rm.monstersInCombat)
                                {
                                    g.UpdateCombat(gameTime);
                                }
                            }
                            else if(enums.mRace == MonsterRace.Devil)
                            {
                                foreach(Devil d in rm.monstersInCombat)
                                {
                                    d.UpdateCombat(gameTime);
                                }
                            }
                        }
                    }
                }
            }

            foreach (Monster m in rm.monstersInCombat)
            {
                if (KeymouseReader.KeyPressed(Keys.R))
                {
                    m.IsAlive = false;
                }

                if (m.Health <= 0)
                {
                    m.IsAlive = false;
                }

                if (!m.IsAlive && rm.monstersInCombat.Count != 0)
                {
                    foreach (Hero h in rm.heroes)
                    {
                        h.position = h.positionBeforeCombat;
                    }
                    //rm.monstersInCombat.RemoveAll(P => m.IsAlive = false);

                    ResetCombat();
                    enums.mRace = MonsterRace.None;
                }
            }


        }

        public void ResetCombat()
        {
            cmRepeat = false;
            doThisOnce = true;
            doThisOnce1 = true;
            //rm.warrior.healingRecharged = true;

            
            foreach(Hero h in rm.heroes)
            {
                h.ManaCostOnce = true;
                h.Round = 0;
                h.yourTurn = true;
            }
        }
    }
}
