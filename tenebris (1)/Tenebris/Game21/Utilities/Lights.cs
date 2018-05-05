using Microsoft.Xna.Framework;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21.Utilities
{
    class Lights
    {
        PenumbraComponent penumbra;
        Enums enums;
        public Lights(PenumbraComponent penumbra, Enums enums)
        {
            this.penumbra = penumbra;
            this.enums = enums;
            penumbra.Lights.Add(light);
            penumbra.Hulls.Add(hull);
        }

        Light light = new PointLight
        {
            Scale = new Vector2(700f),
            ShadowType = ShadowType.Solid
            
        };

        Hull hull = new Hull(new Vector2(1.0f), new Vector2(-1.0f, 1.0f), new Vector2(-1.0f), new Vector2(1.0f, -1.0f))
        {
            Scale = new Vector2(50f)
        };

        public void Update(GameTime gameTime, ReadManager rm)
        {
            if(enums.gState == GameState.Game)
            {
                light.Position = new Vector2(870f, 345f);
                light.Scale = new Vector2(700f);
            }
            else if(enums.gState == GameState.Combat)
            {
                light.Position = new Vector2(950f, 345f);
                light.Scale = new Vector2(2300f);
            }
            else if(enums.gState == GameState.Menu)
            {
                light.Position = new Vector2(900f, 345f);
                light.Scale = new Vector2(700f);
            }
            penumbra.AmbientColor = Color.Black;
            
            foreach (Tile t in rm.tileList) { hull.Position = t.position; }

            hull.Rotation = MathHelper.WrapAngle(-(float)gameTime.TotalGameTime.TotalSeconds);
        }
    }
}
