﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.RRClasses;
using RecordRobot.GameElements;

namespace RecordRobot.MovingObjects
{

    public class Robot : Mover
    {
        /// <summary>
        /// The direction the robot will go at the next intersection
        /// </summary>
        public Direction NextDirection;

        private DateTime invincibleUntil;
        public bool IsInvincible = false;


        public bool RobotFlashing = false;

        public int Lives;

        /// <summary>
        /// Creates a new member of the Robot class
        /// </summary>
        /// <param name="x">The x position of the robot</param>
        /// <param name="y">The y position of the robot</param>
        public Robot(int x, int y)
        {
            if (Settings.DifficultyLevel != Settings.DifficultySettings.easy)
                Settings.RobotSpeed = 3;
            else
                Settings.RobotSpeed = 2;
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
                //MovingObjectManager.Objects.RemoveAll(item => item is Record);
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
                if (!Maze.CanGo(this.Position, this.NextDirection))
                {
                    if (!Maze.CanGo(this.Position, this.Direction))
                        this.Direction = Direction.None;
                }
                else
                    this.Direction = NextDirection;
            }

            // Check if NextDirection is the opposite of Direction
            else if ((int)NextDirection + (int)this.Direction == 0)
            {
                this.Direction = NextDirection;
            }

            // Change which direction robot is facing
            if (IsInvincible)
                if ((invincibleUntil - DateTime.Now).TotalMilliseconds % 1000 > 500)
                {
                    RobotFlashing = true;
                }
                else
                {
                    RobotFlashing = false;
                }

            if (this.Direction == MovingObjects.Direction.Up ||
                this.Direction == MovingObjects.Direction.Down ||
                this.Direction == MovingObjects.Direction.None)
            {
                if (RobotFlashing)
                    this.Texture = Textures.RobotDead;
                else
                    this.Texture = Textures.Robot;
            }
            else if (this.Direction == MovingObjects.Direction.Left)
            {
                if (RobotFlashing)
                    this.Texture = Textures.RobotFlashingLeft;
                else
                    this.Texture = Textures.RobotLeft;
            }
            else if (this.Direction == MovingObjects.Direction.Right)
            {
                if (RobotFlashing)
                    this.Texture = Textures.RobotFlashingRight;
                else
                    this.Texture = Textures.RobotRight;
            }

            if (MovingObjectManager.GameOver)
            {
                this.Texture = Textures.RobotDead;
                Game1.GameOver();
            }
            if (MovingObjectManager.GameWin)
                this.Texture = Textures.RobotWin;

            base.UpdatePosition();
        }

    }
}
