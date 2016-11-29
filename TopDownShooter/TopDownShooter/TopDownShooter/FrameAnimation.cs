using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TopDownShooter
{
    class FrameAnimation : SpriteManager
    {
        public FrameAnimation(Texture2D Texture, int frames, int animations)
            : base(Texture, frames, animations)
        {
        }

        public void SetFrame(int frame)
        {
            if (frame < Animations[Animation].Frames)
                FrameIndex = frame;
        }
    }
}
