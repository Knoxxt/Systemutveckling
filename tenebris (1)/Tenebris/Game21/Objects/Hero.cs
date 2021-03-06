﻿using Game21.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Hero : GameObject // Created by Anton.
    {
        public Color[] textureData;
        public Vector2 velocity, centre, savedCombatPos, positionBeforeCombat;
        public Texture2D fakeHitbox;
        private Rectangle sourceRectangle;
        private ReadManager rm;
        public bool hasJumped, yourTurn, attack, combatMovement, spell1, spell2, spell3, spell4, drawParticles, loadParticles;
        private float rotation, acceleration;
        public float drawTime;
        protected int size, spellDamage, frame;
        protected double frameTimer, frameInterval, deltaTime;
        public int manaCost1, manaCost2, manaCost3, manaCost4;
        private SpriteEffects heroFX;
        PlayerClass pClass = new PlayerClass();
        protected Random rnd;
        private int round = 0;
        protected List<Texture2D> particleTextures = new List<Texture2D>();
        protected ParticleEngine pe;
        protected ContentManager content;

        public int Round
        {
            get
            {
                return round;
            }
            set
            {
                round = value;
            }
        }

        // Stats
        public int health, maxHealth, mana, maxMana, damage, speed, defense, currentExp, maxExp, level, attributePoint, skillPoint;
        public float strength, intelligence, agility;

        // Spells
        public int spell1Rank, spell2Rank, spell3Rank, spell4Rank;
        public string spell1Name, spell2Name, spell3Name, spell4Name, 
            spell1Desc, spell2Desc, spell3Desc, spell4Desc;
        public bool ManaCostOnce { get; set; }

        public Hero(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, ReadManager rm, ContentManager content, Texture2D fakeTexture) : base(texture, position, isWall, hitbox)
        {
            // TextureData for pixel collision
            textureData = new Color[fakeTexture.Width * fakeTexture.Height];
            fakeTexture.GetData(textureData);
            // Vectors
            centre = new Vector2(5, 5);
            savedCombatPos = new Vector2(0, 74);
            // Booleans
            hasJumped = true;
            attack = false;
            yourTurn = true;
            combatMovement = false;
            spell1 = false;
            spell3 = false;
            spell4 = false;
            loadParticles = true;
            drawParticles = false;
            // ints and floats
            rotation = 0;
            acceleration = 2;
            size = 1;
            frame = 3;
            frameTimer = 100;
            frameInterval = 100;
            // Extra
            rnd = new Random();
            this.rm = rm;
            this.content = content;
            this.fakeHitbox = fakeTexture;
            pClass = PlayerClass.Mage;

            // Stats
            damage = 10 + (int)((strength / 10) * rnd.Next(1, 3));
            speed = 2 + (int)(agility * 0.1);
            level = 1;
            currentExp = 0;
            maxExp = 200 + (level * 200);
            skillPoint = 0;
            // Skills
            spell1Rank = 1;
            spell2Rank = 1;
            spell3Rank = 1;
            spell4Rank = 1;
            // Animation
            particleTextures.Add(content.Load<Texture2D>("floor"));
            pe = new ParticleEngine(particleTextures, position + new Vector2(110, 730));
        }

        protected void frameUpdate()
        {
            if (frame == 1)
            {
                sourceRectangle = new Rectangle(6, 2, 57, 61);
            }
            else if (frame == 2)
            {
                sourceRectangle = new Rectangle(75, 0, 56, 60);
            }
            else if (frame == 3)
            {
                sourceRectangle = new Rectangle(138, 5, 54, 61);
            }
            else if (frame == 4)
            {
                sourceRectangle = new Rectangle(199, 5, 59, 57);
            }
            else if (frame == 5)
            {
                sourceRectangle = new Rectangle(266, 9, 56, 56);
            }
            else if (frame == 6)
            {
                sourceRectangle = new Rectangle(327, 4, 58, 59);
            }
            else if (frame == 7)
            {
                sourceRectangle = new Rectangle(392, 6, 62, 58);
            }

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                if (frame == 7)
                {
                    frame = 3;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            speed = 2 + (int)(agility * 0.1);
            LevelGain();
            if(pClass == PlayerClass.Mage)
            {

            }
            else if (pClass == PlayerClass.Warrior)
            {
                foreach(Warrior w in rm.heroes)
                {
                    w.ResetBuff();
                }
            }

            position += velocity;
            hitbox = new Rectangle((int)position.X, (int)position.Y, fakeHitbox.Width - 25, fakeHitbox.Height - 6);
            // Movement.
            if (KeymouseReader.KeyPressed(Keys.Left) || KeymouseReader.KeyPressed(Keys.A))
            {
                deltaTime += gameTime.ElapsedGameTime.TotalSeconds;
                rotation = MathHelper.ToRadians(0);
                velocity.X = -speed;
                heroFX = SpriteEffects.FlipHorizontally;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if ((KeymouseReader.KeyPressed(Keys.Up) || KeymouseReader.KeyPressed(Keys.W)) && hasJumped == false)
                {
                    position.Y -= 10f;
                    velocity.Y = -5f;
                    hasJumped = true;
                }
            }
            else if (KeymouseReader.KeyPressed(Keys.Right) || KeymouseReader.KeyPressed(Keys.D))
            {
                deltaTime += gameTime.ElapsedGameTime.TotalSeconds;
                rotation = MathHelper.ToRadians(0);
                velocity.X = speed;
                heroFX = SpriteEffects.None;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if((KeymouseReader.KeyPressed(Keys.Up) || KeymouseReader.KeyPressed(Keys.W)) && hasJumped == false)
                {
                    position.Y -= 10f;
                    velocity.Y = -5f;
                    hasJumped = true;
                }
            }
            else if ((KeymouseReader.KeyPressed(Keys.Up) || KeymouseReader.KeyPressed(Keys.W)) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -5f;
                hasJumped = true;
            }
            else if ((KeymouseReader.KeyPressed(Keys.Up) || KeymouseReader.KeyPressed(Keys.W)))
            {
                frame = 2;
            }
            else // Gravity.
            {
                velocity.X = 0;
                frame = 1;
            }
            float i = 1;
            velocity.Y += 0.15f * 1;

            frameUpdate();
        }

        protected void Attack(bool skillUsed, float accelerationIncrease, GameTime gameTime) // needs fixing
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            LevelGain();
            foreach(Monster m in rm.monstersInCombat)
            {
                if (position.X <= savedCombatPos.X)
                {
                    if (combatMovement)
                    {
                        yourTurn = false;
                        skillUsed = false;
                        acceleration = 2;
                        attack = skillUsed;
                        spell1 = skillUsed;
                        spell2 = skillUsed;
                        spell3 = skillUsed;
                        spell4 = skillUsed;
                        
                    }
                    combatMovement = false;
                }
                else if (position.X >= (m.position.X) - fakeHitbox.Width)
                {
                    combatMovement = true;
                    m.Health -= (damage - (m.Defense / 2));

                }

                frameUpdate();
            }

            acceleration += accelerationIncrease;
            float currentSpeed;
            if (combatMovement)
            {
                heroFX = SpriteEffects.FlipHorizontally;
                currentSpeed = -2 * acceleration;
                position.X += currentSpeed;
            }
            else
            {
                currentSpeed = 2 * acceleration;
                position.X += currentSpeed;
                heroFX = SpriteEffects.None;
            }

        }

        private void LevelGain()
        {
            int expSaved = currentExp - maxExp;
            if(currentExp >= maxExp)
            {
                currentExp = expSaved;
                level += 1;
                attributePoint += 5;
                skillPoint += 1;
            }
        }

        public void ReturnParticlePosition()
        {
            pe.EmitterLocation = new Vector2(200, 670);
        }

        public void RangedParticles(ParticleEngine pe, String particleTexture, Vector2 particleSpeed, bool drawOnly, int spellDamage1)
        {
            spellDamage = spellDamage1;
            if (loadParticles)
            {
                if (particleTextures.Count != 0)
                {
                    particleTextures.Clear();
                    particleTextures.Add(content.Load<Texture2D>(particleTexture));
                }
                drawParticles = true;
                loadParticles = false;
            }
            pe.EmitterLocation += particleSpeed;
            foreach (Monster m in rm.monstersInCombat)
            {
                if (pe.EmitterLocation.X >= 1800)
                {
                    drawParticles = false;
                    ReturnParticlePosition();
                    if (!drawOnly)
                    {
                        m.Health -= (damage - (m.Defense / 2));
                        yourTurn = false;
                        spell1 = false;
                        spell2 = false;
                        spell3 = false;
                        spell4 = false;
                    }
                }
            }



        }
        public void OnSelfParticles(ParticleEngine pe, String particleTexture, float animationDuration, GameTime gameTime, bool expire)
        {
            drawTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (loadParticles)
            {
                if (particleTextures.Count != 0)
                {
                    particleTextures.Clear();
                    particleTextures.Add(content.Load<Texture2D>(particleTexture));
                }
                drawParticles = true;
                loadParticles = false;
            }
            pe.EmitterLocation = position + new Vector2(110, 730);

            if (expire)
            {
                if (drawTime >= animationDuration)
                {
                    drawParticles = false;
                    yourTurn = false;
                    spell1 = false;
                    spell2 = false;
                    spell3 = false;
                    spell4 = false;
                }
            }
            else
            {
                if (combatMovement)
                {
                    drawParticles = false;
                }
            }
        }

        public void DrawParticles(SpriteBatch spriteBatch)
        {
            if (drawParticles)
            {
                pe.Draw(spriteBatch);
            }
        }



        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, rotation, new Vector2(16, 16), size, heroFX, 1);
        }
    }
}
