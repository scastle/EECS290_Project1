using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.MovingObjects;

namespace RecordRobot.RRClasses
{
    public class Maze
    {
        public static bool[,,] grid; //true for paths, false for walls

        public static int level;

        /// <summary>
        /// Loads all levels into 3d matrix at beginning of the game.
        /// </summary>
        /// <param name="p">the 3d array that has been read from a text file</param>
        public static void LoadMaze(int[,,] map)
        {
            level = 0;
            int levels = map.GetUpperBound(0) + 1;
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



        /// <summary>
        /// Checks if the specified point is an intersection
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <returns>true if the point is an intersection</returns>
        public static bool IsIntersection(Point p)
        {/////////////////////
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

        public static bool IsDeadEnd(Point p)
        {
            int r = p.Y / 30;
            int c = p.X / 30;

            if (((grid[level, r - 1, c]) && !(grid[level, r + 1, c] || grid[level, r, c - 1] || grid[level, r, c + 1])) ||
                ((grid[level, r + 1, c]) && !(grid[level, r - 1, c] || grid[level, r, c - 1] || grid[level, r, c + 1])) ||
                ((grid[level, r, c + 1]) && !(grid[level, r + 1, c] || grid[level, r, c - 1] || grid[level, r - 1, c])) ||
                ((grid[level, r, c - 1]) && !(grid[level, r + 1, c] || grid[level, r - 1, c] || grid[level, r, c + 1]))
                && ((p.X - 15) % 30 == 0 && (p.Y - 15) % 30 == 0))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if the robot or record can move in the current direction
        /// </summary>
        /// <param name="d">the Location, and the direction to move</param>
        /// <returns>true if the robot can move in the direction</returns>
        public static bool CanGo(Point p, Direction d)
        {
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

                if (grid[level, r, c] && (r > grid.GetUpperBound(1) / 2 || c > grid.GetUpperBound(2) / 2))
                {
                    return (new Point(c * 30 + 15, r * 30 + 15));
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
                        Game1.spriteBatch.Draw(Textures.mazepath, new Vector2(c * 30, r * 30), Color.Black);
                    else
                        Game1.spriteBatch.Draw(Textures.mazewall, new Vector2(c * 30, r * 30), Color.Blue);
                }
            }
            Game1.spriteBatch.End();
            
        }
    }
}
