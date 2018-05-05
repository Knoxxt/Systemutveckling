using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    abstract class DynamicGameObject // Created by Anton.
    {
        public Color[] textureData;
        public Vector2 position;
        public Texture2D texture;
        public bool isWall; // Is it walkable or not.
        public Rectangle hitbox;
        public Texture2D fakeHitbox;
        protected Rectangle sourceRectangle;
        protected double frameTimer, frameInterval, deltaTime, delayTimer;
        protected float rotation, currentFrame;
        protected int frame, damage;
        protected SpriteEffects spriteEffects;



        protected DynamicGameObject(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox, Texture2D fakeHitbox)
        {
            this.texture = texture;
            this.position = position;
            this.isWall = isWall;
            this.hitbox = hitbox;
            currentFrame = 0;
        }

        protected void UpdateAnimation(GameTime gameTime, int numberOfFrames, int widthOfFrame, bool lap)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            delayTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (lap)
            {
                if(frameTimer <= 0)
                {
                    currentFrame++;
                    frame--;
                    frameTimer = frameInterval;
                    sourceRectangle.X = (frame % numberOfFrames) * widthOfFrame;
                   
                }
                if (currentFrame == numberOfFrames)
                {
                    damage = 50;
                    currentFrame = 0;
                }
                else
                {
                    damage = 0;
                }

            }
            else
            {
                if (frameTimer <= 0)
                {
                    frame++;
                    frameTimer = frameInterval;
                    sourceRectangle.X = (frame % numberOfFrames) * widthOfFrame;
                }
            }
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, rotation, new Vector2(16, 16), 1, spriteEffects, 1);
        }
    }
}
