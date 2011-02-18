using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.MovingObjects
{
    public enum Direction 
    {
        Up, Down, Left, Right
    }

    class Robot : Mover
    {
        /// <summary>
        /// The current direction the robot is moving
        /// </summary>
        public Direction CurrentDirection;

        /// <summary>
        /// The direction the robot will go at the next intersection
        /// </summary>
        public Direction NextDirection;
    }
}
