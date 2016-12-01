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
    class Background
    {
        Texture2D backgroundTexture;
        Texture2D treesTexture;

        public Vector2 backgroundPosition;

        

        public void Initialize(Player player)
        {
            //backgroundPosition = new Vector2(0-player.playerPosition.X, 0-player.playerPosition.Y);
            backgroundPosition = new Vector2(0, 0);
            
        }

        public void LoadContent(ContentManager Content)
        {
            backgroundTexture = Content.Load<Texture2D>("Background");
            treesTexture = Content.Load<Texture2D>("Trees");
        }

        public void Update()
        {
            //backgroundPosition = new Vector2(0 - player.playerPosition.X * 2, 0 - player.playerPosition.Y*2);
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
        }

        public void DrawTrees(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(treesTexture, backgroundPosition, Color.White);
        }
    }
}
