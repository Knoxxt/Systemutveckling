using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Camera // Camera that follows the hero both x and y axis. Created by Oscar.
    {
        public Matrix transform;
        private Viewport view;
        public Vector2 centre;
        private float zoom = 2.5f;
        public bool gameCamera = true;


        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Hero hero, bool gameCamera)
        {
            this.gameCamera = gameCamera;
            if (gameCamera)
            {
                centre = new Vector2(hero.position.X + (hero.fakeHitbox.Width / 2) - 900, hero.position.Y + (hero.fakeHitbox.Height / 2) - 400);
                transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            }
            else
            {
                centre = centre = new Vector2(0 + (hero.hitbox.Width / 2) + 100, 92 - 200);
                transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 0)) * Matrix.CreateTranslation(new Vector3(view.Width / 2, view.Height / 2, 0));
            }
        }
    }
}
