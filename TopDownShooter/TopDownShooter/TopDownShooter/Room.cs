﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TopDownShooter
{
    class Room
    {
        //Textura pokoju
        Texture2D roomTexture;
        //
        Rectangle roomBoundingBox;
        //Pozycja pokoju
        Vector2 roomPosition = new Vector2(0, 0);
        //Miejsce w ktorym gracz rozpoczyna
        Vector2[] playerStartingPoint = new Vector2[4];
        //Czy pokój jest aktualnie widoczny
        public bool isVisible;
        public List<Door> doors = new List<Door>();

        //Zmienne potrzebne fo generowania labiryntu
        public bool visited = false;
        //int roomType = 0;
        public bool door0 = false;
        public bool door1 = false;
        public bool door2 = false;
        public bool door3 = false;

        public bool enemyKilled = false;
        /*
        Texture2D torchTexture;
        SpriteAnimation torch;
        Vector2 torchPosition;
        */
        SpriteFont error;
        //string errorText;
        public int roomNumber;

        //Wyjscie z labiryntu
        Texture2D ladderTexture;
        Vector2 ladderPosition;
        Rectangle ladderBoundingBox;
        bool isLadderVisible = false;
        SpriteFont wyjdzZLabiryntuFont;
        string wyjdzZLabiryntuString = "Aby wyjsc z labiryntu nacisnij E";
        Vector2 wyjdzZLAbiryntuStringPosition;
        Color wyjdzZLabiryntuColor = Color.White;
        bool isStringVisible = false;
        KeyboardState kbState, prevKbState;

        public Room() { }

        public Room(int RoomNumber,ContentManager Content)
        {
            roomTexture = Content.Load<Texture2D>("Rooms/Room" + Convert.ToString(RoomNumber));
            roomNumber = RoomNumber;
            CheckHowManyDoors(Content);
            playerStartingPoint[0] = new Vector2(330, 410);
            playerStartingPoint[1] = new Vector2(110, 260);
            playerStartingPoint[2] = new Vector2(325, 100);
            playerStartingPoint[3] = new Vector2(520, 260);
            isVisible = false;
            //error = Content.Load<SpriteFont>("ErrorInformation");
        }


        public void ChangePlayerPosition(Player player,int doorNumber)
        {
            player.ChangePosition(playerStartingPoint[doorNumber]);
        }

        public void Initialize()
        {
            /*
            torchPosition = new Vector2(200, 100);
            torch.Animation = "torchAnimation";
            torch.Position = torchPosition;
            */
        }

        public void LoadContent(ContentManager Content)
        {
            /*
            roomTexture = Content.Load<Texture2D>("Room2");
            torchTexture = Content.Load<Texture2D>("Torch");
            torch = new SpriteAnimation(torchTexture, 5, 1);
            AnimationClass ani = new AnimationClass();
            torch.AddAnimation("torchAnimation", 1, 5, ani.Copy());
            */
        }

        public void Update(GameTime gameTime,Player player,ContentManager Content,LevelManager level)
        {
            if (roomNumber == 20 && isVisible)
                isLadderVisible = true;
            else
                isLadderVisible = false;

            prevKbState = kbState;

            kbState = Keyboard.GetState();

            if(isLadderVisible)
            {
                ladderTexture = Content.Load<Texture2D>("Ladder");
                ladderPosition = new Vector2(300, 200);
                ladderBoundingBox = new Rectangle((int)ladderPosition.X, (int)ladderPosition.Y, ladderTexture.Width, ladderTexture.Height);
                wyjdzZLabiryntuFont = Content.Load<SpriteFont>("Fonts/WyjdzZLabiryntuFont");
                wyjdzZLAbiryntuStringPosition = new Vector2(ladderPosition.X - 20, ladderPosition.Y - 20);

                if (ladderBoundingBox.Intersects(player.boundingBox))
                {
                    isStringVisible = true;
                    if (kbState.IsKeyDown(Keys.E))
                    {
                        level.isCongratulationsVisible = true;
                    }
                }
                else
                    isStringVisible = false;
            }

            roomBoundingBox = new Rectangle((int)roomPosition.X + 64, (int)roomPosition.Y + 64, roomTexture.Width - 128, roomTexture.Height - 128);
            if(isVisible)
                foreach (Door door in doors)
                {
                    door.Update(gameTime, player,level);
                }

            WallColission(player);
            /*
            torch.Update(gameTime);
            */
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isVisible)
            {
                
                spriteBatch.Draw(roomTexture, new Vector2(0, 0), Color.White);
                foreach (Door door in doors)
                {
                    door.Draw(spriteBatch);
                }
                if (isLadderVisible)
                    spriteBatch.Draw(ladderTexture, ladderPosition, Color.White);
                if (isStringVisible)
                    spriteBatch.DrawString(wyjdzZLabiryntuFont, wyjdzZLabiryntuString, wyjdzZLAbiryntuStringPosition, wyjdzZLabiryntuColor);

            }

        }

        public void CheckHowManyDoors(ContentManager Content)
        {
            if (roomNumber == 1)
            {
                Door door = new Door(2, Content);
                doors.Add(door);
            }

            if (roomNumber == 2)
            {
                Door door = new Door(3, Content);
                doors.Add(door);
            }

            if (roomNumber == 3)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
            }

            if (roomNumber == 4)
            {
                Door door = new Door(1, Content);
                doors.Add(door);
            }

            if (roomNumber == 5)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
                door = new Door(3, Content);
                doors.Add(door);
            }

            if (roomNumber == 6)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
                door = new Door(1, Content);
                doors.Add(door);
            }

            if (roomNumber == 7)
            {
                Door door = new Door(1, Content);
                doors.Add(door);
                door = new Door(2, Content);
                doors.Add(door);
            }

            if (roomNumber == 8)
            {
                Door door = new Door(2, Content);
                doors.Add(door);
                door = new Door(3, Content);
                doors.Add(door);
            }

            if (roomNumber == 9)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
                door = new Door(2, Content);
                doors.Add(door);
            }

            if (roomNumber == 10)
            {
                Door door = new Door(1, Content);
                doors.Add(door);
                door = new Door(3, Content);
                doors.Add(door);
            }

            if (roomNumber == 11)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
                door = new Door(1, Content);
                doors.Add(door);
                door = new Door(2, Content);
                doors.Add(door);
            }

            if (roomNumber == 12)
            {
                Door door = new Door(1, Content);
                doors.Add(door);
                door = new Door(2, Content);
                doors.Add(door);
                door = new Door(3, Content);
                doors.Add(door);
            }

            if (roomNumber == 13)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
                door = new Door(2, Content);
                doors.Add(door);
                door = new Door(3, Content);
                doors.Add(door);
            }

            if (roomNumber == 14)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
                door = new Door(1, Content);
                doors.Add(door);
                door = new Door(3, Content);
                doors.Add(door);
            }

            if (roomNumber == 15)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
                door = new Door(1, Content);
                doors.Add(door);
                door = new Door(2, Content);
                doors.Add(door);
                door = new Door(3, Content);
                doors.Add(door);
            }
        }

        public void WallColission(Player player)
        {
            if (player.boundingBox.X - 30 <= roomBoundingBox.X && player.boundingBox.Y - 20 <= roomBoundingBox.Y)
            {//Czy porusza sie w lewo i do góry
                player.canGoRight = true;
                player.canGoUp = false;
                player.canGoDown = true;
                player.canGoLeft = false;
            }
            else if (player.boundingBox.Y - 20 <= roomBoundingBox.Y && player.boundingBox.X >= roomBoundingBox.X + roomTexture.Width - 128)
            {//Porusza sie w prawo i do góry
                player.canGoRight = false;
                player.canGoUp = false;
                player.canGoDown = true;
                player.canGoLeft = true;
            }
            else if (player.boundingBox.Y >= roomBoundingBox.Y + roomTexture.Height - 128 && player.boundingBox.X - 30 <= roomBoundingBox.X)
            {//Porusza sie w lewo i w dół
                player.canGoRight = true;
                player.canGoUp = true;
                player.canGoDown = false;
                player.canGoLeft = false;
            }
            else if (player.boundingBox.Y >= roomBoundingBox.Y + roomTexture.Height - 128 && player.boundingBox.X >= roomBoundingBox.X + roomTexture.Width - 128)
            {//Porusza sie w prawo i w dół
                player.canGoRight = false;
                player.canGoUp = true;
                player.canGoDown = false;
                player.canGoLeft = true;
            }
            else if (player.boundingBox.X - 30 <= roomBoundingBox.X)//Porusza sie w lewo
            {
                //player.ChangePosition(new Vector2(player.prevPlayerPosition.X,player.prevPlayerPosition.Y));
                player.canGoRight = true;
                player.canGoUp = true;
                player.canGoDown = true;
                player.canGoLeft = false;
            }
            else if (player.boundingBox.Y - 20 <= roomBoundingBox.Y)//Porusza sie do gory
            {
                //player.ChangePosition(new Vector2(player.prevPlayerPosition.X, player.prevPlayerPosition.Y));
                player.canGoRight = true;
                player.canGoUp = false;
                player.canGoDown = true;
                player.canGoLeft = true;
            }
            else if (player.boundingBox.X >= roomBoundingBox.X + roomTexture.Width - 128)//Porusza sie w prawo
            {
                player.canGoRight = false;
                player.canGoUp = true;
                player.canGoDown = true;
                player.canGoLeft = true;
            }
            else if (player.boundingBox.Y >= roomBoundingBox.Y + roomTexture.Height - 128)//Porusza sie w dol
            {
                player.canGoRight = true;
                player.canGoUp = true;
                player.canGoDown = false;
                player.canGoLeft = true;
            }
            else
            {
                player.canGoLeft = true;
                player.canGoUp = true;
                player.canGoRight = true;
                player.canGoDown = true;
            }
        }
    }
}
