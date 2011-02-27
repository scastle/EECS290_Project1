using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.MovingObjects;

namespace RecordRobot
{
    class Maze
    {
        public static bool[][] grid; //true for paths, false for walls

        /// <summary>
        /// Checks if the specified point is an intersection
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <returns>true if the point is an intersection</returns>
        public static bool IsIntersection(Point p)
        {
            int r = p.Y / 30;
            int c = p.X / 30;

            if ((grid[r - 1][c] || grid[r + 1][c]) && (grid[r][c - 1] || grid[r][c + 1]))
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
            int r = p.Y / 30;
            int c = p.X / 30;
            int xsquare = p.X % 30;
            int ysquare = p.Y % 30;

            switch (d)
            {
                case Direction.Up:
                    if (grid[r - 1][c] || ysquare > 15)
                        return true;
                    break;
                case Direction.Down:
                    if (grid[r + 1][c] || ysquare < 15)
                        return true;
                    break;
                case Direction.Left:
                    if (grid[r][c - 1] || xsquare > 15)
                        return true;
                    break;
                case Direction.Right:
                    if (grid[r][c + 1] || xsquare < 15)
                        return true;
                    break;
            }
            return false;
        }

        public static void Draw()
        {

        }
    }
}
