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
    class Door
    {
        //Numer drzwi 0-4
        int doorNumber;
        //Pozycja drzwi zależnie od numeru
        Vector2 doorPosition;
        //Pole kolizji drzwi
        Rectangle doorBoundingBox;
        //Czcionka do napisu informujacego o mozliwosci otworzenia drzwi
        SpriteFont font;
        //Czy napis powinien być wyświetlany
        bool isStringVisible;

        public Door() { }

        public Door(int DoorNumber,ContentManager Content)
        {
            font = Content.Load<SpriteFont>("DoorInformation");
            isStringVisible = false;

            if (DoorNumber == 0)
            {
                doorNumber = 0;
                doorPosition = new Vector2(220, 450);
                doorBoundingBox = new Rectangle((int)doorPosition.X, (int)doorPosition.Y, 180, 60);
            }
            else if (DoorNumber == 1)
            {
                doorNumber = 1;
                doorPosition = new Vector2(0, 160);
                doorBoundingBox = new Rectangle((int)doorPosition.X, (int)doorPosition.Y, 100, 180);
            }
            else if (DoorNumber == 2)
            {
                doorNumber = 2;
                doorPosition = new Vector2(220, 0);
                doorBoundingBox = new Rectangle((int)doorPosition.X, (int)doorPosition.Y, 180, 100);
            }
            else if (DoorNumber == 3)
            {
                doorNumber = 3;
                doorPosition = new Vector2(550, 160);
                doorBoundingBox = new Rectangle((int)doorPosition.X, (int)doorPosition.Y, 60, 180);
            }

        }


        public void Update(GameTime gameTime,Player player)
        {
            if(doorBoundingBox.Intersects(player.boundingBox))
            {
                isStringVisible = true;
            }else
            {
                isStringVisible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isStringVisible)
                spriteBatch.DrawString(font, "Aby przejsc do nastepnego pomieszczenia nacisnij E", doorPosition,Color.White);
        }

    }
}
