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

        /// <summary>
        /// The current direction of the robot
        /// </summary>
        public Direction CurrentDirection;

        //public int Score;
        //public int Lives;

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
            Maze.TargetColor = Maze.CollisionType.redrecord;

            Maze.Lives = 3;
        }

        public override void Update()
        {
            Maze.UpdatePosition(this.Position, Maze.CollisionType.path);
            Direction d = Controls.GetDirection();
            if (d != MovingObjects.Direction.None)
                NextDirection = Controls.GetDirection();
            if ((int)NextDirection + (int)this.Direction == 0)
            {
                this.Direction = NextDirection;

                // Change which direction robot is facing
                if (this.Direction == MovingObjects.Direction.Left)
                {
                    this.Texture = Game1.RobotLeft;
                }
                else if (this.Direction == MovingObjects.Direction.Right)
                {
                    this.Texture = Game1.RobotRight;
                }
            }

            else if (Maze.IsIntersection(this.Position))
            {
                this.Direction = NextDirection;
                if (!Maze.CanGo(this.Position, this.Direction))
                {
                    this.Direction = Direction.None;
                }

                // Change which direction robot is facing
                if (this.Direction == MovingObjects.Direction.Up ||
                    this.Direction == MovingObjects.Direction.Down ||
                    this.Direction == MovingObjects.Direction.None)
                {
                    this.Texture = Game1.Robot;
                }
                else if (this.Direction == MovingObjects.Direction.Left)
                {
                    this.Texture = Game1.RobotLeft;
                }
                else if (this.Direction == MovingObjects.Direction.Right)
                {
                    this.Texture = Game1.RobotRight;
                }
            }


            this.CurrentDirection = this.Direction;
            // Check for collisions with other moving objects? MOVED TO RECORD BECAUSE IT WILL BE EASIER

            //update robot position in collision grid
            Maze.UpdatePosition(this.Position, Maze.CollisionType.robot);
            if (MovingObjectManager.GameOver)
                this.Texture = Game1.RobotDead;
            if (MovingObjectManager.GameWin)
                this.Texture = Game1.RobotWin;

            this.BufferTexture = this.Texture;

            if (Maze.RobotFlashing)
                this.Texture = Game1.RobotDead;
            else
                this.Texture = this.BufferTexture;

            base.UpdatePosition();
        }

    }
}
