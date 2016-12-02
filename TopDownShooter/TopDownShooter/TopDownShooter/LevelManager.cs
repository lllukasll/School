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
    class LevelManager
    {
        public Player player = new Player();
        EnemyManager enemy = new EnemyManager();
        Item Machinegun = new Item();
        ItemManager item = new ItemManager();
        //Inventory inventory = new Inventory();
        //InventoryManager 
        InventoryManager inventoryManager = new InventoryManager();
        //RoomGenerator roomGenerator = new RoomGenerator();
        KeyboardState kbState,prevKbState;
        bool start = true;
        //Background background = new Background();
        //Room room = new Room();
        LabiryntGenerator labiryntGenerator;


        //Zmienne tymczasowe
        public int X=4, Y=2;
        public bool isColidingWithDoor0 = false;
        public bool isColidingWithDoor1 = false;
        public bool isColidingWithDoor2 = false;
        public bool isColidingWithDoor3 = false;

        public void Initialize(ContentManager Content)
        {
            player.Initialize(Content);
            //room.Initialize();
            //background.Initialize(player);
            //inventory.Initialize(new Vector2(10, 64));
            //labiryntGenerator = new LabiryntGenerator(3, 3, Content);
            //labiryntGenerator.genarateMaze();
            
        }

        public void LoadContent(ContentManager Content)
        {
            player.LoadContent(Content);
            //room.LoadContent(Content);
            //background.LoadContent(Content);
            //inventory.LoadContent(Content);
        }

        public void Update(GameTime gameTime,ContentManager Content,Vector2 screenSize)
        {
            if(start)
            {
                item.SpawnItem("Pistol", "Pistol", new Vector2(100, 200), 0, Content, 30, 30, 1);
                start = false;
                
            }
            
            prevKbState = kbState;
            kbState = Keyboard.GetState();
            
            if (kbState.IsKeyDown(Keys.D1)) //&& kbState.IsKeyUp(Keys.Q))
            {
                inventoryManager.inventory.ElementAt(0).justChanged = true;
                inventoryManager.inventory.ElementAt(0).isChoosen = true;
                inventoryManager.inventory.ElementAt(1).isChoosen = false;
                inventoryManager.inventory.ElementAt(2).isChoosen = false;
                //player.ChangeShootingDelay(15);
                //Trzeba to bedzie zrobic z foreach!!!!!!!!!!!!!
            }
            else if (kbState.IsKeyDown(Keys.D2)) //&& kbState.IsKeyUp(Keys.Q))
            {
                inventoryManager.inventory.ElementAt(1).justChanged = true;
                inventoryManager.inventory.ElementAt(1).isChoosen = true;
                inventoryManager.inventory.ElementAt(2).isChoosen = false;
                inventoryManager.inventory.ElementAt(0).isChoosen = false;
                //player.ChangeShootingDelay(5);
                //Trzeba to bedzie zrobic z foreach!!!!!!!!!!!
            }
            else if (kbState.IsKeyDown(Keys.D3)) //&& kbState.IsKeyUp(Keys.Q))
            {
                inventoryManager.inventory.ElementAt(2).justChanged = true;
                
                inventoryManager.inventory.ElementAt(0).isChoosen = false;
                inventoryManager.inventory.ElementAt(1).isChoosen = false;
                inventoryManager.inventory.ElementAt(2).isChoosen = true;
                //player.ChangeShootingDelay(5);
                //Trzeba to bedzie zrobic z foreach!!!!!!!!!!!
            }
            //Losowe położenie X i Y 
            Random rnd = new Random();
            int randX = rnd.Next(100, 400);
            int randY = rnd.Next(100, 400);
            int randX2 = rnd.Next(100, 400);
            int randY2 = rnd.Next(100, 400);

            /*
            //Jeżeli pokoi jest mniej niż 2 w liście to dodaj
            if (roomGenerator.rooms.Count < 2)
            {
                roomGenerator.SpawnRoom(0, Content,new Vector2(0,0));
                roomGenerator.SpawnRoom(1, Content,new Vector2(0,0));
            }*/

            //Jeżeli jest mniej niż 3 zombie dodaj je do listy w losowych miejscach
            if (enemy.zombies.Count < 3)
                enemy.SpawnZombie(randX, randY, Content);

            //Jeżeli jest mniej niż 3 fatso dodaj je do listy w losowych miejscach
            if (enemy.fatsos.Count < 2)
                enemy.SpawnFatso(randX, randY, Content);

            //Sprawdzenie czy broni jest mniej niz 2 jesli tak to dodaj je do listy
            if (item.items.Count < 2)
            {
                item.SpawnItem("Machinegun", "Machinegun",new Vector2(randX, randY), 2,Content,70,60,1);
                item.SpawnItem("Sniper", "Sniper", new Vector2(randX2, randY2), 1, Content,20,20,1);
            }

            player.Update(gameTime,inventoryManager,screenSize);

            //Wykonaj polecenie Update dla każdego aktywnego zombie
            foreach (Enemy zombie in enemy.zombies)
            {
                zombie.Update(gameTime, player.playerPosition, player);
            }

            //Wykonaj polecenie Update dla każdego aktywnego fatso
            foreach (Enemy fatso in enemy.fatsos)
            {
                fatso.Update(gameTime, player.playerPosition, player);
            }

            //Wykonaj polecenie Update dla każdego aktywnego przedmiotu
            foreach (Item item in item.items)
            {
                item.Update(gameTime, player, inventoryManager);
            }

            for (int i = 0; i < player.bullets.Count; i++)
            {
                //Sprawdzanie czy zombie koliduje z kulami
                for (int j = 0; j < enemy.zombies.Count; j++)
                {
                    if (enemy.zombies[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        enemy.zombies[j].DecreaseHP(player.strength);
                    }
                }
                for (int j = 0; j < enemy.fatsos.Count; j++)
                {
                    //Sprawdzanie czy fatso koliduje z kulami
                    if (enemy.fatsos[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        enemy.fatsos[j].DecreaseHP(player.strength);
                    }
                }
            }


            //Sprawdzenie czy ktorys z zombie jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < enemy.zombies.Count; i++)
            {
                if (!enemy.zombies[i].isVisible)
                {
                    enemy.zombies.RemoveAt(i);
                    i--;
                }
            }

            //Sprawdzenie czy ktorys z fatso jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < enemy.fatsos.Count; i++)
            {
                if (!enemy.fatsos[i].isVisible)
                {
                    enemy.fatsos.RemoveAt(i);
                    i--;
                }
            }


            //InventoryManager , jeżeli pól jes mniej niż 10 dodaj je do listy 
            if (inventoryManager.inventory.Count < 10)
                inventoryManager.DrawInventory(Content, new Vector2(10, 64 + inventoryManager.inventory.Count * 32));
            //Wykonaj polecenie Update dla każdego pola w inventory
            foreach (Inventory field in inventoryManager.inventory)
            {
                field.Update(gameTime, Content,player,screenSize);
            }

            //Sprawdzenie czy ktoras z broni jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < item.items.Count; i++)
            {
                if (!item.items[i].isVisible)
                {
                    item.items.RemoveAt(i);
                    i--;
                }
            }

            
            
            
            //Po nacisnieciu przyciskow zmieniaja sie pokoje
            if(isColidingWithDoor2 && kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E))
            {
                isColidingWithDoor2 = false;
                X--;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 0);
                
            }
            if (isColidingWithDoor0 && kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E))
            {
                X++;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 2);
            }
            if (isColidingWithDoor1 && kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E))
            {
                Y--;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 3);
            }
            if (isColidingWithDoor3 && kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E))
            {
                Y++;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 1);
            }

            isColidingWithDoor0 = false;
            isColidingWithDoor1 = false;
            isColidingWithDoor2 = false;
            isColidingWithDoor3 = false;
            /*
            if (kbState.IsKeyDown(Keys.I) && prevKbState.IsKeyUp(Keys.I))
            {
                X--;
                labiryntGenerator.rooms[1, 1].ChangePlayerPosition(player);
            }
            
            if (kbState.IsKeyDown(Keys.L) && prevKbState.IsKeyUp(Keys.L))
            {
               Y++;
                labiryntGenerator.rooms[1, 1].ChangePlayerPosition(player);
            }

            if (kbState.IsKeyDown(Keys.J) && prevKbState.IsKeyUp(Keys.J))
            {
                Y--;
                labiryntGenerator.rooms[1, 1].ChangePlayerPosition(player);
            }
            if (kbState.IsKeyDown(Keys.K) && prevKbState.IsKeyUp(Keys.K))
            {
                X++;
                labiryntGenerator.rooms[1, 1].ChangePlayerPosition(player);
            }

             */
            labiryntGenerator = new LabiryntGenerator(Content);

            if (labiryntGenerator.rooms[X, Y].isVisible == false)
            {
                foreach(Room room in labiryntGenerator.rooms)
                {
                    room.isVisible = false;
                }
                labiryntGenerator.rooms[X, Y].isVisible = true;
            }
                

            foreach (Room room in labiryntGenerator.rooms)
            {
                room.Update(gameTime, player,Content,this);
            }
            
            //room.Update(gameTime);

        }

        public void UpdateRoomNumber(int newX,int newY)
        {
            X = newX;
            Y = newY;
        }

        public void UpdateX()
        {
            X--;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //room.Draw(spriteBatch);
            //background.DrawBackground(spriteBatch);
            
            
            foreach (Room room in labiryntGenerator.rooms)
            {
                room.Draw(spriteBatch);
            }
            
            player.Draw(spriteBatch);

            
            //Wyswietla wszystkie zombie
            foreach (Enemy zombie in enemy.zombies)
            {
                zombie.Draw(spriteBatch);
            }

            //Wyswietla wszystkie fatso
            foreach (Enemy fatso in enemy.fatsos)
            {
                fatso.Draw(spriteBatch);
            }

            //background.DrawTrees(spriteBatch);

            foreach (Item item in item.items)
            {
                item.Draw(spriteBatch);
            }
            foreach (Inventory field in inventoryManager.inventory)
            {
                field.Draw(spriteBatch);
            }

            //inventory.Draw(spriteBatch);
        }
    }
}
