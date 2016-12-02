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
        int sizeX, sizeY;

        public LabiryntGenerator(int ileWierszy,int ileKolumn,ContentManager Content)
        {
            sizeX = ileKolumn;
            sizeY = ileWierszy;
            rooms = new Room[sizeX, sizeY];

            for(int i = 0; i<= ileWierszy-1;i++)
            {
                for(int j=0;j<=ileKolumn-1;j++)
                {
                    rooms[j, i] = new Room(0, Content, new Vector2(0, 0));
                }
            }

           
        }

        public void genarateMaze()
        {
            int visitedCount = 1;
            int cellsCount = sizeX * sizeY;

            Queue<Vector2> visitedCells = new Queue<Vector2>();
            Random rnd = new Random();
            int x = rnd.Next(1, sizeX - 1);
            int y = rnd.Next(1, sizeY - 1);
            
            while(visitedCount < cellsCount)
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
                        rooms[y, x].visited = true;
                        x--;
                    }
                    else if (dir == "right")
                    {
                        rooms[y, x].door3 = true;
                        rooms[y, x].visited = true;
                        x++;
                    }
                    else if (dir == "top")
                    {
                        rooms[y, x].door2 = true;
                        rooms[y, x].visited = true;
                        y++;
                    }
                    else if (dir == "bottom")
                    {
                        rooms[y, x].door3 = true;
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
