using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RecordRobot.MovingObjects
{
    class Maze
    {
        public bool[][] grid;

        /// <summary>
        /// Checks if the specified point is an intersection
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <returns>true if the point is an intersection</returns>
        public static bool IsIntersection(Point p)
        {
            int r = p.Y % 30;
            int c = p.X % 30;

            if( grid[r-1][c] || grid[r+1][c])
            {

            }

            // add logic here
            return true;
        }

        /// <summary>
        /// Checks if the robot can move in the current direction
        /// </summary>
        /// <param name="d">The direction to move</param>
        /// <returns>true if the robot can move in the direction</returns>
        public static bool CanGo(Point p, Direction d)
        {
            // add logic here
            return true;
        }
    }
}
