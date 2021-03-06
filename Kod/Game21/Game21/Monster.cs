﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    abstract class Monster : GameObject
    {
        protected Color[] textureData;
        protected Vector2 velocity;
        public Monster(Texture2D texture, Vector2 position, bool isWall, Rectangle hitbox) : base(texture, position, isWall, hitbox)
        {
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

    }
}
