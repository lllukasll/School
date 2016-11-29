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
    class CharacterAnimationManager
    {


        public SpriteAnimation character;
        AnimationClass ani = new AnimationClass();
        //Rotacja postaci
        public float rotation;
        public Vector2 velocity, position;
        //Obramowanie do kolizji
        public Rectangle boundingBox;
        //Prędkość poruszania się
        public float baseSpeed;
        //kolizja
        public bool collision = false;
        //Czy jest widoczny
        public bool isVisible = true;
        //Czy zaatakowany
        public bool harm = false;
        //Ile HP
        public int hp;
        //Odległość od gracza
        public float distance;

        public void Follow(Vector2 target)
        {
            Vector2 direction = target - position; //Różnica między X i Y gracza i przeciwnika
            distance = calculateDistance(target, position); //Odległość miedzy graczem i przeciwnikiem
            velocity = Vector2.Zero; //Zeruje velocity

            rotation = (float)Math.Atan2((double)direction.Y, (double)direction.X); // Oblicza rotacje

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            if (distance < baseSpeed)
                velocity += direction * distance;
            else
                velocity += direction * baseSpeed;

            position += velocity;

            character.Position = position;

            if (distance < 30)
                collision = true;
            else
                collision = false;
        }

        private float calculateDistance(Vector2 A, Vector2 B)
        {
            A = new Vector2(Math.Abs(A.X), Math.Abs(A.Y));
            B = new Vector2(Math.Abs(B.X), Math.Abs(B.Y));

            float Y_diff, X_diff, distance;

            Y_diff = A.Y - B.Y;
            X_diff = A.X - B.X;

            distance = (float)Math.Sqrt(Math.Pow(X_diff, 2) + Math.Pow(Y_diff, 2));

            return Math.Abs(distance);
        }

        public void AnimAdd(string name, int row, int frames)
        {
            character.AddAnimation("0" + name, row, frames, ani.Copy());

            ani.Rotation = 0.375f;
            character.AddAnimation("1" + name, row, frames, ani.Copy());

            ani.Rotation = 0.75f;
            character.AddAnimation("2" + name, row, frames, ani.Copy());

            ani.Rotation = 1.125f;
            character.AddAnimation("3" + name, row, frames, ani.Copy());

            ani.Rotation = 1.5f;
            character.AddAnimation("4" + name, row, frames, ani.Copy());

            ani.Rotation = 1.875f;
            character.AddAnimation("5" + name, row, frames, ani.Copy());

            ani.Rotation = 2.25f;
            character.AddAnimation("6" + name, row, frames, ani.Copy());

            ani.Rotation = 2.625f;
            character.AddAnimation("7" + name, row, frames, ani.Copy());

            ani.Rotation = 2.625f;
            character.AddAnimation("8" + name, row, frames, ani.Copy());

            ani.Rotation = 3.0f;
            character.AddAnimation("9" + name, row, frames, ani.Copy());

            ani.Rotation = -2.625f;
            character.AddAnimation("10" + name, row, frames, ani.Copy());

            ani.Rotation = -2.25f;
            character.AddAnimation("11" + name, row, frames, ani.Copy());

            ani.Rotation = -1.875f;
            character.AddAnimation("12" + name, row, frames, ani.Copy());

            ani.Rotation = -1.5f;
            character.AddAnimation("13" + name, row, frames, ani.Copy());

            ani.Rotation = -1.125f;
            character.AddAnimation("14" + name, row, frames, ani.Copy());

            ani.Rotation = -0.75f;
            character.AddAnimation("15" + name, row, frames, ani.Copy());

            ani.Rotation = -0.375f;
            character.AddAnimation("16" + name, row, frames, ani.Copy());
        }

        public void AnimChoose(string name)
        {
            if (rotation >= -0.1875f && rotation < 0.1875f)
            {
                if (character.Animation != "0" + name)
                    character.Animation = "0" + name;
            }
            else if (rotation >= 0.1875f && rotation < 0.5125f)
            {
                if (character.Animation != "1" + name)
                    character.Animation = "1" + name;
            }
            else if (rotation >= 0.5125f && rotation < 0.9375f)
            {
                if (character.Animation != "2" + name)
                    character.Animation = "2" + name;
            }
            else if (rotation >= 0.9375f && rotation < 1.3125f)
            {
                if (character.Animation != "3" + name)
                    character.Animation = "3" + name;
            }
            else if (rotation >= 1.3125f && rotation < 1.6875f)
            {
                if (character.Animation != "4" + name)
                    character.Animation = "4" + name;
            }
            else if (rotation >= 1.6875f && rotation < 2.0625f)
            {
                if (character.Animation != "5" + name)
                    character.Animation = "5" + name;
            }
            else if (rotation >= 2.0625f && rotation < 2.4375f)
            {
                if (character.Animation != "6" + name)
                    character.Animation = "6" + name;
            }
            else if (rotation >= 2.4375f && rotation < 2.8125f)
            {
                if (character.Animation != "7" + name)
                    character.Animation = "7" + name;
            }
            else if (rotation >= 2.8125f && rotation < -2.8125f)
            {
                if (character.Animation != "8" + name)
                    character.Animation = "8" + name;
            }
            else if (rotation >= -2.8125f && rotation < -2.4375f)
            {
                if (character.Animation != "9" + name)
                    character.Animation = "9" + name;
            }
            else if (rotation >= -2.4375f && rotation < -2.0625f)
            {
                if (character.Animation != "10" + name)
                    character.Animation = "10" + name;
            }
            else if (rotation >= -2.0625f && rotation < -1.6875f)
            {
                if (character.Animation != "11" + name)
                    character.Animation = "11" + name;
            }
            else if (rotation >= -1.6875f && rotation < -1.3125f)
            {
                if (character.Animation != "12" + name)
                    character.Animation = "12" + name;
            }
            else if (rotation >= -1.3125f && rotation < -0.9375f)
            {
                if (character.Animation != "13" + name)
                    character.Animation = "13" + name;
            }
            else if (rotation >= -0.9375f && rotation < -0.5125f)
            {
                if (character.Animation != "14" + name)
                    character.Animation = "14" + name;
            }
            else if (rotation >= -0.5125f && rotation < -0.1875f)
            {
                if (character.Animation != "15" + name)
                    character.Animation = "15" + name;
            }
            else
            {
                if (character.Animation != "0" + name)
                    character.Animation = "0" + name;
            }
        }

    }

}

