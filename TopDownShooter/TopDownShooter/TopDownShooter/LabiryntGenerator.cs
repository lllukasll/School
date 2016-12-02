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
        //public int number = 1;

        public LabiryntGenerator(ContentManager Content)
        {
            /*
            sizeX = ileKolumn;
            sizeY = ileWierszy;
            rooms = new Room[sizeY, sizeX];

            for(int i = 0; i<sizeY;i++)
            {
                for(int j=0;j<sizeX;j++)
                {
                    rooms[i, j] = new Room(0,Content);
                }
            }

            //number = 1;*/
            rooms = new Room[5, 5];
            int[,] tab = new int[,]
            {
                {3,5,14,10,6 },
                {8,11,13,4,9 },
                {5,7,1,2,11 },
                {13,10,14,14,7 },
                {8,4,1,8,4 }
            };

            roomsStructure(tab, 5, 5, Content);
        }

        
        public void Maze1(ContentManager Content)
        {
            rooms = new Room[5, 5];
            int[,] tab = new int[,]
            {
                {3,5,14,10,6 },
                {8,11,13,4,9 },
                {5,7,1,2,11 },
                {13,10,14,14,7 },
                {8,4,1,8,4 }
            };

            roomsStructure(tab, 5, 5, Content);
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
