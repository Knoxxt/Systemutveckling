using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Objects
{
    class RuneOfWind : Item
    {
        public RuneOfWind(Texture2D texture, Vector2 position, string name) : base(texture, position, name)
        {
            this.texture = texture;
            this.position = position;
            this.name = name;

            name = "Rune of Wind";
            agility = 10;
        }
    }
}
