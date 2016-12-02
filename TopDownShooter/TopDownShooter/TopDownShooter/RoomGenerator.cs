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
    class RoomGenerator
    {
        public List<Room> rooms = new List<Room>();

        public void SpawnRoom(int RoomNumber,ContentManager Content,Vector2 Position)
        {
            Room room = new Room(RoomNumber,Content,Position);
            rooms.Add(room);
        }


    }
}
