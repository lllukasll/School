using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class FrameAnimation : SpriteManager
    {
        public FrameAnimation(Texture2D Texture, int frames)
            : base(Texture, frames)
        {
        }

        public void SetFrame(int frame)
        {
            if (frame < Rectangles.Length)
                FrameIndex = frame;
        }
    }
}
