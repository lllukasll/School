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
    class GunManager
    {
        public List<Gun> guns = new List<Gun>();

        public void SpawnGun(string SpriteSheetName,string GunName,Vector2 Position,int GunIndex,ContentManager Content)
        {
            Gun gun = new Gun(Content.Load<Texture2D>(SpriteSheetName), Position, GunName, GunIndex,Content);

            guns.Add(gun);
        }
    }
}
