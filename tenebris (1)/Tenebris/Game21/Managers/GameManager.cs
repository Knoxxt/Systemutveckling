using GeonBit.UI;
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
    class GameManager
    {
        private Game1 g;
        private Variables vars;
        private ReadManager rm;
        private CombatManager com;
        private Camera camera;
        private CollisionManager cm;
        private Interface interfaces;
        public bool combat, game, menu;
        public GameState state = GameState.Menu;

        public GameManager(Game1 g, Camera camera, ReadManager rm, CombatManager com, CollisionManager cm, Variables vars, Interface interfaces)
        {
            this.g = g;
            this.rm = rm;
            this.com = com;
            this.cm = cm;
            this.vars = vars;
            this.interfaces = interfaces;
            this.camera = camera;
        }


        public void UpdatePlatform(GameTime gameTime)
        {
            combat = true;
            menu = true;
            rm.hero.Update(gameTime);
            rm.green.Update(gameTime);
            cm.Update(gameTime);
            camera.Update(gameTime, rm.hero, true);
            if (game)
            {
                interfaces.Game();
                game = false;
            }
        }

        public void UpdateCombat(GameTime gameTime)
        {
            game = true;
            menu = true;
            com.Update(gameTime);
            cm.Update(gameTime);
            camera.Update(gameTime, rm.hero, false);
            if (combat)
            {
                interfaces.Combat();
                combat = false;
            }
            interfaces.Update();
        }

        public void UpdateMenu()
        {
            game = true;
            combat = true;
            if (menu)
            {
                interfaces.Menu();
                menu = false;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {

           
        }
    }
}
