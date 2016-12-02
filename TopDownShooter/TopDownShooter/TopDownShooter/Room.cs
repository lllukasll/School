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
    class Room
    {
        //Textura pokoju
        Texture2D roomTexture;
        //
        Rectangle roomBoundingBox;
        //Pozycja pokoju
        Vector2 roomPosition;
        //Miejsce w ktorym gracz rozpoczyna
        Vector2 playerStartingPoint;
        //Czy pokój jest aktualnie widoczny
        public bool isVisible;
        //Czy gracz koliduje ze ścianami
        //bool isPlayerColiding;
        //Vector2 playerActualPosition;
        List<Door> doors = new List<Door>();

        //Zmienne potrzebne fo generowania labiryntu
        public bool visited = false;
        int roomType = 0;
        public bool door0 = false;
        public bool door1 = false;
        public bool door2 = false;
        public bool door3 = false;


        /*
        Texture2D torchTexture;
        SpriteAnimation torch;
        Vector2 torchPosition;
        */


        public Room() { }

        public Room(int roomNumber,ContentManager Content,Vector2 RoomPosition)
        {
            roomTexture = Content.Load<Texture2D>("Room" + Convert.ToString(roomNumber));
            playerStartingPoint = new Vector2(320, 410);
            CheckDoorNumber(Content);
            roomPosition = RoomPosition;
            //isPlayerColiding = false;
            roomBoundingBox = new Rectangle((int)roomPosition.X + 64, (int)roomPosition.Y + 64, roomTexture.Width - 128, roomTexture.Height - 128);
            isVisible = false;
        }

        public void ChangePlayerPosition(Player player)
        {
            player.ChangePosition(playerStartingPoint);
        }

        public void CheckDoorNumber(ContentManager Content)
        {
            if(door0)
            {
                Door door = new Door(0, Content);
                doors.Add(door);
            }
            if(door1)
            {
                Door door = new Door(1, Content);
                doors.Add(door);
            }
            if (door2)
            {
                Door door = new Door(2, Content);
                doors.Add(door);
            }
            if (door3)
            {
                Door door = new Door(3, Content);
                doors.Add(door);
            }
        }

        void CheckDoors(ContentManager Content)
        {
            if(door0)
            {
                roomTexture = Content.Load<Texture2D>("Rooms/Room3");
                if(door1)
                {
                    roomTexture = Content.Load<Texture2D>("Rooms/Room6");
                    if(door2)
                    {
                        roomTexture = Content.Load < Texture2D>("Rooms/Room11");
                        if (door3)
                        {
                            roomTexture = Content.Load<Texture2D>("Rooms/Room15");
                        }
                            
                    }
                }else if(door2)
                {
                    roomTexture = Content.Load<Texture2D>("Rooms/Room9");
                    if(door3)
                    {
                        roomTexture = Content.Load<Texture2D>("Rooms/Room13");
                    }
                }else if(door3)
                {
                    roomTexture = Content.Load<Texture2D>("Rooms/Room5");
                }
            }else if(door1)
            {
                roomTexture = Content.Load<Texture2D>("Rooms/Room4");
                if(door2)
                {
                    roomTexture = Content.Load<Texture2D>("Rooms/Room7");
                    if(door3)
                    {
                        roomTexture = Content.Load<Texture2D>("Rooms/Room12");
                    }
                }
                else if(door3)
                {
                    roomTexture = Content.Load<Texture2D>("Rooms/Room10");
                }
            }else if(door2)
            {
                roomTexture = Content.Load<Texture2D>("Rooms/Room1");
                if(door3)
                {
                    roomTexture = Content.Load<Texture2D>("Rooms/Room8");
                }
            }else if(door3)
            {
                roomTexture = Content.Load<Texture2D>("Rooms/Room2");
            }
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

        public void Update(GameTime gameTime,Player player,ContentManager Content)
        {
            CheckDoors(Content);
            foreach (Door door in doors)
            {
                door.Update(gameTime, player);
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
            }
                
            //torch.Draw(spriteBatch);
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
