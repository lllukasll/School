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
    class Gun
    {
        public Texture2D texture;
        public Vector2 position;
        public int gunIndex;
        public bool isVisible = true;

        public Rectangle boundingBox;
        private string text;
        private SpriteFont font;
        private bool isStringVisible = false;
        private string gunName;
        
        private KeyboardState kbState;
        private float stringPositionX;

        public Gun() { }

        public Gun(Texture2D Texture, Vector2 Position, string Text, int GunIndex,ContentManager Content)
        {
            texture = Texture;
            position = Position;
            text = "Aby podniesc " + Text + " nacisnij E";
            gunIndex = GunIndex;
            font = Content.Load<SpriteFont>("Gun");
            gunName = Text;
        }
        public void Initialize(Texture2D Texture, Vector2 Position, string Text)
        {
            texture = Texture;
            position = Position;
            text = "Aby podniesc " + Text + " nacisnij E";
            gunName = Text;
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Gun");
        }

        public void Update(GameTime gameTime,Player player,Inventory inventory)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            kbState = Keyboard.GetState();

            if(boundingBox.Intersects(player.boundingBox))
            {
                isStringVisible = true;
                if(kbState.IsKeyDown(Keys.E))
                {
                    isVisible = false;
                    isStringVisible = false;
                    inventory.AddItem(gunName + "Icon");
                }
            }
            else
            {
                isStringVisible = false;
            }
            stringPositionX =position.X + texture.Width/2 - font.MeasureString(text).X/2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isStringVisible && isVisible)
                spriteBatch.DrawString(font, text, new Vector2(stringPositionX, position.Y - font.MeasureString(text).Y), Color.White);
            if(isVisible)
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
