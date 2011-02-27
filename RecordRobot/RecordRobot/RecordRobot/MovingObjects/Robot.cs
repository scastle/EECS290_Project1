using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public override void Update()
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

            base.UpdatePosition();
        }

        public override void Draw()
        {
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(Game1.Robot, new Vector2(Position.X, Position.Y), Color.White);
            Game1.spriteBatch.End();
        }
    }
}
