using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace TopDownShooter
{
    class Player
    {

        //Kontroler animacji nog postaci
        SpriteAnimation playerLegsAnimation;
        //Textura torsu postaci
        Texture2D torsoTexture;
        //Zmienne przetrzymujące aktualne przyciski (myszy i klawiatury)
        KeyboardState kbState;
        MouseState msState;
        MouseState prevMsState;
        //Kontroler animacji tworzący kopie aby każda animacja mogła być niezależna
        AnimationClass ani = new AnimationClass();
        //Zmienna przetrzymująca rotację postaci
        float rotation;
        //Zmienna przetrzymująca aktualną pozycje gracza
        public Vector2 playerPosition;
        //Poprzednia pozycja gracza
        public Vector2 prevPlayerPosition;
        //Ramka do kolizji
        public Rectangle boundingBox;
        //Wysokość i szerokość gracza
        int playerWidth = 64, playerHeight = 64;

        //Czas delayu miedzy strzalami
        int delayTime;
        //Tymczasowa zmienna przechowujaca delayTime
        int tmpdelayTime;
        //Sprawdzenie czy gracz atrzelil
        bool isShooting;

        ContentManager Content;

        //Bullets
        public List<Bullets> bullets = new List<Bullets>();
        public int strength;

        //Item trzymany przez gracza 
        bool isHolding;
        Texture2D itemTexture;
        Vector2 itemPosition;
        //Rectangle itemBoundingBox;

        //Score
        SpriteFont scoreFont;
        string scoreText;
        public int score;
        Vector2 scorePosition;

        //HP
        Texture2D hpBarTexture;
        Texture2D hpTextutre;
        Vector2 hpPosition;
        int Hp;
        Vector2 hpScale;

        //Sprawdzenie czy nie koliduje ze scianami
        public bool canGoLeft = true;
        public bool canGoRight = true;
        public bool canGoUp = true;
        public bool canGoDown = true;

        public void Initialize(ContentManager content)
        {
            //playerTorsoAnimation.FramesPerSecond = 8;
            playerLegsAnimation.FramesPerSecond = 8;
            Content = content;

            rotation = 0;

            //HP
            Hp = 150;
            hpPosition = new Vector2(10, 10);
            hpScale.X = 15;
            hpScale.Y = 1;
            delayTime = 15;
            tmpdelayTime = delayTime;
            strength = 0;
            score = 0;

        }

        public void LoadContent(ContentManager Content)
        {
            //Score
            scoreFont = Content.Load<SpriteFont>("ScoreFont");

            //HP
            hpBarTexture = Content.Load<Texture2D>("hpBar");
            hpTextutre = Content.Load<Texture2D>("hp");


            //Animacja Gracza
            Texture2D legsTexture = Content.Load<Texture2D>("PlayerLegsAnimation");
            torsoTexture = Content.Load<Texture2D>("PlayerTorso2hAnimation");

            playerLegsAnimation = new SpriteAnimation(legsTexture, 4, 2);
            playerLegsAnimation.Position = new Vector2(100, 100);


            playerLegsAnimation.AddAnimation("Right", 1, 4, ani.Copy());

            ani.Rotation = MathHelper.Pi;
            playerLegsAnimation.AddAnimation("Left", 1, 4, ani.Copy());

            ani.Rotation = -MathHelper.PiOver2;
            playerLegsAnimation.AddAnimation("Up", 1, 4, ani.Copy());

            ani.Rotation = MathHelper.PiOver2;
            playerLegsAnimation.AddAnimation("Down", 1, 4, ani.Copy());

            ani.Rotation = MathHelper.PiOver4;
            playerLegsAnimation.AddAnimation("DownRight", 1, 4, ani.Copy());

            ani.Rotation = -MathHelper.PiOver2 - MathHelper.PiOver4;
            playerLegsAnimation.AddAnimation("LeftUp", 1, 4, ani.Copy());

            ani.Rotation = -MathHelper.PiOver4;
            playerLegsAnimation.AddAnimation("UpRight", 1, 4, ani.Copy());

            ani.Rotation = MathHelper.PiOver2 + MathHelper.PiOver4;
            playerLegsAnimation.AddAnimation("LeftDown", 1, 4, ani.Copy());

            playerLegsAnimation.AddAnimation("Idle", 2, 1, ani.Copy());




        }

        public void Update(GameTime gameTime, InventoryManager inventoryManager, Vector2 screenSize,LevelManager level)
        {
            if(hpScale.X<=0)
            {
                level.isGameOverMenuVisible = true;
            }
            prevPlayerPosition = playerPosition;

            //Ustawia położenie itemu aktualnie trzymanego przez gracza 
            itemPosition = new Vector2(playerLegsAnimation.Position.X, playerLegsAnimation.Position.Y);

            //BoundingBox aktualizacja
            boundingBox = new Rectangle((int)playerPosition.X,(int) playerPosition.Y, playerWidth, playerHeight);

            //Rotacja
            MouseState Ms = Mouse.GetState();
            Vector2 mouse_pos = new Vector2(Ms.X, Ms.Y);//Pozycja myszy(x,y)
            Vector2 direction = mouse_pos - playerLegsAnimation.Position;//Róznica miedzy X,Y

            rotation = (float)Math.Atan2((double)direction.Y, (double)direction.X);

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }


            //Poruszanie gracza
            kbState = Keyboard.GetState();


            if (((kbState.IsKeyDown(Keys.Up) && kbState.IsKeyDown(Keys.Right)) || (kbState.IsKeyDown(Keys.W) && kbState.IsKeyDown(Keys.D))) && (canGoUp &&canGoRight))
            {
                playerLegsAnimation.Position.Y -= 2;
                playerLegsAnimation.Position.X += 2;
                if (playerLegsAnimation.Animation != "UpRight")
                    playerLegsAnimation.Animation = "UpRight";
            }
            else if (((kbState.IsKeyDown(Keys.Down) && kbState.IsKeyDown(Keys.Right)) || (kbState.IsKeyDown(Keys.S) && kbState.IsKeyDown(Keys.D))) && (canGoDown && canGoRight))
            {
                playerLegsAnimation.Position.Y += 2;
                playerLegsAnimation.Position.X += 2;
                if (playerLegsAnimation.Animation != "DownRight")
                    playerLegsAnimation.Animation = "DownRight";
            }
            else if (((kbState.IsKeyDown(Keys.Left) && kbState.IsKeyDown(Keys.Up)) || (kbState.IsKeyDown(Keys.A) && kbState.IsKeyDown(Keys.W))) && (canGoLeft&& canGoUp))
            {
                playerLegsAnimation.Position.Y -= 2;
                playerLegsAnimation.Position.X -= 2;
                if (playerLegsAnimation.Animation != "LeftUp")
                    playerLegsAnimation.Animation = "LeftUp";
            }
            else if (((kbState.IsKeyDown(Keys.Down) && kbState.IsKeyDown(Keys.Left)) || (kbState.IsKeyDown(Keys.S) && kbState.IsKeyDown(Keys.A))) && (canGoDown && canGoLeft))
            {
                playerLegsAnimation.Position.Y += 2;
                playerLegsAnimation.Position.X -= 2;
                if (playerLegsAnimation.Animation != "LeftDown")
                    playerLegsAnimation.Animation = "LeftDown";
            }
            else if ((kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.A)) && canGoLeft)
            {
                playerLegsAnimation.Position.X -= 2;
                if (playerLegsAnimation.Animation != "Left")
                    playerLegsAnimation.Animation = "Left";
            }
            else if ((kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.D)) && canGoRight)
            {
                playerLegsAnimation.Position.X += 2;
                if (playerLegsAnimation.Animation != "Right")
                    playerLegsAnimation.Animation = "Right";
            }
            else if ((kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.W)) && canGoUp)
            {
                playerLegsAnimation.Position.Y -= 2;
                if (playerLegsAnimation.Animation != "Up")
                    playerLegsAnimation.Animation = "Up";
            }
            else if ((kbState.IsKeyDown(Keys.Down) || kbState.IsKeyDown(Keys.S)) && canGoDown)
            {
                playerLegsAnimation.Position.Y += 2;
                if (playerLegsAnimation.Animation != "Down")
                    playerLegsAnimation.Animation = "Down";
            }
            else
            {
                if (playerLegsAnimation.Animation != "Idle")
                    playerLegsAnimation.Animation = "Idle";
            }


            //Strzelanie
            msState = Mouse.GetState();

            if (msState.LeftButton == ButtonState.Pressed && prevMsState.LeftButton == ButtonState.Released && isShooting == false )
            {
                foreach(Inventory field in inventoryManager.inventory)
                {
                    if(field.isChoosen)
                    {
                        if(field.quantity > 0)
                        {
                            field.SubtractQuantity();
                            Shoot();
                        }
                        
                    }
                }
                
                isShooting = true;
            }
                
            if(isShooting)
            {
                if(tmpdelayTime==delayTime)
                {
                    tmpdelayTime--;
                    
                }
                else
                {
                    tmpdelayTime--;
                    if(tmpdelayTime==0)
                    {
                        isShooting = false;
                        tmpdelayTime = delayTime;
                    }
                }
            }
            prevMsState = msState;

            UpdateBullets();

            //Wyświetlanie animacji gracza
            playerPosition = playerLegsAnimation.Position;
            playerLegsAnimation.Update(gameTime);

            //HP
            hpScale.X = Hp / 10;
            hpPosition = new Vector2(playerPosition.X - screenSize.X / 2 + 40, playerPosition.Y - screenSize.Y / 2 + 10 + hpBarTexture.Height);

            //Score
            scoreText = "SCORE : " + Convert.ToString(score);
            scorePosition = new Vector2(playerPosition.X + screenSize.X / 2 - scoreFont.MeasureString(scoreText).X, playerPosition.Y - screenSize.Y / 2 + 10 + scoreFont.MeasureString(scoreText).Y);
        }

        //Bullets metod
        public void UpdateBullets()
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.boundingBox = new Rectangle((int)bullet.position.X, (int)bullet.position.Y,
                    bullet.texture.Width, bullet.texture.Height);

                bullet.position += bullet.velocity;
                if (Vector2.Distance(bullet.position, playerLegsAnimation.Position) > 500)
                    bullet.isVisible = false;
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].isVisible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }

            }
        }

        public void Shoot()
        {
            Bullets newBullet = new Bullets(Content.Load<Texture2D>("Bullet"));
            newBullet.velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 5f;
            newBullet.position = playerLegsAnimation.Position + newBullet.velocity * 5;
            newBullet.isVisible = true;

            if (bullets.Count < 20)
                bullets.Add(newBullet);
        }

        public void AddScore(int howMuchToAdd)
        {
            score += howMuchToAdd;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Hp
            spriteBatch.Draw(hpTextutre, hpPosition, null, Color.White, 0, Vector2.Zero,
                hpScale, SpriteEffects.None, 0);
            spriteBatch.Draw(hpBarTexture, hpPosition, Color.White);

            //Score
            spriteBatch.DrawString(scoreFont, scoreText, scorePosition, Color.White);

            //Rysuje nogi
            playerLegsAnimation.Draw(spriteBatch);

            //Bullets
            foreach (Bullets bullet in bullets)
                bullet.Draw(spriteBatch, rotation);

            //Rysuje tors
            spriteBatch.Draw(torsoTexture, playerLegsAnimation.Position, null, Color.White, rotation,
                new Vector2(torsoTexture.Width / 2, torsoTexture.Height / 2), 1.0f, SpriteEffects.None, 1.0f);

            //Jeżli gracz coś trzyma to rysuje item
            if (isHolding)
                spriteBatch.Draw(itemTexture, itemPosition, null, Color.White, rotation,
                    new Vector2(torsoTexture.Width / 2, torsoTexture.Height / 2), 1.0f, SpriteEffects.None, 1.0f);
        }

        public void DecreaseHP(int ammountToDecrease)
        {
            Hp -= ammountToDecrease;
        }

        public void IncreaseHp(int ammountToDecrease)
        {
            Hp += ammountToDecrease;
        }

        public void ChangeShootingDelay(int delay)
        {
            delayTime = delay;
            tmpdelayTime = delayTime;
        }

        public void ChangePosition(Vector2 Position)
        {
            playerLegsAnimation.Position = Position;
        }

        public void ChooseItem(int index)
        {
            if(index==0)//Wybrany pistolet
            {
                ChangeShootingDelay(15);
                strength = 30;
                isHolding = true;
                itemTexture = Content.Load<Texture2D>("Pistol");
                torsoTexture = Content.Load<Texture2D>("PlayerTorso1hAnimation");
            }
            if (index==1)//Wybrany sniper
            {
                ChangeShootingDelay(40);
                strength = 100;
                isHolding = true;
                itemTexture = Content.Load<Texture2D>("Sniper");
                torsoTexture = Content.Load<Texture2D>("PlayerTorso2hAnimation");
            }
            if (index==2)//Wybrany machinegun
            {
                ChangeShootingDelay(2);
                strength = 20;
                isHolding = true;
                itemTexture = Content.Load<Texture2D>("Machinegun");
                torsoTexture = Content.Load<Texture2D>("PlayerTorso2hAnimation");
            }
        }
    }
}
