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

        public int score;

        public int lives;

        public void Update()
        {
            //update control... find what direction?

            if (NextDirection.Equals(CurrentDirection))
            {
                //move
                //check collisions with walls/objects
            }
            else
            {
                //hmm not sure how to do this- check for ability to change direction.
                // ^ this might go in mover class??????? as would moving around corner?

                //move up to the next intersection (possibly around corner) and change direction. 
                //else just continue moving in current direction
                //check collisions with walls/objects
            }

        }
    }
}
