using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.RRClasses;
using RecordRobot.GameElements;
using RecordRobot.Screens;

namespace RecordRobot.MovingObjects
{
    public class MovingObjectManager
    {
        /// <summary>
        /// All moving objects to be updated and drawn are placed in this list.
        /// </summary>
        public static List<Mover> Objects;

        public static bool GameOver;

        const double SQRT2 = 1.414;

        public static RecordColor nextColor;

        public static bool NewLevel = true;

        public static bool GameWin;

        public static Robot RobotPlayer { get; set; }

        /// <summary>
        /// Adds all initial moving objects to the Objects list
        /// </summary>
        private static void InitializeObjects()
        {
            NewLevel = false;

            Objects = new List<Mover>();

            if (RobotPlayer == null)
                RobotPlayer = new Robot(45, 45);
            else
                RobotPlayer.Position = Settings.RobotStartingPosition;
            Objects.Add(RobotPlayer);


            for (int i = 0; i < Game1.CurrentLevel.NumRecords; i++)
            {
                Point p = Maze.getPointToPlace();
                Objects.Add(new Record(p.X, p.Y, (RecordColor)i));
            }

            nextColor = RecordColor.red;
        }

        /// <summary>
        /// Draws all moving objects
        /// </summary>
        public static void Draw()
        {
            foreach (Mover m in Objects)
            {
                m.Draw();
            }
        }


        /// <summary>
        /// Updates the positions and statuses of all moving objects
        /// </summary>
        public static void Update()
        {
            if (Objects == null || NewLevel)
            {
                InitializeObjects();
            }
            foreach (Mover m in Objects)
            {
                m.Update();
            }
            foreach (Mover m in CheckCollisions())
            {
                if (m is Record)
                {
                    Record r = m as Record;
                    if (r.Color == nextColor)
                    {
                        r.ChangeToGrey();
                        nextColor++;
                        ScoreManager.CurrentScore += r.Value;
                        if ((int)nextColor > Game1.CurrentLevel.NumRecords - 1)
                        {
                            Objects.RemoveAll(item => item is Record);
                            //GameWin = true;
                            if (Maze.level == 4)
                            {
                                Maze.level = 0;
                                Game1.CurrentLevel.NumRecords = 6;
                            }
                            else
                            {
                                Maze.level++;
                            }
                            Game1.CurrentLevel.LevelNumber++;
                            //display a screen between levels
                            Game1.screens.Play(new PreLevelScreen());

                            //put in settings for harder level groups here.
                            Maze.Draw();
                            InitializeObjects();
                            nextColor = RecordColor.red;

                            
                        }
                    }
                    else if (!RobotPlayer.IsInvincible && r.CanDamage)
                    {
                        RobotPlayer.LoseLife();
                        RobotPlayer.SetInvincible(new TimeSpan(0, 0, Settings.SecondsInvincible));
                    }
                }
            }
        }

        public static IEnumerable<Mover> CheckCollisions()
        {
            Mover[] objs = Objects.ToArray();
            foreach (Mover m in objs)
            {
                if (!(m is Robot))
                {
                    // Calculate the difference in the X and Y positions
                    double xDiff = m.Position.X - RobotPlayer.Position.X;
                    double yDiff = m.Position.Y - RobotPlayer.Position.Y;

                    // Calculate record radius
                    double radius = Textures.RedRecord.Width / 2;

                    // Calculate maximum distance for collision
                    double xColl = RobotPlayer.Texture.Width / 2 + radius / SQRT2 - 7;
                    double yColl = RobotPlayer.Texture.Height / 2 + radius / SQRT2 - 7;
                    if (Math.Abs(xDiff) < xColl && Math.Abs(yDiff) < yColl)
                    {
                        yield return m;
                    }
                }
            }
        }

