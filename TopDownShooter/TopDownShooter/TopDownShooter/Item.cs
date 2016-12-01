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
    class Item
    {
        //Tekstura przedmiotu
        public Texture2D texture;
        //Pozycja przedmiotu
        public Vector2 startPosition;
        public Vector2 position;
        //Index przedmiotu
        public int itemIndex;
        public bool isVisible = true;

        public Rectangle boundingBox;
        private string text;
        private SpriteFont font;
        private bool isStringVisible = false;
        private string gunName;
        
        private KeyboardState kbState;
        private KeyboardState prevkbState;
        private float stringPositionX;

        //Typ przedmiotu 0-Puste | 1-Bron | 2-Apteczka itp.
        private int type;
        //Ile go wysyłam
        private int quantity;
        //Maksymalna ilosc tego przedmiotu
        private int maxQuantity;

        public Item() { }

        public Item(Texture2D Texture, Vector2 Position, string Text, int ItemIndex,int Type,int Quantity, int MaxQuantity,ContentManager Content)
        {
            texture = Texture;
            position = Position;
            text = "Aby podniesc " + Text + " nacisnij E";
            itemIndex = ItemIndex;
            font = Content.Load<SpriteFont>("Gun");
            gunName = Text;
            type = Type;
            quantity = Quantity;
            maxQuantity = MaxQuantity;
        }
        public void Initialize(Texture2D Texture, Vector2 Position, string Text, int Type)
        {
            texture = Texture;
            startPosition = Position;
            text = "Aby podniesc " + Text + " nacisnij E";
            gunName = Text;
            type = Type;
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Gun");
        }

        public void Update(GameTime gameTime,Player player,InventoryManager inventoryManager,Background background)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            prevkbState = kbState;
            kbState = Keyboard.GetState();

            if(boundingBox.Intersects(player.boundingBox))
            {
                isStringVisible = true;
                if(kbState.IsKeyDown(Keys.E) && prevkbState.IsKeyUp(Keys.E))
                {
                    isVisible = false;
                    isStringVisible = false;
                    foreach(Inventory field in inventoryManager.inventory)
                    {
                        if(field.isEmpty==false && field.icon == gunName +"Icon")
                        {
                            //Tutaj jest miejsce na dodawanie amunicji gdy podniesie się broń , ktora
                            //juz jest w ekwipunku
                            field.AddQuantity(quantity);
                            return;
                        }
                        else if(field.isEmpty==true)
                        {
                            field.AddItem(gunName + "Icon",gunName,type,quantity,maxQuantity,itemIndex);
                            field.isEmpty = false;
                            return;
                        }
                    }
                }
            }
            else
            {
                isStringVisible = false;
            }
            stringPositionX =position.X + texture.Width/2 - font.MeasureString(text).X/2;

            //position = new Vector2(background.backgroundPosition.X + startPosition.X,background.backgroundPosition.Y+startPosition.Y);
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
