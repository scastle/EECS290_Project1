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

        /// <summary>
        /// Creates a new member of the Robot class
        /// </summary>
        /// <param name="x">The x position of the robot</param>
        /// <param name="y">The y position of the robot</param>
        public Robot(int x, int y)
        {
            base.Position.X = x;
            base.Position.Y = y;
            this.Speed = 2;
            this.Texture = Game1.Robot;
        }

        public override void Update()
        {
            Direction d = Controls.GetDirection();
            if (d != MovingObjects.Direction.None)
                NextDirection = Controls.GetDirection();
            if ((int)NextDirection + (int)Direction == 0)
            {
                Direction = NextDirection;

                // Change which direction robot is facing
                if (Direction == MovingObjects.Direction.Left)
                {
                    this.Texture = Game1.RobotLeft;
                }
                else if (Direction == MovingObjects.Direction.Right)
                {
                    this.Texture = Game1.RobotRight;
                }
            }
            else if (Maze.IsIntersection(this.Position))
            {
                Direction = NextDirection;
                if (!Maze.CanGo(Position, Direction))
                {
                    Direction = Direction.None;
                }

                // Change which direction robot is facing
                if (Direction == MovingObjects.Direction.Up ||
                    Direction == MovingObjects.Direction.Down || 
                    Direction == MovingObjects.Direction.None)
                {
                    this.Texture = Game1.Robot;
                }
                else if (Direction == MovingObjects.Direction.Left)
                {
                    this.Texture = Game1.RobotLeft;
                }
                else if (Direction == MovingObjects.Direction.Right)
                {
                    this.Texture = Game1.RobotRight;
                }
            }

            // Check for collisions with other moving objects?

            base.UpdatePosition();
        }
    }
}
