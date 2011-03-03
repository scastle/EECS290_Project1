using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RecordRobot.GameElements
{
    class Settings
    {
        public enum DifficultySettings
        {
            easy = 0, medium = 1, hard = 2
        }

        public static int RobotSpeed = 3;
        public static int RecordSpeed = 2;
        public static int SecondsInvincible = 2;
        public static int NumRecords = 1;
        public static Point RobotStartingPosition = new Point(45, 45);
        public static int Lives = 10;
        public static DifficultySettings DifficultyLevel = DifficultySettings.easy;
    }
}
