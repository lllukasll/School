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
    class LevelManager
    {
        public Player player = new Player();
        public EnemyManager enemy = new EnemyManager();
        ItemManager item = new ItemManager();
        InventoryManager inventoryManager = new InventoryManager();
        LabiryntGenerator labiryntGenerator;


        //Zmienne tymczasowe
        KeyboardState kbState, prevKbState;
        MouseState msState, prevMsState;
        bool start = true;
        public int X=4, Y=2;
        public bool isColidingWithDoor0 = false;
        public bool isColidingWithDoor1 = false;
        public bool isColidingWithDoor2 = false;
        public bool isColidingWithDoor3 = false;

        //Czy menu jest widoczne
        public bool isMenuVisible;
        public bool isGameOverMenuVisible;
        public bool isInstructionsVisible;
        public bool isCongratulationsVisible;
        SpriteFont newGameFont;
        SpriteFont escapeFont;
        SpriteFont instructionsFont;
        SpriteFont gameOverFont;
        SpriteFont instructionsTextFont;
        SpriteFont backToMenuFont;
        SpriteFont congratulationsFont;
        string newGameString;
        string escapeString;
        string instructionsString;
        string gameOverString;
        string instructionsTextString;
        string backToMenuString;
        string congratulationsString;
        Vector2 newGameStringPosition;
        Vector2 escapeStringPosition;
        Vector2 instructionStringPosition;
        Vector2 gameOverStringPosition;
        Vector2 instructionsTextStringPosition;
        Vector2 backToMenuStringPosition;
        Vector2 congratulationsStringPosition;
        Color newGameStringColor;
        Color instructionStringColor;
        Color escapeStringColor;
        Color gameOverStringColor;
        Color instructionsTextStringColor;
        Color backToMenuStringColor;
        Color congratulationsStringColor;
        int chosenOption;
        int gameOverChoosenOption;
        int instructionsChoosenOption;
        int congratulationsChoosenOption;

        //Crosshair
        Texture2D crosshairTexture;
        Vector2 crosshairPossition;


        //Zmienna sprawdzajaca czy wszyscy przeciwnicy w pokoju zabici
        public bool enemyKilled;

        public void Initialize(ContentManager Content,Vector2 ScreenSize,GraphicsDeviceManager Graphic)
        {
            player.Initialize(Content);
            isMenuVisible = true;
            newGameString = "Nowa Gra";
            escapeString = "Wyjdz";
            instructionsString = "Jak Grac";
            instructionsTextString = " CEL : Znajdz wyjscie z labiryntu \n\n STEROWANIE : \n WSAD : Poruszanie sie \n E : Interakcja \n 1,2,3 : Zmiana aktualnie trzymanego itemu";
            newGameStringPosition = new Vector2(ScreenSize.X / 2 - newGameFont.MeasureString(newGameString).X / 2, ScreenSize.Y /4- 50 - newGameFont.MeasureString(newGameString).Y / 2);
            instructionStringPosition = new Vector2(ScreenSize.X / 2 - instructionsFont.MeasureString(instructionsString).X / 2, ScreenSize.Y / 4- instructionsFont.MeasureString(instructionsString).Y / 2);
            escapeStringPosition = new Vector2(ScreenSize.X / 2 - escapeFont.MeasureString(escapeString).X / 2, ScreenSize.Y / 4 + ScreenSize.Y / 4 +50 - escapeFont.MeasureString(escapeString).Y / 2);
            newGameStringColor = Color.White;
            instructionStringColor = Color.White;
            escapeStringColor = Color.White;
            isInstructionsVisible = false;
            chosenOption = 0;
            gameOverChoosenOption = 0;
            labiryntGenerator = new LabiryntGenerator();
            enemyKilled = false;
            isGameOverMenuVisible = false;
            gameOverStringColor = Color.White;
            gameOverString = "Nie zyjesz :(";
            instructionsTextStringColor = Color.White;
            backToMenuStringColor = Color.White;
            backToMenuString = "Nacisnij E zeby wrocic";
            instructionsChoosenOption = 0;
            congratulationsStringColor = Color.White;
            congratulationsString = "Gratulacje udalo ci sie wyjsc z labiryntu";
            isCongratulationsVisible = false;
            congratulationsChoosenOption = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            player.LoadContent(Content);
            newGameFont = Content.Load<SpriteFont>("Fonts/NewGameFont");
            escapeFont = Content.Load<SpriteFont>("Fonts/EscapeFont");
            instructionsFont = Content.Load<SpriteFont>("Fonts/InstructionsFont");
            gameOverFont = Content.Load<SpriteFont>("Fonts/GameOverFont");
            crosshairTexture = Content.Load<Texture2D>("Crosshair");
            instructionsTextFont = Content.Load<SpriteFont>("Fonts/InstructionsTextFont");
            backToMenuFont = Content.Load<SpriteFont>("Fonts/BackToMenu");
            congratulationsFont = Content.Load<SpriteFont>("Fonts/CongratulationsFont");
        }

        public void Update(GameTime gameTime,ContentManager Content,Vector2 screenSize, Game1 game1,Vector2 ScreenSize)
        {

            

            prevKbState = kbState;
            kbState = Keyboard.GetState();
            
            prevMsState = msState;
            msState = Mouse.GetState();

            //Crosshair
            crosshairPossition = new Vector2(msState.X - crosshairTexture.Width / 2,
                msState.Y - crosshairTexture.Height / 2);

            if (start)
            {
                item.SpawnItem("Pistol", "Pistol", new Vector2(100, 200), 0, Content, 30, 30, 1);
                start = false;
                
            }
            
            
            if (isMenuVisible)
            {
                

                if ((kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S)) || (kbState.IsKeyDown(Keys.Down) && prevKbState.IsKeyUp(Keys.Down)))
                    chosenOption++;
                else if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W) || (kbState.IsKeyDown(Keys.Up) && prevKbState.IsKeyUp(Keys.Up)))
                    chosenOption--;

                if (chosenOption > 2)
                    chosenOption = 0;

                if (chosenOption < 0)
                    chosenOption = 2;

                if (chosenOption == 0)
                {
                    if (kbState.IsKeyDown(Keys.Enter) && prevKbState.IsKeyUp(Keys.Enter))
                        isMenuVisible = false;

                    newGameStringColor = Color.Red;
                    instructionStringColor = Color.White;
                    escapeStringColor = Color.White;
                }  
                else if (chosenOption == 1)
                {
                    if (kbState.IsKeyDown(Keys.Enter) && prevKbState.IsKeyUp(Keys.Enter))
                    {
                        //isMenuVisible = false;
                        isInstructionsVisible = true;
                    }

                    newGameStringColor = Color.White;
                    instructionStringColor = Color.Red;
                    escapeStringColor = Color.White;

                    
                }  
                else if (chosenOption == 2)
                {
                    if (kbState.IsKeyDown(Keys.Enter) && prevKbState.IsKeyUp(Keys.Enter))
                        game1.Exit();

                    newGameStringColor = Color.White;
                    instructionStringColor = Color.White;
                    escapeStringColor = Color.Red;
                }
                    



            }

            if(isGameOverMenuVisible)
            {
                gameOverStringPosition =new Vector2( player.playerPosition.X-gameOverFont.MeasureString(gameOverString).X/2,player.playerPosition.Y - gameOverFont.MeasureString(gameOverString).Y/2);
                escapeStringPosition = new Vector2(player.playerPosition.X - escapeFont.MeasureString(escapeString).X / 2,
                    player.playerPosition.Y - escapeFont.MeasureString(escapeString).Y / 2 + 50);

                if (gameOverChoosenOption == 0)
                {
                    escapeStringColor = Color.Red;
                    if (kbState.IsKeyDown(Keys.Enter) && prevKbState.IsKeyUp(Keys.Enter))
                    {
                        game1.Exit();
                    }
                    
                }
            }

            if (isInstructionsVisible)
            {
                instructionsTextStringPosition = new Vector2(newGameStringPosition.X - 150, newGameStringPosition.Y - 100);
                backToMenuStringPosition = new Vector2(instructionsTextStringPosition.X, instructionsTextStringPosition.Y + 300);
                if (instructionsChoosenOption == 0)
                {
                    backToMenuStringColor = Color.Red;
                    if (kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E))
                    {
                        isMenuVisible = true;
                        isInstructionsVisible = false;
                    }
                }

            }

            if(isCongratulationsVisible)
            {
                congratulationsStringPosition = new Vector2(player.playerPosition.X - congratulationsFont.MeasureString(congratulationsString).X / 2, player.playerPosition.Y - congratulationsFont.MeasureString(congratulationsString).Y / 2);
                escapeStringPosition = new Vector2(player.playerPosition.X - escapeFont.MeasureString(escapeString).X / 2,
                   player.playerPosition.Y - escapeFont.MeasureString(escapeString).Y / 2 + 50);

                if (congratulationsChoosenOption == 0)
                {
                    escapeStringColor = Color.Red;
                    if (kbState.IsKeyDown(Keys.Enter) && prevKbState.IsKeyUp(Keys.Enter))
                    {
                        game1.Exit();
                    }

                }
            }

            if (!isMenuVisible && !isGameOverMenuVisible && !isInstructionsVisible && !isCongratulationsVisible)
            {

                labiryntGenerator.Maze1(Content, this);//= new LabiryntGenerator(Content);

                ChangeItem(kbState);
                //Losowe położenie X i Y 
                Random rnd = new Random();
                int randX = rnd.Next(100, 400);
                int randY = rnd.Next(100, 400);
                int randX2 = rnd.Next(100, 400);
                int randY2 = rnd.Next(100, 400);

                //Sprawdzenie czy broni jest mniej niz 2 jesli tak to dodaj je do listy
                if (item.items.Count < 2)
                {
                    item.SpawnItem("Machinegun", "Machinegun", new Vector2(randX, randY), 2, Content, 70, 60, 1);
                    item.SpawnItem("Sniper", "Sniper", new Vector2(randX2, randY2), 1, Content, 20, 20, 1);
                }

                player.Update(gameTime, inventoryManager, screenSize,this);
                enemy.Update(gameTime, player);

                //Wykonaj polecenie Update dla każdego aktywnego przedmiotu
                foreach (Item item in item.items)
                {
                    item.Update(gameTime, player, inventoryManager);
                }

                //InventoryManager , jeżeli pól jes mniej niż 10 dodaj je do listy 
                if (inventoryManager.inventory.Count < 10)
                    inventoryManager.DrawInventory(Content, new Vector2(10, 64 + inventoryManager.inventory.Count * 32));
                
                //Wykonaj polecenie Update dla każdego pola w inventory
                foreach (Inventory field in inventoryManager.inventory)
                {
                    field.Update(gameTime, Content, player, screenSize);
                }

                //Sprawdzenie czy ktoras z broni jest niewidoczna , jeśli tak to usun go z listy
                for (int i = 0; i < item.items.Count; i++)
                {
                    if (!item.items[i].isVisible)
                    {
                        item.items.RemoveAt(i);
                        i--;
                    }
                }

                //Sprawdzam z którymi drzwiami koliduje i zmieniam numer pokoju
                if(labiryntGenerator.rooms[X,Y].enemyKilled == true)
                    ChangeRoom(kbState, prevKbState);
                
                //Sprawdzam czy pokój jest widoczny jeżeli nie to zmieniam na tak
                if (labiryntGenerator.rooms[X, Y].isVisible == false)
                {
                    foreach (Room room in labiryntGenerator.rooms)
                    {
                        room.isVisible = false;
                    }
                    labiryntGenerator.rooms[X, Y].isVisible = true;
                }

                //Wykonuje polecenie Update dla każdego pokoju
                foreach (Room room in labiryntGenerator.rooms)
                {
                    room.Update(gameTime, player, Content, this);
                }

                //Wysyłam numer aktualnego pokoju do labiryntGeneratora
                labiryntGenerator.activeRoom(X, Y);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isInstructionsVisible)
            {
                spriteBatch.DrawString(instructionsTextFont, instructionsTextString, instructionsTextStringPosition, instructionsTextStringColor);
                spriteBatch.DrawString(backToMenuFont, backToMenuString, backToMenuStringPosition, backToMenuStringColor);

            }
            else if (isMenuVisible)
            {
                
                spriteBatch.DrawString(newGameFont, newGameString, newGameStringPosition, newGameStringColor);
                spriteBatch.DrawString(instructionsFont, instructionsString, instructionStringPosition, instructionStringColor);
                spriteBatch.DrawString(escapeFont, escapeString, escapeStringPosition, escapeStringColor);
            } else if(isGameOverMenuVisible)
            {
                spriteBatch.DrawString(gameOverFont, gameOverString, gameOverStringPosition, gameOverStringColor);
                spriteBatch.DrawString(escapeFont, escapeString, escapeStringPosition, escapeStringColor);
            }else if(isCongratulationsVisible)
            {
                spriteBatch.DrawString(congratulationsFont, congratulationsString, congratulationsStringPosition, congratulationsStringColor);
                spriteBatch.DrawString(escapeFont, escapeString, escapeStringPosition, escapeStringColor);

            }
            else
            {

                foreach (Room room in labiryntGenerator.rooms)
                {
                    room.Draw(spriteBatch);
                }

                player.Draw(spriteBatch);
                enemy.Draw(spriteBatch);
                //background.DrawTrees(spriteBatch);

                foreach (Item item in item.items)
                {
                    item.Draw(spriteBatch);
                }
                foreach (Inventory field in inventoryManager.inventory)
                {
                    field.Draw(spriteBatch);
                }

                spriteBatch.Draw(crosshairTexture, crosshairPossition, Color.White);
            }
        }

        public void ChangeRoom(KeyboardState KbState, KeyboardState PrevKbState)
        {
            //Po nacisnieciu przyciskow zmieniaja sie pokoje
            if (isColidingWithDoor2 && KbState.IsKeyDown(Keys.E) && PrevKbState.IsKeyUp(Keys.E))
            {
                isColidingWithDoor2 = false;
                X--;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 0);
                labiryntGenerator.roomActivated = true;

            }
            if (isColidingWithDoor0 && KbState.IsKeyDown(Keys.E) && PrevKbState.IsKeyUp(Keys.E))
            {
                X++;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 2);
                labiryntGenerator.roomActivated = true;
            }
            if (isColidingWithDoor1 && KbState.IsKeyDown(Keys.E) && PrevKbState.IsKeyUp(Keys.E))
            {
                Y--;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 3);
                labiryntGenerator.roomActivated = true;
            }
            if (isColidingWithDoor3 && KbState.IsKeyDown(Keys.E) && PrevKbState.IsKeyUp(Keys.E))
            {
                Y++;
                labiryntGenerator.rooms[X, Y].ChangePlayerPosition(player, 1);
                labiryntGenerator.roomActivated = true;
            }

            isColidingWithDoor0 = false;
            isColidingWithDoor1 = false;
            isColidingWithDoor2 = false;
            isColidingWithDoor3 = false;
        }

        public void ChangeItem(KeyboardState KbState)
        {

            if (KbState.IsKeyDown(Keys.D1)) //&& kbState.IsKeyUp(Keys.Q))
            {
                inventoryManager.inventory.ElementAt(0).justChanged = true;
                inventoryManager.inventory.ElementAt(0).isChoosen = true;
                inventoryManager.inventory.ElementAt(1).isChoosen = false;
                inventoryManager.inventory.ElementAt(2).isChoosen = false;
                //player.ChangeShootingDelay(15);
                //Trzeba to bedzie zrobic z foreach!!!!!!!!!!!!!
            }
            else if (KbState.IsKeyDown(Keys.D2)) //&& kbState.IsKeyUp(Keys.Q))
            {
                inventoryManager.inventory.ElementAt(1).justChanged = true;
                inventoryManager.inventory.ElementAt(1).isChoosen = true;
                inventoryManager.inventory.ElementAt(2).isChoosen = false;
                inventoryManager.inventory.ElementAt(0).isChoosen = false;
                //player.ChangeShootingDelay(5);
                //Trzeba to bedzie zrobic z foreach!!!!!!!!!!!
            }
            else if (KbState.IsKeyDown(Keys.D3)) //&& kbState.IsKeyUp(Keys.Q))
            {
                inventoryManager.inventory.ElementAt(2).justChanged = true;

                inventoryManager.inventory.ElementAt(0).isChoosen = false;
                inventoryManager.inventory.ElementAt(1).isChoosen = false;
                inventoryManager.inventory.ElementAt(2).isChoosen = true;
                //player.ChangeShootingDelay(5);
                //Trzeba to bedzie zrobic z foreach!!!!!!!!!!!
            }
        }
    }
}
