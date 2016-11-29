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
    class Zombie
    {
        public SpriteAnimation character;
        public float baseSpeed;
        public float rotation;
        public Vector2 velocity, position;
        public float distance;

        public void Initialize(Vector2 Position, float BaseSpeed)
        {
            character.FramesPerSecond = 8;
            baseSpeed = BaseSpeed;
            rotation = 0;
            position = Position;
            character.Animation = "Move";
        }

        public void LoadContent(ContentManager Content)
        {
            Texture2D texture = Content.Load<Texture2D>("ZombieAnimations");
            character = new SpriteAnimation(texture, 4, 3);
            character.Position = position;

            AnimationClass ani = new AnimationClass();

            character.AddAnimation("Move", 1, 4, ani.Copy());
            character.AddAnimation("Idle", 1, 1, ani.Copy());
        }

        public void Update(GameTime gameTime, Vector2 target)
        {

            Follow(target);

            character.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            character.Draw(spriteBatch);
        }

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
    }
}
