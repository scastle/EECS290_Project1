using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RecordRobot.MovingObjects
{
    public enum Direction
    {
        Up, Down, Left, Right, None
    }

    class Mover
    {
        /// <summary>
        /// The speed of the moving object.
        /// </summary>
        public int Speed = 1;

        /// <summary>
        /// The current location of the object.
        /// </summary>
        public Point Position;

        /// <summary>
        /// The current direction the robot is moving.
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// Updates the objects location based on the current direction.
        /// </summary>
        public void UpdatePosition()
        {
            switch (this.Direction)
            {
                case Direction.Up: this.Position.Y -= Speed; break;
                case Direction.Down: this.Position.Y += Speed; break;
                case Direction.Left: this.Position.X -= Speed; break;
                case Direction.Right: this.Position.X += Speed; break;
            }
        }
    }
}
