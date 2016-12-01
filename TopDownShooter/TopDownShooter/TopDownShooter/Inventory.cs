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
        //Textura obramowania gdy wybrane
        Texture2D textureChoosen;
        //textura ikony
        Texture2D iconTexture;
        //Pozycja pola
        Vector2 position;
        //Pozycja końcowa (potrzebne przy poruszaniu się)
        Vector2 endPosition;
        //Pozycja napisu o tyypie broni i ilosci dostepnej amunicji
        Vector2 stringPosition;
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
        public int quantity;
        //Maxymalna ilosc
        int maxQuantity;
        //Nazwa przedmioty
        string itemName;
        //Index przedmoitu np. 1-Pistol; 2-Sniper, 3-Machinegun ,..., 15-Apteczka itp...
        int itemIndex;
        //Sprawdza czy wlasnie zmieniony zeby moc wyslac numer indexu do gracza 
        public bool justChanged = false;
        

        public Inventory() { }

        //Konstruktor
        public Inventory(Texture2D Texture,Texture2D TextureChoosen, Vector2 Position,SpriteFont itemInformationFont)
        {
            texture = Texture;
            position = Position;
            isActive = false;
            font = itemInformationFont;
            isChoosen = false;
            type = 0;
            quantity = 0; // Do dokonczenia
            maxQuantity = 0; // Do dokonczenia
            textureChoosen = TextureChoosen;
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
            textureChoosen = Content.Load<Texture2D>("InventoryBorderChoosen");
        }

        public void Update(GameTime gametime,ContentManager Content, Player player,Vector2 screenSize)
        {
            

            //Jeżeli ilość przedmiotu jest wieksza od max to iloasc = max
            if (quantity > maxQuantity)
                quantity = maxQuantity;

            if (isActive)
                iconTexture = Content.Load<Texture2D>(icon);

            if(isChoosen)
            {
                if(justChanged == true)
                {
                    player.ChooseItem(itemIndex);
                    justChanged = false;
                }
                    

                if (type==0)
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
                    itemInformationString = icon + " | Ilosc : ";
                }
            }

            endPosition = new Vector2(player.playerPosition.X - 400 + 10 + texture.Width, player.playerPosition.Y - 200 + position.Y );
            stringPosition = new Vector2(player.playerPosition.X - screenSize.X/2 + 40, player.playerPosition.Y - screenSize.Y / 2 + 80);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            if(isActive)
                spriteBatch.Draw(iconTexture, endPosition, Color.White);
            if (isChoosen)
            {
                spriteBatch.DrawString(font, itemInformationString, stringPosition, Color.White);
                spriteBatch.Draw(textureChoosen, endPosition, Color.White);
            }else
            {
                spriteBatch.Draw(texture, endPosition, Color.White);
            }
            
        }

        public void AddItem(string iconName,string Name,int Type,int Quantity,int MaxQuantity,int ItemIndex)
        {
            icon = iconName;
            isActive = true;
            itemName = Name;
            type = Type;
            quantity = Quantity;
            maxQuantity = MaxQuantity;
            itemIndex = ItemIndex;
        }

        public void AddQuantity(int Quantity)
        {
            quantity += Quantity;
        }

        public void SubtractQuantity()
        {
            quantity--;
        }
    }
}
