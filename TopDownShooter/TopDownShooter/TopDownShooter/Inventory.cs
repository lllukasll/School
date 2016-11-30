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
        //Textura obramowania
        Texture2D texture;
        //textura ikony
        Texture2D iconTexture;
        //Pozycja pola
        Vector2 position;
        //Czy jest aktywne
        bool isActive;
        //Czy jest puste
        public bool isEmpty = true;
        //Nazwa ikony
        public string icon;
        //Czy jest aktualnie wybrane
        public bool isChoosen;
        //Typ elementu w ekwipunku : 0-Puste,1-Bron,2-Apteczka itp.
        int type;
        //SpriteFont - do wyswietlania informacji o amunicji, ilosci apteczek itp.
        SpriteFont font;
        //Napis jak wyzej
        string itemInformationString;
        //Aktualna ilosc 
        int quantity;
        //Maxymalna ilosc
        int maxQuantity;
        //Nazwa przedmioty
        string itemName;

        public Inventory() { }

        //Konstruktor
        public Inventory(Texture2D Texture, Vector2 Position,SpriteFont itemInformationFont)
        {
            texture = Texture;
            position = Position;
            isActive = false;
            font = itemInformationFont;
            isChoosen = false;
            type = 1;
            quantity = 0; // Do dokonczenia
            maxQuantity = 0; // Do dokonczenia
        }

        public void Initialize(Vector2 Position)
        {
            position = Position;
            isActive = false;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("InventoryBorder");
            font = Content.Load<SpriteFont>("ItemInformation");
        }

        public void Update(GameTime gametime,ContentManager Content)
        {
            if (isActive)
                iconTexture = Content.Load<Texture2D>(icon);

            if(isChoosen)
            {
                if(type==0)
                {
                    //Tutaj co się dzieje gdy typ przedmiotu = 0
                    itemInformationString = "";
                }
                else if(type==1)
                {
                    //Tutaj co się dzieje gdy typ przedmiotu = 1
                    itemInformationString = itemName + "| Ammo : " + quantity + "/" + maxQuantity;
                }else if (type == 2)
                {
                    //Tutaj co się dzieje gdy typ przedmiotu = 2
                    //itemInformationString = icon + " | Ilosc : ";
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isActive)
                spriteBatch.Draw(iconTexture, position, Color.White);
            if (isChoosen)
                spriteBatch.DrawString(font, itemInformationString, new Vector2(10, 40), Color.White);
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void AddItem(string iconName,string Name)
        {
            icon = iconName;
            isActive = true;
            itemName = Name;
        }
    }
}
