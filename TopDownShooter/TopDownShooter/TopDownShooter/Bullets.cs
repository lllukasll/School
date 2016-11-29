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
    class Bullets
    {
        public Texture2D texture;

        public Vector2 position, velocity, origin;
        public bool isVisible;


        public Rectangle boundingBox;


        public Bullets(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
        }


        public void Draw(SpriteBatch spriteBatch, float rotation)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 1.0f);
        }


    }
}
