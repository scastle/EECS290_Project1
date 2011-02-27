using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.MovingObjects;

namespace RecordRobot
{
    public class Maze
    {
        public static bool[,,] grid; //true for paths, false for walls

        public static int level;

        public static void LoadMaze(int[,,] map)
        {
            level = 0;
            int levels = map.GetUpperBound(0);
            int rows = map.GetUpperBound(1);
            int columns = map.GetUpperBound(2);
            grid = new bool[levels,rows,columns];
            for (int l = 0; l < levels; l++)
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < columns; c++)
                    {
                        if (map[l, r, c] == 0)
                        {
                            grid[l, r, c] = false;
                        }
                        else
                        {
                            grid[l, r, c] = true;
                        }
                    }
                }
            }
        }

        /*public static void LoadMaze(string file)
        {
            grid = new bool[,,]
            {
                {
                {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                {false, true , true , true , false, false, true , true , true , true , false, true , true , true , false},
                {false, true , false, true , true , true , true , false, false, true , true , true , false, true , false},
                {false, true , true , true , false, false, true , true , true , true , false, true , true , true , false},
                {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false}
                }
            };

            CollisionGrid = new CollisionType[,,]
            {
                {
                {CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall},
                {CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall},
                {CollisionType.wall, CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.wall},
                {CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall},
                {CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall}
                }
            };

            /*grid = new bool[15, 15];
            try
            {

                TextReader read = new StreamReader(file);
                string input = null;
                int r = 0;
                while ((input = read.ReadLine()) != null)
                {
                    for (int c = 0; c < input.Length; c++)
                    {
                        if (input.Substring(c, 1).Equals("1"))
                        {
                            grid[r, c] = true;
                            Console.Write("1");
                        }
                        else
                        {
                            grid[r, c] = false;
                            Console.Write("0");

                        }
                    }
                    r++;
                    Console.Write("/n");
                }
            }
            catch { }
            */

            //string filename = "RecordRobotContent/TextFiles/testmaze.txt";
            //string path = Path.Combine(StorageContainer.TitleLocation, filename);
            //int i ;
            //string lineOfText;
            //System.IO.Stream stream = TitleContainer.OpenStream("RecordRobotContent/TextFiles/testmaze.txt");
            //StreamReader sr = new StreamReader(filename);
            //while ((i = stream.ReadByte()) != -1)
            //{
                // do something
            //}

            /*
            StreamReader re = File.OpenText(file);
            string input = null;
            int r = 0;
            while ((input = re.ReadLine()) != null)
            {
                for (int c = 0; c < input.Length; c++)
                {
                    if (input.Substring(c, 1).Equals("1"))
                    {
                        grid[r, c] = true;
                        Console.Write("1");
                    }
                    else
                    {
                        grid[r, c] = false;
                        Console.Write("0");

                    }
                }
                r++;
                Console.Write("/n");
            }
            re.Close();
            */

        //}


        /// <summary>
        /// Checks if the specified point is an intersection
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <returns>true if the point is an intersection</returns>
        public static bool IsIntersection(Point p)
        {
            //return true;
            int r = p.Y / 30;
            int c = p.X / 30;

            if ((grid[level, r - 1, c] || grid[level, r + 1, c]) && (grid[level, r, c - 1] || grid[level, r, c + 1]) 
                && ((p.X - 15) % 30 == 0 && (p.Y - 15) % 30 == 0))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Checks if the robot can move in the current direction
        /// </summary>
        /// <param name="d">The direction to move</param>
        /// <returns>true if the robot can move in the direction</returns>
        public static bool CanGo(Point p, Direction d)
        {
            //return true;
            int r = p.Y / 30;
            int c = p.X / 30;
            int xsquare = p.X % 30;
            int ysquare = p.Y % 30;

            switch (d)
            {
                case Direction.Up:
                    if (grid[level, r - 1, c] )//|| ysquare > 15)
                        return true;
                    break;
                case Direction.Down:
                    if (grid[level, r + 1, c] )//|| ysquare < 15)
                        return true;
                    break;
                case Direction.Left:
                    if (grid[level, r, c - 1] )//|| xsquare > 15)
                        return true;
                    break;
                case Direction.Right:
                    if (grid[level, r, c + 1] )//|| xsquare < 15)
                        return true;
                    break;
            }
            return false;
        }


        /// <summary>
        /// Finds a location in the maze where it is safe to place one of the objects, i.e. not a wall and
        /// no records right next to the robot
        /// </summary>
        /// 
        /// <returns>Point where an object should be added</returns>
        public static Point getPointToPlace()
        {
            
            do
            {
                int r = Game1.rand.Next(grid.GetUpperBound(1) - 1);
                int c = Game1.rand.Next(grid.GetUpperBound(2) - 1);

                if (grid[level, r, c])
                {
                    return (new Point(c * 30 + 15, r * 30 + 15));
                }
                else
                {
                }

            } while (true) ;

        }


        public static void Draw()
        {
            Game1.spriteBatch.Begin();
            for (int r = 0; r <= grid.GetUpperBound(1); r++)
            {
                for (int c = 0; c <= grid.GetUpperBound(2); c++)
                {
                    if (grid[level, r, c])
                        Game1.spriteBatch.Draw(Game1.mazepath, new Vector2(c * 30, r * 30), Color.Black);
                    else
                        Game1.spriteBatch.Draw(Game1.mazewall, new Vector2(c * 30, r * 30), Color.Blue);
                }
            }
            Game1.spriteBatch.End();
            
        }
    }
}
