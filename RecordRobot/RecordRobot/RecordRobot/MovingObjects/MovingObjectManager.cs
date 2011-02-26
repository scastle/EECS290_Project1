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

            Point p;
            p = Maze.getPointToPlace();
            Objects.Add(new Record(p.X, p.Y, RecordColor.red));
            p = Maze.getPointToPlace();
            Objects.Add(new Record(p.X, p.Y, RecordColor.orange));
            p = Maze.getPointToPlace();
            Objects.Add(new Record(p.X, p.Y, RecordColor.yellow));
            p = Maze.getPointToPlace();
            Objects.Add(new Record(p.X, p.Y, RecordColor.green));
            p = Maze.getPointToPlace();
            Objects.Add(new Record(p.X, p.Y, RecordColor.blue));
            p = Maze.getPointToPlace();
            Objects.Add(new Record(p.X, p.Y, RecordColor.violet));
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
            foreach (Record r in CheckCollisions())
            {
                if (r.color == nextColor)
                {
                    r.ChangeToGrey();
                    nextColor++;
                    if ((int)nextColor > Objects.OfType<Record>().Count() - 1)
                    {
                        GameWin = true;
                        Objects.RemoveAll(item => item is Record);
                    }
                }
                else if (!RobotPlayer.IsInvincible)
                {
                    RobotPlayer.LoseLife();
                    RobotPlayer.SetInvincible(new TimeSpan(0, 0, 3));
                }
            }
        }

        public static IEnumerable<Mover> CheckCollisions()
        {
            Mover[] objs = Objects.ToArray();
            foreach (Mover m in objs)
            {
                if (m is Record)
                {
                    // Calculate the difference in the X and Y positions
                    double xDiff = m.Position.X - RobotPlayer.Position.X;
                    double yDiff = m.Position.Y - RobotPlayer.Position.Y;

                    // Calculate record radius
                    double radius = Game1.RedRecord.Width / 2;

                    // Calculate maximum distance for collision
                    double xColl = RobotPlayer.Texture.Width / 2 + radius / SQRT2;
                    double yColl = RobotPlayer.Texture.Height / 2 + radius / SQRT2;
                    if (Math.Abs(xDiff) < xColl && Math.Abs(yDiff) < yColl)
                    {
                        yield return m;
                    }
                }
            }
        }
    }
}
