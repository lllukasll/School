using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TopDownShooter
{
    public abstract class SpriteManager
    {
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        protected Dictionary<string, AnimationClass> Animations =
            new Dictionary<string, AnimationClass>();
        protected int FrameIndex = 0;
        private string animation;
        public string Animation
        {
            get { return animation; }
            set
            {
                animation = value;
                FrameIndex = 0;
            }
        }
        protected Vector2 Origin;

        private int height;
        private int width;

        public SpriteManager(Texture2D Texture, int Frames, int animations)
        {
            this.Texture = Texture;
            width = Texture.Width / Frames;
            height = Texture.Height / animations;
            Origin = new Vector2(width / 2, height / 2);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position,
                Animations[Animation].Rectangles[FrameIndex],
                Animations[Animation].Color,
                Animations[Animation].Rotation, Origin,
                Animations[Animation].Scale,
                Animations[Animation].SpriteEffect, 0f);
        }

        public void AddAnimation(string name, int row, int frames, AnimationClass animation)
        {
            Rectangle[] recs = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                recs[i] = new Rectangle(i * width, (row - 1) * height, width, height);
            }
            animation.Frames = frames;
            animation.Rectangles = recs;
            Animations.Add(name, animation);
        }
    }
}
