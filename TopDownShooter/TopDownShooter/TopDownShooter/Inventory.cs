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
    class Inventory
    {
        Texture2D texture;
        Texture2D iconTexture;
        Vector2 position;
        bool isActive;
        public bool isEmpty = true;
        string icon;

        public Inventory() { }

        public Inventory(Texture2D Texture, Vector2 Position)
        {
            texture = Texture;
            position = Position;
            isActive = false;
        }

        public void Initialize(Vector2 Position)
        {
            position = Position;
            isActive = false;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("InventoryBorder");
            
        }

        public void Update(GameTime gametime,ContentManager Content)
        {
            if (isActive)
                iconTexture = Content.Load<Texture2D>(icon);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isActive)
                spriteBatch.Draw(iconTexture, position, Color.White);

            spriteBatch.Draw(texture, position, Color.White);
        }

        public void AddItem(string iconName)
        {
            icon = iconName;
            isActive = true;
        }
    }
}
