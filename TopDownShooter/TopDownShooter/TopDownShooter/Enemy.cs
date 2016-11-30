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
    class Enemy : CharacterAnimationManager
    {
        //Czas przez ktory postac wygląda jakby była zraniona
        private int harmTime;
        //Odstęp między kolejnymi atakami
        private int attackDelay;
        //Sprawdzanie czy postac aktualnie atakuje
        private bool isAttacking;
        //Zmienna tymczasowa przechowujaca wartocs attackDelay
        private int tmpAttackDelay;

        public Enemy() { }

        public Enemy(Vector2 Position, float BaseSpeed, int Hp, int Strenght, int AttackDelay, Texture2D texture)
        {
            baseSpeed = BaseSpeed;
            position = Position;
            hp = Hp;
            strenght = Strenght;
            attackDelay = AttackDelay;
            isAttacking = false;
            tmpAttackDelay = attackDelay;
            harmTime = 4;
            rotation = 0;
            //character.FramesPerSecond = 8;
            isVisible = false;

            character = new SpriteAnimation(texture, 4, 6);
            character.Position = position;

            AnimAdd("move", 1, 4);
            AnimAdd("attack", 2, 2);
            AnimAdd("spawn", 3, 2);
            AnimAdd("moveHarm", 4, 4);
            AnimAdd("attackHarm", 5, 2);
        }

        
        public void Initialize(Vector2 Position, float BaseSpeed, int Hp, int Strenght, int AttackDelay)
        {
            baseSpeed = BaseSpeed;
            position = Position;
            hp = Hp;
            strenght = Strenght;
            attackDelay = AttackDelay;
            isAttacking = false;
            tmpAttackDelay = attackDelay;
            harmTime = 4;
            rotation = 0;
            character.FramesPerSecond = 8;
        }

        
        public void LoadContent(ContentManager Content, string Name)
        {
            
            Texture2D texture = Content.Load<Texture2D>(Name);
            character = new SpriteAnimation(texture, 4, 6);
            character.Position = position;

            AnimAdd("move", 1, 4);
            AnimAdd("attack", 2, 2);
            AnimAdd("spawn", 3, 2);
            AnimAdd("moveHarm", 4, 4);
            AnimAdd("attackHarm", 5, 2);
        }

        public void Update(GameTime gameTime, Vector2 target, Player player)
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
                {
                    character.FramesPerSecond = 4;
                    AnimChoose("attackHarm");
                    //player.DecreaseHP(strenght);
                    isAttacking = true;
                }
                else//Jeżeli nie zaatakowany to zmiania animacje na atak
                {
                    character.FramesPerSecond = 4;
                    AnimChoose("attack");
                    //player.DecreaseHP(strenght);
                    isAttacking = true;
                }   
            }
            else // Jeżeli nie następuje kolizja z graczem
            {
                //Jeżeli jest atakowany to zmienia animacje na poruszanie i zatakowany
                if (harm == true)
                {
                    character.FramesPerSecond = 4;
                    AnimChoose("moveHarm");
                    isAttacking = false;
                }  
                else // Jeżeli nie jest atakowany to zmienia animacje na poruszanie
                {
                    character.FramesPerSecond = 8;
                    AnimChoose("move");
                    isAttacking = false;
                } 
            }

            //Jeżeli atakuje to zaczyna odliczac od wartosci attackDelay w dol jak dojdzie do zera to mozliwy kolejny atak
            if(isAttacking)
            {
                if(tmpAttackDelay==attackDelay)
                {
                    player.DecreaseHP(strenght);
                    tmpAttackDelay--;
                }else
                {
                    tmpAttackDelay--;
                    if(tmpAttackDelay == 0)
                    {
                        tmpAttackDelay = attackDelay;
                    }
                }
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

