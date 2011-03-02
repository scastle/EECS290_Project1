using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.MovingObjects;
using RecordRobot.RRClasses;
using RecordRobot.GameElements;

namespace RecordRobot.Screens
{
    public class WorldScreen : GameScreen
    {

        /// <summary>
        /// Used to check if the game has just started to check to put the title screen on the screen
        /// </summary>
        public static bool Beginning { get; private set; }

        
        public WorldScreen()
            : base()
        {
            Beginning = true;
          
        }

        public override void Draw()
        {
            Controls.SkipLevel();
            //this checking is being handled in movingobjectmanager's update method, I have to look into it to see where it should be.
            if (MovingObjectManager.GameWin)
            {
                if (Maze.level == 4)
                {
                    Maze.level = 0;
                    Settings.NumRecords = 6;
                }
                else
                {
                    Maze.level++;
                }
                Maze.Draw();
                MovingObjectManager.GameWin = false;
                MovingObjectManager.NewLevel = true;

            }
            Maze.Draw();
            MovingObjectManager.Draw();
            InfoBar.Draw();
        }

        public override void Update()
        {
            MovingObjectManager.Update();
            
        }
    }
}
