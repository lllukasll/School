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
    class Zombie : CharacterAnimationManager
    {
        private int harmTime;

        public void Initialize(Vector2 Position, float BaseSpeed)
        {
            character.FramesPerSecond = 8;
            baseSpeed = BaseSpeed;
            rotation = 0;
            position = Position;
            hp = 150;
            harmTime = 4;
        }

        public void LoadContent(ContentManager Content)
        {
            Texture2D texture = Content.Load<Texture2D>("ZombieAnimations2");
            character = new SpriteAnimation(texture, 4, 6);
            character.Position = position;

            AnimAdd("move", 1, 4);
            AnimAdd("attack", 2, 2);
            AnimAdd("spawn", 3, 2);
            AnimAdd("moveHarm", 4, 4);
            AnimAdd("attackHarm", 5, 2);
        }

        public void Update(GameTime gameTime, Vector2 target)
        {
            //Ustawia ramke do kolizji na nowo w każdej klatce
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            //Podąża za targetem
            Follow(target);

            //Jeżeli nasepuje kolizja z graczem
            if (collision == true)
            {
                //Jeżeli zatakowany to zmienia animacje na atak i zaatakowany 
                if (harm == true)
                    AnimChoose("attackHarm");
                else//Jeżeli nie zaatakowany to zmiania animacje na atak
                    AnimChoose("attack");
            }
            else // Jeżeli nie następuje kolizja z graczem
            {
                //Jeżeli jest atakowany to zmienia animacje na poruszanie i zatakowany
                if (harm == true)
                    AnimChoose("moveHarm");
                else // Jeżeli nie jest atakowany to zmienia animacje na poruszanie
                    AnimChoose("move");
            }


            //Jeżeli zaatakoany to odejmuje 1 od licznika czasu zmiany animacji na atakowanego
            if (harm)
                harmTime--;

            //Jeżeli licznik mniejszy lub rowny zero to postac juz nie jest atakowana
            if (harmTime <= 0)
            {
                harm = false;
                harmTime = 4;
            }


            //Jeżeli hp mniej lub rowne zero to postac staje sie niewidoczna
            if (hp <= 0)
                isVisible = false;

            character.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
                character.Draw(spriteBatch);
        }

        //Metoda odejmująca HP i ustawiajaca bool atakowany na true
        public void DecreaseHP(int ammountToDecrease)
        {
            hp -= ammountToDecrease;
            harm = true;
        }
    }
}

