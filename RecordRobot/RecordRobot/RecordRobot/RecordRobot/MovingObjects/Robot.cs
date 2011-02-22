using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.MovingObjects
{

    class Robot : Mover
    {
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
            if (Maze.IsIntersection(this.Position))
            {
                Direction = NextDirection;
                if (!Maze.CanGo(Position, Direction))
                {
                    Direction = Direction.None;
                }
            }

            // Check for collisions with other moving objects?

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

            this.UpdatePosition();
        }
    }
}
