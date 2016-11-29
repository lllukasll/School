using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TopDownShooter
{
    public class SpriteManager
    {
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;

        protected Dictionary<string, Rectangle[]> Animations =
            new Dictionary<string, Rectangle[]>();
        protected int FrameIndex = 0;
        public string Animation;

        protected int Frames;
        private int height;
        private int width;

        public SpriteManager(Texture2D Texture, int Frames, int animations)
        {
            this.Texture = Texture;
            this.Frames = Frames;
            width = Texture.Width / Frames;
            height = Texture.Height / animations;
        }

        public void AddAnimation(string name, int row)
        {
            Rectangle[] recs = new Rectangle[Frames];
            for (int i = 0; i < Frames; i++)
            {
                recs[i] = new Rectangle(i * width,
                    (row - 1) * height, width, height);
            }
            Animations.Add(name, recs);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position,
                Animations[Animation][FrameIndex],
                Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }
    }
}
