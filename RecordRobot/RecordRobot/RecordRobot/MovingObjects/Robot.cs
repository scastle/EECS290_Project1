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
            this.Texture = Textures.Robot;
        }

        public void LoseLife()
        {
            Lives--;
            if (Lives <= 0)
            {
                MovingObjectManager.GameOver = true;
                MovingObjectManager.Objects.RemoveAll(item => item is Record);
            }
        }

        public void SetInvincible(TimeSpan time)
        {
            invincibleUntil = DateTime.Now + time;
            IsInvincible = true;
            Texture = Textures.RobotInvincible;
        }

        public override void Update()
        {
            if (IsInvincible && DateTime.Now > invincibleUntil)
            {
                Texture = Textures.Robot;
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
                    this.Texture = Textures.Robot;
                }
                else if (this.Direction == MovingObjects.Direction.Left)
                {
                    this.Texture = Textures.RobotLeft;
                }
                else if (this.Direction == MovingObjects.Direction.Right)
                {
                    this.Texture = Textures.RobotRight;
                }
            }
            else
            {
                if ((invincibleUntil - DateTime.Now).TotalMilliseconds % 1000 > 500)
                {
                    Texture = Textures.RobotDead;
                }
                else
                {
                    Texture = Textures.Robot;
                }
            }
            
            if (MovingObjectManager.GameOver)
                this.Texture = Textures.RobotDead;
            if (MovingObjectManager.GameWin)
                this.Texture = Textures.RobotWin;

            base.UpdatePosition();
        }

    }
}
