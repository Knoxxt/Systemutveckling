using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21.Utilities;

namespace Game21
{
    abstract class Monster : GameObject // Created by Anton.
    {
        public Color[] textureData;
        private Vector2 currentPosition;
        public Vector2 savedCombatPos;
        private int health, maxHealth, mana, defense, damage, id, expGiven;
        private float maxSpeed, chaseDistance, hysteresis, acceleration;
        private Vector2 centre;
        public bool isAlive, wanderMovement, combatMovement;
        public AiState state = AiState.Wander;
        public GameState gState = new GameState();
        private ReadManager rm;

        public int ExpGiven
        {
            get
            {
                return expGiven;
            }
            set
            {
                expGiven = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                maxHealth = value;
            }
        }

        public Vector2 CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                currentPosition = value;
            }
        }

        public float ChaseDistance
        {
            get
            {
                return chaseDistance;
            }
            set
            {
                chaseDistance = value;
            }
        }

        public float Hysteresis
        {
            get
            {
                return hysteresis;
            }
        }

        public float MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
            set
            {
                maxSpeed = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
            set
            {
                isAlive = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public int Mana
        {
            get
            {
                return mana;
            }
            set
            {
                mana = value;
            }
        }

        public int Defense
        {
            get
            {
                return defense;
            }
            set
            {
                defense = value;
            }
        }

        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }

        public Monster(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, ReadManager rm) : base(texture, position, isWall, hitbox)
        {
            // TextureData for pixel collision
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            // Vectors
            centre = new Vector2(texture.Width / 2, texture.Height / 2);
            savedCombatPos = new Vector2(700, 75);
            // Booleans
            isAlive = true;
            wanderMovement = true;
            combatMovement = false;
            // int and float
            acceleration = 2;
            maxSpeed = 2;
            chaseDistance = 250;
            hysteresis = 15;
            // Extra
            this.rm = rm;
        }

        public void Update(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (IsAlive)
            {
                MovementUpdate(gameTime);
            }
        }

        protected void MovementUpdate(GameTime gameTime)
        {
            foreach(Hero h in rm.heroes)
            {
                float distanceFromHero = Vector2.Distance(position, h.position);
                float chaseThreshold = chaseDistance;
                float currentSpeed;

                if (state == AiState.Wander) // Created to avoid constantly removing in and out of range. 
                {
                    chaseThreshold -= hysteresis / 2;
                }

                else if (state == AiState.Chasing)
                {
                    chaseThreshold += hysteresis / 2;
                }

                if (distanceFromHero > chaseThreshold)
                {
                    state = AiState.Wander;
                }
                else if (distanceFromHero < chaseThreshold)
                {
                    state = AiState.Chasing;
                }

                if (state == AiState.Chasing) // if chasing go towards hero position.
                {
                    currentSpeed = maxSpeed;
                    Vector2 direction = Vector2.Normalize(h.position - position);
                    position.X += direction.X * currentSpeed;
                }
                else if (state == AiState.Wander) // if wandering go towards original position then walk back and forth.
                {
                    if (wanderMovement)
                    {
                        currentSpeed = 1;
                        position.X += currentSpeed;
                    }
                    else
                    {
                        currentSpeed = -1;
                        position.X += currentSpeed;
                    }
                    foreach(Monster m in rm.monsters)
                    {
                        if (position.X >= currentPosition.X + texture.Width)
                        {
                            wanderMovement = false;
                        }
                        else if (position.X <= currentPosition.X - texture.Width)
                        {
                            wanderMovement = true;
                        }
                    }
                }
            
            }
        }

        protected void AttackUpdate(GameTime gameTime, ReadManager rm)
        {
           foreach(Hero h in rm.heroes)
            {
                
                if (position.X >= savedCombatPos.X)
                {
                    if (combatMovement)
                    {
                        h.yourTurn = true;
                        acceleration = 2;
                    }
                    combatMovement = false;
                }
                else if (position.X <= h.savedCombatPos.X + texture.Width)
                {
                    combatMovement = true;
                    h.health -= (Damage - (h.defense / 2));
                }
            }

            acceleration += 0.03f;
            float currentSpeed;
            if (!combatMovement)
            {
                currentSpeed = -2 * acceleration;
                position.X += currentSpeed;
            }
            else
            {
                currentSpeed = 2 * acceleration;
                position.X += currentSpeed;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.Draw(texture, position - centre, Color.White);
            }
            else
            {

            }
        }

    }
}
