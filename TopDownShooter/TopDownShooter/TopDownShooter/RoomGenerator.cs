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
            //Jeśli będe chciał spawnowac pokoje to musze dodac tu do Contentu roomNumber i Psoition
            Room room = new Room(RoomNumber,Content);
            rooms.Add(room);
        }

        

        public void Room1()
        {

        }
    }
}
