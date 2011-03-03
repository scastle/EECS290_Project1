using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.MovingObjects;

namespace RecordRobot.RRClasses
{
    public class ScoreManager
    {
        public static int CurrentScore;

        public static int GetScore(RecordColor c)
        {
            if (c == RecordColor.grey)
                return 0;
            else
                return (((int)c + 2) * (Game1.CurrentLevel.LevelNumber + 1)) * 5 / 7;
        }
    }
}
