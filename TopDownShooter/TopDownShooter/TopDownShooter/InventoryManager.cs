﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace TopDownShooter
{
    class InventoryManager
    {
        public List<Inventory> inventory = new List<Inventory>();

        public void DrawInventory(ContentManager Content,Vector2 Position)
        {
            Inventory field = new Inventory(Content.Load<Texture2D>("InventoryBorder"), Position);

            inventory.Add(field);
        }
    }
}
