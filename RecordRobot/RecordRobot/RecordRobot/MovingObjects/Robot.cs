using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.MovingObjects
{
    public enum Direction 
    {
        Up, Down, Left, Right, None
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

        public int Score;

        public int Lives;

        public void Update()
        {
            NextDirection = Controls.GetDirection();

            // Check intersection
            if (Maze.IsIntersection(this.Location))
            {
                CurrentDirection = NextDirection;
                if (!Maze.CanGo(CurrentDirection))
                {
                    CurrentDirection = Direction.None;
                }
            }
            /*
            if (NextDirection.Equals(CurrentDirection))
            {
                //check collisions with walls/objects
                //move
            }
            else
            {
                //hmm not sure how to do this- check for ability to change direction.
                // ^ this might go in mover class??????? as would moving around corner?

                //move up to the next intersection (possibly around corner) and change direction. 
                //else just continue moving in current direction
                //check collisions with walls/objects
            }
            */
            UpdateLocation();

        }

        private void UpdateLocation()
        {
            switch (this.CurrentDirection)
            {
                case Direction.Up: this.Location.Y--; break;
                case Direction.Down: this.Location.Y++; break;
                case Direction.Left: this.Location.X--; break;
                case Direction.Right: this.Location.X++; break;
            }
        }
    }
}
