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
        public void Initialize(Vector2 Position, float BaseSpeed)
        {
            character.FramesPerSecond = 8;
            baseSpeed = BaseSpeed;
            rotation = 0;
            position = Position;

        }

        public void LoadContent(ContentManager Content)
        {
            Texture2D texture = Content.Load<Texture2D>("ZombieAnimations");
            character = new SpriteAnimation(texture, 4, 3);
            character.Position = position;

            AnimAdd("move", 1, 4);
            AnimAdd("attack", 2, 2);
            AnimAdd("spawn", 3, 2);

        }

        public void Update(GameTime gameTime, Vector2 target)
        {

            //Podąża za targetem
            Follow(target);

            //Jeżeli nasepuje kolizja z graczem
            if (collision == true)
            {
                AnimChoose("attack");
            }
            else // Jeżeli nie następuje kolizja z graczem
            {

                AnimChoose("move");
            }
 
            character.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

             character.Draw(spriteBatch);
        }

    }
}
