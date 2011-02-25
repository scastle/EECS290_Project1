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

        public static bool GameWin;

        public static Robot RobotPlayer { get; private set; }

        public static void Draw()
        {
            if (Objects == null)
            {
                //Objects = new List<Mover>();
                //RobotPlayer = new Robot(45, 45);
                //Objects.Add(RobotPlayer);
                //Objects.Add(new Record(345, 45, RecordColor.red));
                //Objects.Add(new Record(405, 45, RecordColor.yellow));
                //Objects.Add(new Record(375, 45, RecordColor.green));
                //Objects.Add(new Record(405, 45, RecordColor.blue));
                //Objects.Add(new Record(345, 75, RecordColor.violet));
            }
            if(!GameOver && !GameWin)
                foreach (Mover m in Objects)
                {
                    m.Draw();
                }
            else
                RobotPlayer.Draw();
        }

        public static void Update()
        {
            if (Objects == null)
            {
                Objects = new List<Mover>();
                RobotPlayer = new Robot(45, 45);


                Objects.Add(RobotPlayer);
                Point p;
                p = Maze.getPointToPlace();
                Objects.Add(new Record(p.X, p.Y, RecordColor.red));
                p = Maze.getPointToPlace();
                Objects.Add(new Record(p.X, p.Y, RecordColor.yellow));
                p = Maze.getPointToPlace();
                Objects.Add(new Record(p.X, p.Y, RecordColor.green));
                //Objects.Add(new Record(405, 45, RecordColor.blue));
                //Objects.Add(new Record(345, 75, RecordColor.violet));
                
            }
            if (!GameOver && !GameWin)
                foreach (Mover m in Objects)
                {
                    m.Update();
                }
            else
                RobotPlayer.Update();
        }

    }
}
