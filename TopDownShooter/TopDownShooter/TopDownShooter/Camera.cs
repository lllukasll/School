using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TopDownShooter
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime,Player player,float ScreenWidth,float ScreenHeight)
        {
            centre = new Vector2(player.playerPosition.X + (player.boundingBox.Width / 2) - ScreenWidth / 2,
                player.playerPosition.Y + (player.boundingBox.Height / 2) - ScreenHeight / 2);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }

        public void UpdateCamera(GameTime gameTime, Vector2 Position,SpriteFont Font, string text, float ScreenWidth, float ScreenHeight)
        {
            centre = new Vector2(Position.X + (Font.MeasureString(text).X/2) - ScreenWidth / 2,
                Position.Y + (Font.MeasureString(text).Y / 2) - ScreenHeight / 2);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }

    }
}
