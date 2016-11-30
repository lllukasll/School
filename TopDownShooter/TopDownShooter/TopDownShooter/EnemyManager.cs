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
    }
}
