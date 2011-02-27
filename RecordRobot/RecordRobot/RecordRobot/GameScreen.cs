using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.MovingObjects;

namespace RecordRobot
{
    class GameScreen
    {

       
        public static void Draw()
        {
            if (MovingObjectManager.GameWin)
            {
                Maze.level++;
                Maze.Draw();
                MovingObjectManager.GameWin = false;

            }
            Maze.Draw();
            MovingObjectManager.Draw();
            
        }
    }
}
