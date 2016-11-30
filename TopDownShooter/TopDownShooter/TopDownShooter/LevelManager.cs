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
        Player player = new Player();
        EnemyManager enemy = new EnemyManager();
        Gun Machinegun = new Gun();
        GunManager gun = new GunManager();
        //Inventory inventory = new Inventory();
        //InventoryManager 
        InventoryManager inventoryManager = new InventoryManager();

        public void Initialize(ContentManager Content)
        {
            player.Initialize(Content);
            Machinegun.Initialize(Content.Load<Texture2D>("Machinegun"), new Vector2(300, 300),
                "Machinegun");

           // inventory.Initialize(new Vector2(10, 64));
        }

        public void LoadContent(ContentManager Content)
        {
            player.LoadContent(Content);
            Machinegun.LoadContent(Content);
            //inventory.LoadContent(Content);
        }

        public void Update(GameTime gameTime,ContentManager Content)
        {

            

            //Losowe położenie X i Y 
            Random rnd = new Random();
            int randX = rnd.Next(100, 400);
            int randY = rnd.Next(100, 400);
            int randX2 = rnd.Next(100, 400);
            int randY2 = rnd.Next(100, 400);

            //Jeżeli jest mniej niż 3 zombie dodaj je do listy w losowych miejscach
            if (enemy.zombies.Count < 3)
                enemy.SpawnZombie(randX, randY, Content);

            //Jeżeli jest mniej niż 3 fatso dodaj je do listy w losowych miejscach
            if (enemy.fatsos.Count < 2)
                enemy.SpawnFatso(randX, randY, Content);

            //Sprawdzenie czy broni jest mniej niz 2 jesli tak to dodaj je do listy
            if (gun.guns.Count < 2)
            {
                gun.SpawnGun("Machinegun", "Machinegun", new Vector2(randX, randY), 1, Content);
                gun.SpawnGun("Shotgun", "Shotgun", new Vector2(randX2, randY2), 1, Content);
            }
                

            
            player.Update(gameTime);

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

            //Wykonaj polecenie Update dla każdego aktywnego fatso
            foreach (Gun gun in gun.guns)
            {
                gun.Update(gameTime, player, inventoryManager);
            }

            for (int i = 0; i < player.bullets.Count; i++)
            {
                //Sprawdzanie czy zombie koliduje z kulami
                for (int j = 0; j < enemy.zombies.Count; j++)
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

            //inventory.Update(gameTime, Content);

            //Machinegun.Update(gameTime, player, inventory);


            //InventoryManager , jeżeli pól jes mniej niż 10 dodaj je do listy 
            if (inventoryManager.inventory.Count < 10)
                inventoryManager.DrawInventory(Content, new Vector2(10, 64 + inventoryManager.inventory.Count * 32));
            //Wykonaj polecenie Update dla każdego pola w inventory
            foreach (Inventory field in inventoryManager.inventory)
            {
                field.Update(gameTime, Content);
            }

            //Sprawdzenie czy ktoras z broni jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < gun.guns.Count; i++)
            {
                if (!gun.guns[i].isVisible)
                {
                    gun.guns.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Machinegun.Draw(spriteBatch);
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

            foreach (Gun gun in gun.guns)
            {
                gun.Draw(spriteBatch);
            }
            foreach (Inventory field in inventoryManager.inventory)
            {
                field.Draw(spriteBatch);
            }
            //inventory.Draw(spriteBatch);
        }
    }
}
