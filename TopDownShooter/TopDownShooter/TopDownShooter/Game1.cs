using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace TopDownShooter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player = new Player();
        EnemyManager enemy = new EnemyManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            player.Initialize(Content);
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content); 
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //Losowe po³o¿enie X i Y 
            Random rnd = new Random();
            int randX = rnd.Next(100, 400);
            int randY = rnd.Next(100, 400);

            if (enemy.zombies.Count < 3)
                enemy.SpawnZombie(randX,randY,Content);

            if (enemy.fatsos.Count < 2)
                enemy.SpawnFatso(randX, randY, Content);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            player.Update(gameTime);

            //fatso.Update(gameTime, player.playerPosition,player);

            foreach (Enemy zombie in enemy.zombies)
            {
                zombie.Update(gameTime, player.playerPosition, player);
            }
            foreach (Enemy fatso in enemy.fatsos)
            {
                fatso.Update(gameTime, player.playerPosition, player);
            }

            for (int i = 0; i < player.bullets.Count; i++)
            {
                //Sprawdzanie czy zombie koliduje z kulami
                for (int j=0; j < enemy.zombies.Count;j++)
                {
                    if (enemy.zombies[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        enemy.zombies[j].DecreaseHP(50);
                    }  
                }
                for (int j = 0; j < enemy.fatsos.Count; j++)
                {
                    //Sprawdzanie czy fatso koliduje z kulami
                    if (enemy.fatsos[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        enemy.fatsos[j].DecreaseHP(50);
                    }
                }
            }

            

            for (int i = 0; i < enemy.zombies.Count; i++)
            {
                if (!enemy.zombies[i].isVisible)
                {
                    enemy.zombies.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < enemy.fatsos.Count; i++)
            {
                if (!enemy.fatsos[i].isVisible)
                {
                    enemy.fatsos.RemoveAt(i);
                    i--;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            //zombie.Draw(spriteBatch);
            //fatso.Draw(spriteBatch);
            
            foreach (Enemy zombie in enemy.zombies)
            {
                zombie.Draw(spriteBatch);
            }

            foreach (Enemy fatso in enemy.fatsos)
            {
                fatso.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
