using Game21.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class Mage : Hero
    {
        private int roundDuration, roundEffectEnd;
        private float drawTime;
        public bool doThisOnce, healingRecharged;

        ReadManager rm;
        public Mage(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, ReadManager rm, ContentManager content) : base(texture, position, isWall, hitbox, rm, content)
        {
            // Booleans
            doThisOnce = true;
            ManaCostOnce = true;
            healingRecharged = true;
            drawParticles = false;
            loadParticles = false;
            // ints and floats
            roundDuration = 3;
            roundEffectEnd = 100;
            // Extra
            this.rm = rm;
            // Stats
            maxHealth = 80;
            health = 80;
            maxMana = 120;
            mana = 120;
            strength = 5;
            intelligence = 15;
            agility = 10;
            spellDamage = 0;
            // Spells
            spell1Name = "Wizard's blessing";
            spell2Name = "Flame Rush";
            spell3Name = "Fireball";
            spell4Name = "Ice Block";
            spell1Desc = "Pray to the God of Wisdom and Magic, Hecate. Allowing you to regenerate 20 mana.";
            spell2Desc = "Catch your opponent off guard with a quick attack, doing 15 + (20 * spell rank) damage.";
            spell3Desc = "hej";
            spell4Desc = "Encase yourself in solid ice, blocking any and all enemy attacks for one turn";
            manaCost1 = 15;
            manaCost2 = 30;
            manaCost3 = 20;
            manaCost4 = 80;
        }

        public void UpdateCombat(GameTime gameTime)
        {
            
            damage = 10 + spellDamage + (int)((strength / 10) * rnd.Next(1, 3));
            if (attack)
            {
                spellDamage = 0;
                Attack(attack, 0.03f);
            }
            else if (spell1)
            {
                WizardsBlessing(gameTime);
            }
            else if (spell2)
            {
                FlameRush(gameTime);
            }
            else if (spell3)
            {
                Fireball();
            }
            else if (spell4)
            {
                IceBlock();
            }

            if (Round == roundEffectEnd)
            {
                ResetBuff();
            }
            if(pe != null)
            {
                pe.Update();
            }
        }

        private void WizardsBlessing(GameTime gameTime)
        {
            intelligence = intelligence + (spell1Rank * 15);
            if (ManaCostOnce)
            {
                loadParticles = true;
                drawTime = 0;
                mana -= manaCost1;
                ManaCostOnce = false;
            }
            pe.EmitterLocation = position + new Vector2(110, 730);
            OnSelfParticles(pe, "fireparticle", 1, gameTime, true);
            if (doThisOnce)
            {
                roundEffectEnd = Round + roundDuration;
                doThisOnce = false;
            }
        }

        private void FlameRush(GameTime gameTime)
        {
            spellDamage = 5 + (spell2Rank * 20);
            Attack(spell2, 0.5f);
            if (ManaCostOnce)
            {
                loadParticles = true;
                mana -= manaCost2;
                ManaCostOnce = false;
            }
            OnSelfParticles(pe, "fireparticle", 1.5f, gameTime, false);
        }

        private void Fireball()
        {
            if (ManaCostOnce)
            {
                pe.EmitterLocation = new Vector2(200, 670);
                loadParticles = true;
                mana -= manaCost3;
                ManaCostOnce = false;
            }
            RangedParticles(pe, "fireparticle", new Vector2(8, 0), false, 25);
        }

        private void IceBlock() // fixa
        {

        }

        public void ResetBuff()
        {
            if (!doThisOnce)
            {
                intelligence = intelligence - (spell1Rank * 15);
                doThisOnce = true;
            }
        }
    }
}
