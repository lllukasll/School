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
    class LabiryntGenerator
    {
        public Room[,] rooms;
        public int sizeX, sizeY;
        int activeRoomX;
        int activeRoomY;
        

        public bool roomActivated = true;

        public LabiryntGenerator() { }
     
        public void Maze1(ContentManager Content,LevelManager level)
        {
            rooms = new Room[5, 5];
            int[,] tab = new int[,]
            {
                {3,5,14,10,6 },
                {8,11,13,20,9 },
                {5,7,1,2,11 },
                {13,10,14,14,7 },
                {8,4,1,8,4 }
            };

            roomsStructure(tab, 5, 5, Content);

            if(tab[activeRoomX,activeRoomY]==1)
            {
                if (roomActivated)
                {
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            
            if (tab[activeRoomX, activeRoomY] == 6)
            {
                if (roomActivated)
                {

                    level.enemy.SpawnZombie(300, 110, Content);
                    level.enemy.SpawnCrow(200, 220, Content);
                    level.enemy.SpawnRoot(100, 300, Content);
                    level.enemy.SpawnRoot(400, 350, Content);
                    level.enemy.SpawnRoot(500, 190, Content);
                    level.enemy.SpawnRoot(300, 200, Content);
                    level.enemy.SpawnCrow(100, 200, Content);
                    level.enemy.SpawnCrow(100, 200, Content);
                    level.enemy.SpawnCrow(400, 160, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 9)
            {
                if (roomActivated)
                {

                    level.enemy.SpawnZombie(300, 110, Content);
                    level.enemy.SpawnZombie(200, 220, Content);
                    level.enemy.SpawnRoot(100, 300, Content);
                    level.enemy.SpawnRoot(400, 350, Content);
                    level.enemy.SpawnFatso(500, 190, Content);
                    level.enemy.SpawnFatso(300, 200, Content);
                    level.enemy.SpawnCrow(100, 200, Content);
                    level.enemy.SpawnCrow(100, 200, Content);
                    level.enemy.SpawnCrow(400, 160, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 5)
            {
                if (roomActivated)
                {

                    level.enemy.SpawnZombie(300, 110, Content);
                    level.enemy.SpawnZombie(200, 220, Content);
                    level.enemy.SpawnZombie(100, 300, Content);
                    level.enemy.SpawnZombie(400, 350, Content);
                    level.enemy.SpawnFatso(500, 190, Content);
                    level.enemy.SpawnFatso(300, 200, Content);
                    level.enemy.SpawnFatso(100, 200, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 2)
            {
                if (roomActivated)
                {
                    
                    level.enemy.SpawnZombie(300, 110, Content);
                    level.enemy.SpawnZombie(200, 220, Content);
                    level.enemy.SpawnZombie(100, 300, Content);
                    level.enemy.SpawnZombie(400, 350, Content);
                    level.enemy.SpawnZombie(500, 190, Content);
                    level.enemy.SpawnZombie(300, 200, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 11)
            {
                if (roomActivated)
                {
                    level.enemy.SpawnRoot(130, 100, Content);
                    level.enemy.SpawnFatso(500, 200, Content);
                    level.enemy.SpawnZombie(300, 300, Content);
                    level.enemy.SpawnCrow(400, 150, Content);
                    level.enemy.SpawnCrow(320, 120, Content);
                    level.enemy.SpawnFatso(500, 300, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 4)
            {
                if (roomActivated)
                {
                    level.enemy.SpawnRoot(130, 100, Content);
                    level.enemy.SpawnRoot(500, 200, Content);
                    level.enemy.SpawnRoot(300, 300, Content);
                    level.enemy.SpawnRoot(400, 150, Content);
                    level.enemy.SpawnRoot(320, 120, Content);
                   
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 13)
            {
                if (roomActivated)
                {
                    level.enemy.SpawnCrow(130, 100, Content);
                    level.enemy.SpawnCrow(500, 200, Content);
                    level.enemy.SpawnCrow(300, 300, Content);
                    level.enemy.SpawnCrow(400, 150, Content);
                    level.enemy.SpawnCrow(320, 120, Content);
                    level.enemy.SpawnCrow(300, 110, Content);
                    level.enemy.SpawnCrow(370, 100, Content);
                    level.enemy.SpawnCrow(123, 180, Content);
                    level.enemy.SpawnCrow(390, 220, Content);
                    level.enemy.SpawnCrow(100, 100, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }

            if (tab[activeRoomX, activeRoomY] == 14)
            {
                if (roomActivated)
                {
                    //rooms[activeRoomX, activeRoomY].enemyKilled = false;
                    level.enemy.SpawnZombie(100, 100, Content);
                    level.enemy.SpawnZombie(550, 100, Content);
                    level.enemy.SpawnCrow(130, 100, Content);
                    level.enemy.SpawnCrow(500, 100, Content);
                    level.enemy.SpawnCrow(300, 100, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }

            if (tab[activeRoomX, activeRoomY] == 10)
            {
                if (roomActivated)
                {
                    //rooms[activeRoomX, activeRoomY].enemyKilled = false;
                    level.enemy.SpawnFatso(100, 100, Content);
                    level.enemy.SpawnFatso(550, 100, Content);
                    level.enemy.SpawnCrow(130, 100, Content);
                    level.enemy.SpawnCrow(500, 100, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 7)
            {
                if (roomActivated)
                {
                    //rooms[activeRoomX, activeRoomY].enemyKilled = false;
                    level.enemy.SpawnFatso(100, 100, Content);
                    level.enemy.SpawnFatso(550, 100, Content);
                    level.enemy.SpawnFatso(550, 100, Content);
                    level.enemy.SpawnCrow(500, 100, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }
            if (tab[activeRoomX, activeRoomY] == 8)
            {
                if (roomActivated)
                {
                    //rooms[activeRoomX, activeRoomY].enemyKilled = false;
                    level.enemy.SpawnFatso(100, 400, Content);
                    level.enemy.SpawnFatso(550, 400, Content);
                    level.enemy.SpawnFatso(550, 400, Content);
                    level.enemy.SpawnRoot(500, 400, Content);
                    level.enemy.SpawnRoot(300, 400, Content);
                    roomActivated = false;
                }
                if (level.enemy.fatsos.Count < 1 && level.enemy.crows.Count < 1 && level.enemy.roots.Count < 1 && level.enemy.zombies.Count < 1)
                    rooms[activeRoomX, activeRoomY].enemyKilled = true;
            }

        }

        public void activeRoom(int X,int Y)
        {
            activeRoomX = X;
            activeRoomY = Y;
        }

        public void roomsStructure(int[,] tab,int maxX,int maxY,ContentManager Content)
        {
            for(int i=0;i<=maxX-1;i++)
            {
                for(int j=0;j<= maxY-1;j++)
                {
                    rooms[i, j] = new Room(tab[i, j], Content);
                }
            }
        }
















        /*

        public void Initialize(int sizeX,int sizeY,ContentManager Content)
        {
            rooms = new Room[sizeY, sizeX];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    rooms[y, x] = new Room(Content);
        }

        */
        public void genarateMaze()
        {
            
            int visitedCount = 1;
            int cellsCount = sizeX * sizeY;

            Queue<Vector2> visitedCells = new Queue<Vector2>();
            Random rnd = new Random();
            int x = rnd.Next(1, sizeX -1);
            int y = rnd.Next(1, sizeY -1);

            
            while (visitedCount < cellsCount)
            {
                string[] possibleDirection = new string[4];
                int possibleDirCount = 0;

                if (x > 0 && !rooms[y, x - 1].visited)
                    possibleDirection[possibleDirCount++] = "left";

                if (x < sizeX-1 && !rooms[y, x + 1].visited)
                    possibleDirection[possibleDirCount++] = "right";

                if (y > 0 && !rooms[y-1, x].visited)
                    possibleDirection[possibleDirCount++] = "bottom";

                if (y < sizeY-1 && !rooms[y+1,x].visited)
                    possibleDirection[possibleDirCount++] = "top";


                if(possibleDirCount != 0)
                {
                    int random = rnd.Next(0, possibleDirCount);
                    string dir = possibleDirection[random];

                    if(dir == "left")
                    {
                        rooms[y, x].door1 = true;
                        //rooms[y, x - 1].door1 = true;
                        /*
                        if (x > 0)
                            rooms[y, x - 1].door3 = true;
                            */
                        rooms[y, x].visited = true;
                        x--;
                    }
                    else if (dir == "right")
                    {
                        rooms[y, x].door3 = true;
                        //rooms[y, x + 1].door1 = true;
                        /*
                        if (x < sizeX-1)
                            rooms[y, x + 1].door1 = true;
                            */
                        rooms[y, x].visited = true;
                        x++;
                    }
                    else if (dir == "top")
                    {
                        rooms[y, x].door2 = true;
                        //rooms[y-1, x].door1 = true;
                        /*
                        if (y > 0)
                            rooms[y-1, x].door0 = true;
                            */
                        rooms[y, x].visited = true;
                        y++;
                    }
                    else if (dir == "bottom")
                    {
                        rooms[y, x].door0 = true;
                        //rooms[y+1, x].door2 = true;
                        /*
                        if (y < sizeY-1)
                            rooms[y+1, x].door2 = true;
                            */
                        rooms[y, x].visited = true;
                        y--;
                    }
                    rooms[y, x].visited = true;
                    visitedCount++;
                    visitedCells.Enqueue(new Vector2(x, y));

                }
                else
                {
                    Vector2 retrace = visitedCells.Dequeue();
                    x = (int)retrace.X;
                    y = (int)retrace.Y;
                }
            }

        }
    }
}
