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
    class ItemManager
    {
        public List<Item> items = new List<Item>();

        public void SpawnItem(string SpriteSheetName,string GunName,Vector2 Position,int GunIndex,ContentManager Content,int Quantity,int MaxQuantity, int Type)
        {
            Item item = new Item(Content.Load<Texture2D>(SpriteSheetName), Position, GunName, GunIndex,Type,Quantity,MaxQuantity,Content);

            items.Add(item);
        }
    }
}
