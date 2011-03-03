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
            Game1.screens.IsPaused = false;
            Game1.screens.IsTitle = false;
            AudioPlayer.Play(0, 1);
        }

        public override void Draw()
        {


            Maze.Draw();
            MovingObjectManager.Draw();
            InfoBar.Draw();
        }

        public override void Update()
        {
            MovingObjectManager.Update();
            Controls.SkipLevel();   // pressing "p" allows skipping a level
        }
    }
}
