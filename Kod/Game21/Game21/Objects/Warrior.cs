using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Warrior : Hero
    {
        private int roundDuration, roundEffectEnd;
        public bool doThisOnce, healingRecharged;
       

        ReadManager rm;
        public Warrior(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, ReadManager rm, ContentManager content) : base(texture, position, isWall, hitbox, rm, content)
        {
            // Booleans
            doThisOnce = true;
            ManaCostOnce = true;
            healingRecharged = true;
            // ints and floats
            roundDuration = 3;
            roundEffectEnd = 100;
            // Extra
            this.rm = rm;
            // Stats
            maxHealth = 120;
            health = 120;
            maxMana = 80;
            mana = 80;
            strength = 15;
            intelligence = 5;
            agility = 10;
            spellDamage = 0;
            // Spells
            spell1Name = "Warrior's Blessing";
            spell2Name = "Quick Attack";
            spell3Name = "Battle Joy";
            spell4Name = "Ultimate";
            spell1Desc = "Pray to the God of War Ares, and recieve his boon. Double your size and increase your strength by 15 * spell rank.";
            spell2Desc = "Catch your opponent off guard with a quick attack, doing 15 + (20 * spell rank) damage.";
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
                if (doThisOnce)
                {
                    roundEffectEnd = Round + roundDuration;
                    doThisOnce = false;
                }
                StrengthBuff();
            }
            else if (spell2)
            {
                QuickAttack(gameTime);
            }
            else if (spell3)
            {
                BattleJoy(gameTime);
            }
            else if (spell4)
            {

            }

            if(Round == roundEffectEnd)
            {
                ResetBuff();
            }
            if (pe != null)
            {
                pe.Update();
            }
        }

        private void StrengthBuff()
        {
            size = 2;
            strength = strength + (spell1Rank * 15);
            position.Y -= 55;
            yourTurn = false;
            spell1 = false;
            if (ManaCostOnce)
            {
                mana -= manaCost1;
                ManaCostOnce = false;
            }
        }

        private void QuickAttack(GameTime gameTime)
        {
            spellDamage = 5 + (spell2Rank * 20);
            Attack(spell2, 0.5f);
            if (ManaCostOnce)
            {
                loadParticles = true;
                mana -= manaCost2;
                ManaCostOnce = false;
            }
            OnSelfParticles(pe, "smokeParticle", 1.5f, gameTime, false);
        }

        private void BattleJoy(GameTime gameTime)
        {
            if (ManaCostOnce)
            {
                drawTime = 0;
                drawParticles = true;
                health += spell3Rank * 20;
                if(health > maxHealth)
                {
                    health = maxHealth;
                }
                mana -= manaCost3;
                ManaCostOnce = false;
            }
            OnSelfParticles(pe, "smokeParticle", 1, gameTime, true);
        }

        private void Ultimate() // fixa
        {

        }

        public void ResetBuff()
        {
            if (!doThisOnce)
            {
                size = 1;
                strength = strength - (spell1Rank * 15);
                if(position != savedCombatPos)
                {
                    position.Y += 55;
                }
                doThisOnce = true;
            }
        }
    }
}
