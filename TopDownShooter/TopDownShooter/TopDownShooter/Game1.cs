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
        Gun Machinegun = new Gun();
        GunManager gun = new GunManager();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            player.Initialize(Content);
            Machinegun.Initialize(Content.Load<Texture2D>("2h_machinegun"), new Vector2(300, 300),
                "Machinegun");
            
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            Machinegun.LoadContent(Content);
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

            //Je¿eli jest mniej ni¿ 3 zombie dodaj je do listy w losowych miejscach
            if (enemy.zombies.Count < 3)
                enemy.SpawnZombie(randX,randY,Content);

            //Je¿eli jest mniej ni¿ 3 fatso dodaj je do listy w losowych miejscach
            if (enemy.fatsos.Count < 2)
                enemy.SpawnFatso(randX, randY, Content);

            //Sprawdzenie czy broni jest mniej niz 2 jesli tak to dodaj je do listy
            if (gun.guns.Count < 2)
                gun.SpawnGun("2h_machinegun", "Machinegun", new Vector2(randX, randY), 1, Content);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            player.Update(gameTime);

            //Wykonaj polecenie Update dla ka¿dego aktywnego zombie
            foreach (Enemy zombie in enemy.zombies)
            {
                zombie.Update(gameTime, player.playerPosition, player);
            }

            //Wykonaj polecenie Update dla ka¿dego aktywnego fatso
            foreach (Enemy fatso in enemy.fatsos)
            {
                fatso.Update(gameTime, player.playerPosition, player);
            }

            //Wykonaj polecenie Update dla ka¿dego aktywnego fatso
            foreach (Gun gun in gun.guns)
            {
                gun.Update(gameTime,player);
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

            
            //Sprawdzenie czy ktorys z zombie jest niewidoczny , jeœli tak to usun go z listy
            for (int i = 0; i < enemy.zombies.Count; i++)
            {
                if (!enemy.zombies[i].isVisible)
                {
                    enemy.zombies.RemoveAt(i);
                    i--;
                }
            }

            //Sprawdzenie czy ktorys z fatso jest niewidoczny , jeœli tak to usun go z listy
            for (int i = 0; i < enemy.fatsos.Count; i++)
            {
                if (!enemy.fatsos[i].isVisible)
                {
                    enemy.fatsos.RemoveAt(i);
                    i--;
                }
            }

            Machinegun.Update(gameTime, player);

            

            //Sprawdzenie czy ktoras z broni jest niewidoczny , jeœli tak to usun go z listy
            for (int i = 0; i < gun.guns.Count; i++)
            {
                if (!gun.guns[i].isVisible)
                {
                    gun.guns.RemoveAt(i);
                    i--;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Machinegun.Draw(spriteBatch);
            player.Draw(spriteBatch);
            //zombie.Draw(spriteBatch);
            //fatso.Draw(spriteBatch);
            
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

            foreach (Gun gun in gun.guns)
            {
                gun.Draw(spriteBatch);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
