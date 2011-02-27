using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RecordRobot.MovingObjects
{
    class MovingObjectManager
    {
        /// <summary>
        /// All moving objects to be updated and drawn are placed in this list.
        /// </summary>
        public static List<Mover> Objects;

        public static bool GameOver;

        const double SQRT2 = 1.414;

        static RecordColor nextColor = RecordColor.red;

        public static bool GameWin;

        public static Robot RobotPlayer { get; private set; }

        /// <summary>
        /// Adds all initial moving objects to the Objects list
        /// </summary>
        private static void InitializeObjects()
        {
            Objects = new List<Mover>();

            RobotPlayer = new Robot(45, 45);
            Objects.Add(RobotPlayer);


            for (int i = 0; i < Game1.CurrentLevel.NumRecords; i++)
            {
                Point p = Maze.getPointToPlace();
                Objects.Add(new Record(p.X, p.Y, (RecordColor)i));
            }
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
            if (Objects == null)
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
                        if ((int)nextColor > Game1.CurrentLevel.NumRecords - 1)
                        {
                            Objects.RemoveAll(item => item is Record);
                            //GameWin = true;
                            Maze.level++;
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
    }
}