        public static void SetRelativeDirection(RecordColor color)
        {
            Mover[] objs = Objects.ToArray();
            int r = Game1.rand.Next(2);
            foreach (Mover m in objs)
            {
                if (!(m is Robot))
                {
                    Record rec = m as Record;
                    if (rec.Color == RecordColor.grey || rec.Color == nextColor)
                    {

                        // Calculate the difference in the X and Y positions
                        double xDiff = RobotPlayer.Position.X - m.Position.X;
                        double yDiff = RobotPlayer.Position.Y - m.Position.Y;
                        if (xDiff >= 0 && yDiff >= 0)
                        {
                            if (r == 1)
                            {
                                m.AIChoice1 = Direction.Down;
                                m.AIChoice2 = Direction.Right;
                            }
                            else
                            {
                                m.AIChoice1 = Direction.Right;
                                m.AIChoice2 = Direction.Down;
                            }
                        }
                        else if (xDiff >= 0 && yDiff <= 0)
                        {
                            if (r == 1)
                            {
                                m.AIChoice1 = Direction.Up;
                                m.AIChoice2 = Direction.Right;
                            }
                            else
                            {
                                m.AIChoice1 = Direction.Right;
                                m.AIChoice2 = Direction.Up;
                            }
                        }
                        else if (xDiff <= 0 && yDiff <= 0)
                        {
                            if (r == 1)
                            {
                                m.AIChoice1 = Direction.Up;
                                m.AIChoice2 = Direction.Left;
                            }
                            else
                            {
                                m.AIChoice1 = Direction.Left;
                                m.AIChoice2 = Direction.Up;
                            }
                        }
                        else if (xDiff <= 0 && yDiff >= 0)
                        {
                            if (r == 1)
                            {
                                m.AIChoice1 = Direction.Down;
                                m.AIChoice2 = Direction.Left;
                            }
                            else
                            {
                                m.AIChoice1 = Direction.Left;
                                m.AIChoice2 = Direction.Down;
                            }
                        }
                    }
                }
            }
        }

        public static void AIChangeDirection(RecordColor c)
        {
            Mover[] objs = Objects.ToArray();
            foreach (Mover m in objs)
            {
                if (!(m is Robot))
                {
                    Record rec = m as Record;
                    if (rec.Color == RecordColor.grey)
                    {
                        // Calculate the difference in the X and Y positions
                        double xDiff = RobotPlayer.Position.X - m.Position.X;
                        double yDiff = RobotPlayer.Position.Y - m.Position.Y;

                        if (rec.Direction == Direction.Down && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (yDiff <= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                        else if (rec.Direction == Direction.Up && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (yDiff >= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                        if (rec.Direction == Direction.Right && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (xDiff <= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                        else if (rec.Direction == Direction.Left && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (xDiff >= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                    }
                    else if (rec.Color == MovingObjectManager.nextColor)
                    {
                        // Calculate the difference in the X and Y positions
                        double xDiff = RobotPlayer.Position.X - m.Position.X;
                        double yDiff = RobotPlayer.Position.Y - m.Position.Y;

                        if (rec.Direction == Direction.Down && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (yDiff >= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                        else if (rec.Direction == Direction.Up && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (yDiff <= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                        if (rec.Direction == Direction.Right && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (xDiff >= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                        else if (rec.Direction == Direction.Left && Maze.CanGo(rec.Position, (Direction)((int)rec.Direction * -1)))
                        {
                            if (xDiff <= 0)
                                rec.Direction = (Direction)((int)rec.Direction * -1);
                        }
                    }
                }
            }
        }


        public static Point GetRelativePosition(RecordColor c)
        {
            Mover[] objs = Objects.ToArray();
            int r = Game1.rand.Next(2);
            foreach (Mover m in objs)
            {
                if (!(m is Robot))
                {
                    Record rec = m as Record;
                    if(rec.Color == c)
                        return new Point(Math.Abs(RobotPlayer.Position.X - m.Position.X),
                            Math.Abs(RobotPlayer.Position.Y - m.Position.Y));
                }
            }
            return new Point(-9999, -9999);
            
        }
    }
}
