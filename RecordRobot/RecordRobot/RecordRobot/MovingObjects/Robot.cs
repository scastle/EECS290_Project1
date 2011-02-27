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

        private DateTime invincibleUntil;
        public bool IsInvincible = false;

        //public int Score;
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
            this.Speed = Settings.RobotSpeed;
            this.Lives = Settings.Lives;
            this.Texture = Game1.Robot;
        }

        public void LoseLife()
        {
            Lives--;
            if (Lives <= 0)
            {
                MovingObjectManager.GameOver = true;
            }
        }

        public void SetInvincible(TimeSpan time)
        {
            invincibleUntil = DateTime.Now + time;
            IsInvincible = true;
            Texture = Game1.RobotInvincible;
        }

        public override void Update()
        {
            if (IsInvincible && DateTime.Now > invincibleUntil)
            {
                Texture = Game1.Robot;
                IsInvincible = false;
            }
            Direction d = Controls.GetDirection();
            if (d != MovingObjects.Direction.None)
                NextDirection = Controls.GetDirection();

            // Check if robot can 
            if (Maze.IsIntersection(this.Position))
            {
                this.Direction = NextDirection;
                if (!Maze.CanGo(this.Position, this.Direction))
                {
                    this.Direction = Direction.None;
                }
            }

            // Check if NextDirection is the opposite of Direction
            else if ((int)NextDirection + (int)this.Direction == 0)
            {
                this.Direction = NextDirection;
            }

            // Change which direction robot is facing
            if (!IsInvincible)
            {
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
            
            if (MovingObjectManager.GameOver)
                this.Texture = Game1.RobotDead;
            if (MovingObjectManager.GameWin)
                this.Texture = Game1.RobotWin;

            base.UpdatePosition();
        }

    }
}
