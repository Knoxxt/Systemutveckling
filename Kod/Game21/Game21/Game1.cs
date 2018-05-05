using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Game21.Utilities;
using Game21.Managers;
using Game21.Objects;

namespace Game21
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Variables vars;
        private ReadManager rm;
        private CombatManager com;
        private Camera camera;
        private InventoryManager im;
        private RuneOfWind lel;
        private CollisionManager cm;
        private Interface interfaces;
        private float width, height;
        public bool combat, game, menu;
        public GameState state = GameState.Menu;

        public float Width // Game Window Width
        {
            get { return width; }
            set { width = value; }
        }
        public float Height // Game Window Height
        {
            get { return height; }
            set { height = value; }
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            vars = new Variables();
            height = GraphicsDevice.DisplayMode.Height;
            width = GraphicsDevice.DisplayMode.Width;
            UserInterface.Initialize(Content, BuiltinThemes.editor);
            UserInterface.Active.SetCursor(CursorType.Default);
            graphics.PreferredBackBufferWidth = (int)width;
            graphics.PreferredBackBufferHeight = (int)height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            game = true;
            menu = true;
            combat = true;
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            vars.floor = Content.Load<Texture2D>("floor");
            vars.hero = Content.Load<Texture2D>("hero");
            vars.menuBG = Content.Load<Texture2D>("bg");
            vars.gameBG = Content.Load<Texture2D>("gameBG");
            vars.greenMonster = Content.Load<Texture2D>("greenMonster");
            rm = new ReadManager(vars, this);
            com = new CombatManager(rm, this);
            interfaces = new Interface(rm, this, com);
            cm = new CollisionManager(rm, com, this);
            interfaces.Menu();
            game = true;
            menu = true;
            combat = true;
            im = new InventoryManager(rm);
            lel = new RuneOfWind(vars.floor, new Vector2(0, 0), "Rune of Wind");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            UserInterface.Active.Update(gameTime);
            KeymouseReader.Update();
            switch (state)
            {
                case GameState.Game:
                    combat = true;
                    menu = true;
                    foreach (Hero h in rm.heroes)
                    {
                        h.Update(gameTime);
                        camera.Update(gameTime, h, true);
                    }
                    if (KeymouseReader.KeyPressed(Keys.H))
                    {
                        im.AddItem(lel, false);
                        rm.warrior.agility += 1;
                    }
                    foreach (Monster m in rm.monsters)
                    {
                        m.Update(gameTime);
                    }
                    cm.Update(gameTime);
                    if (game)
                    {
                        interfaces.Game();
                        game = false;
                    }
                    break;
                case GameState.Combat:
                    game = true;
                    menu = true;
                    com.Update(gameTime);
                    cm.Update(gameTime);
                    foreach (Hero h in rm.heroes)
                    {
                        camera.Update(gameTime, h, false);
                    }
                    if (combat)
                    {
                        interfaces.Combat();
                        combat = false;
                    }
                    interfaces.Update(gameTime);
                    break;
                case GameState.Menu:
                    game = true;
                    combat = true;
                    if (menu)
                    {
                        menu = false;
                    }
                    break;
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            float X = 0;
            switch (state)
            {
                case GameState.Game:
                    foreach(Hero h in rm.heroes)
                    {
                        X = h.position.X;
                        spriteBatch.Begin(transformMatrix: camera.transform);
                        spriteBatch.Draw(vars.gameBG, new Rectangle((int)X - 1000, (int)h.position.Y - 500, vars.gameBG.Width, vars.gameBG.Height), Color.White);
                        h.Draw(spriteBatch);
                    }
                    foreach (Monster m in rm.monsters)
                    {
                        m.Draw(spriteBatch);
                    }
                    foreach(Tile t in rm.tileList)
                    {
                        t.Draw(spriteBatch);
                    }
                    spriteBatch.End();
                    break;
                case GameState.Combat:
                    foreach(Hero h in rm.heroes)
                    {
                        X = h.savedCombatPos.X;
                        spriteBatch.Begin(transformMatrix: camera.transform);
                        spriteBatch.Draw(vars.gameBG, new Rectangle((int)X - 1000, (int)h.position.Y - 500, vars.gameBG.Width, vars.gameBG.Height), Color.White);
                        h.Draw(spriteBatch);
                    }
                    foreach (Monster m in rm.monsters)
                    {
                        m.Draw(spriteBatch);
                    }
                    spriteBatch.End();
                    foreach(Hero h in rm.heroes)
                    {
                        h.DrawParticles(spriteBatch);
                    }
                    break;
                case GameState.Menu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(vars.menuBG, new Rectangle(0, 0, (int)Width, (int)Height), Color.White);
                    spriteBatch.End();
                    break;
            }
            UserInterface.Active.Draw(spriteBatch);
            base.Draw(gameTime);
        }


    #region PixelCollision
        public bool IntersectPixel(Rectangle rect1, Color[] data1,
   Rectangle rect2, Color[] data2)
        {
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            for (int y = top; y < bottom; y++)
                for (int x = left; x < right; x++)
                {
                    Color color1 = data1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];
                    Color color2 = data2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];

                    if (color1.A != 0 && color2.A != 0)
                    {
                        return true;
                    }
                }


            return false;
        } 
        #endregion // Perpixel collision. Created by Anton. (OBS should be moved).
    }
}
