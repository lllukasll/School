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
    class EnemyManager
    {
        public List<Enemy> zombies = new List<Enemy>();
        public List<Enemy> fatsos = new List<Enemy>();
        public List<Enemy> roots = new List<Enemy>();
        public List<Enemy> crows = new List<Enemy>();

        public void SpawnZombie(float x, float y,ContentManager Content)
        {
            Enemy zombie = new Enemy(new Vector2(x, y), 1f, 150, 10, 30, Content.Load<Texture2D>("Zombie"));
            zombie.isVisible = true;

            zombies.Add(zombie);
        }

        public void SpawnFatso(float x, float y,ContentManager Content)
        {
            Enemy fatso = new Enemy(new Vector2(x, y), 1f, 150, 10, 30, Content.Load<Texture2D>("Fatso"));
            fatso.isVisible = true;

            fatsos.Add(fatso);
        }

        public void SpawnRoot(float x, float y, ContentManager Content)
        {
            Enemy root = new Enemy(new Vector2(x, y),1.5f, 100, 5, 20, Content.Load<Texture2D>("Root"));
            root.isVisible = true;

            roots.Add(root);
        }

        public void SpawnCrow(float x, float y, ContentManager Content)
        {
            Enemy crow = new Enemy(new Vector2(x, y), 2f, 50, 5, 15, Content.Load<Texture2D>("Crow"));
            crow.isVisible = true;

            roots.Add(crow);
        }

        public void Update(GameTime gameTime,Player player)
        {
            //Wykonaj polecenie Update dla każdego aktywnego zombie
            foreach (Enemy zombie in zombies)
            {
                zombie.Update(gameTime, player.playerPosition, player);
            }

            //Wykonaj polecenie Update dla każdego aktywnego fatso
            foreach (Enemy fatso in fatsos)
            {
                fatso.Update(gameTime, player.playerPosition, player);
            }

            //Wykonaj polecenie Update dla każdego aktywnego fatso
            foreach (Enemy root in roots)
            {
                root.Update(gameTime, player.playerPosition, player);
            }

            //Wykonaj polecenie Update dla każdego aktywnego fatso
            foreach (Enemy crow in crows)
            {
                crow.Update(gameTime, player.playerPosition, player);
            }

            for (int i = 0; i < player.bullets.Count; i++)
            {
                //Sprawdzanie czy zombie koliduje z kulami
                for (int j = 0; j < zombies.Count; j++)
                {
                    if (zombies[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        zombies[j].DecreaseHP(player.strength);
                    }
                }
                for (int j = 0; j < fatsos.Count; j++)
                {
                    //Sprawdzanie czy fatso koliduje z kulami
                    if (fatsos[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        fatsos[j].DecreaseHP(player.strength);
                    }
                }
                for (int j = 0; j < roots.Count; j++)
                {
                    //Sprawdzanie czy root koliduje z kulami
                    if (roots[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        roots[j].DecreaseHP(player.strength);
                    }
                }
                for (int j = 0; j < crows.Count; j++)
                {
                    //Sprawdzanie czy crow koliduje z kulami
                    if (crows[j].boundingBox.Intersects(player.bullets[i].boundingBox))
                    {
                        player.bullets.ElementAt(i).isVisible = false;
                        crows[j].DecreaseHP(player.strength);
                    }
                }
            }

            //Sprawdzenie czy ktorys z zombie jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < zombies.Count; i++)
            {
                if (!zombies[i].isVisible)
                {
                    zombies.RemoveAt(i);
                    i--;
                }
            }

            //Sprawdzenie czy ktorys z fatso jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < fatsos.Count; i++)
            {
                if (!fatsos[i].isVisible)
                {
                    fatsos.RemoveAt(i);
                    i--;
                }
            }

            //Sprawdzenie czy ktorys z roots jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < roots.Count; i++)
            {
                if (!roots[i].isVisible)
                {
                    roots.RemoveAt(i);
                    i--;
                }
            }

            //Sprawdzenie czy ktorys z crows jest niewidoczny , jeśli tak to usun go z listy
            for (int i = 0; i < crows.Count; i++)
            {
                if (!roots[i].isVisible)
                {
                    crows.RemoveAt(i);
                    i--;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Wyswietla wszystkie zombie
            foreach (Enemy zombie in zombies)
            {
                zombie.Draw(spriteBatch);
            }

            //Wyswietla wszystkie fatso
            foreach (Enemy fatso in fatsos)
            {
                fatso.Draw(spriteBatch);
            }

            //Wyswietla wszystkie roots
            foreach (Enemy root in roots)
            {
                root.Draw(spriteBatch);
            }

            //Wyswietla wszystkie roots
            foreach (Enemy crow in crows)
            {
                crow.Draw(spriteBatch);
            }
        }

    }
}
