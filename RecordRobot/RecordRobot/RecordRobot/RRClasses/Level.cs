using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.GameElements;

namespace RecordRobot.RRClasses
{
    public class Level
    {
        public int LevelNumber;
        public int NumRecords = Settings.NumRecords;
        public bool LevelWon;
        public bool GameOver;
    }
}
