using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21;
using Game21.Utilities;

namespace Game21
{
    class CombatManager
    {
        ReadManager rm;
        Game1 g;
        public bool doThisOnce, doThisOnce1, cmRepeat, levelRepeat;
        private Vector2 positionBeforeCombat;
        PlayerClass pClass = new PlayerClass();
        MonsterRace mRace = new MonsterRace();

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
        public CombatManager(ReadManager rm, Game1 g)
        {
            this.rm = rm;
            this.g = g;
            doThisOnce = true;
            doThisOnce1 = true;
            levelRepeat = true;
            pClass = PlayerClass.Mage;
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
                            if (pClass == PlayerClass.Warrior)
                            {
                                rm.warrior.UpdateCombat(gameTime);
                            }
                            else if(pClass == PlayerClass.Mage)
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
                            if (mRace == MonsterRace.Goblin)
                            {
                                rm.green.UpdateCombat(gameTime);
                            }
                            else if(mRace == MonsterRace.Devil)
                            {
                                rm.devil.UpdateCombat(gameTime);
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
                    mRace = MonsterRace.None;
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
                h.mana = h.maxMana;
                h.health = h.maxHealth;
            }
        }
    }
}
