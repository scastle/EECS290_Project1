using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.MovingObjects;

namespace RecordRobot
{
    public class GameScreen
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameScreen"/> is disposed.
        /// If this is set to true, the Screen Stack will automatically take it out of the stack.
        /// Just set this value to true and do nothing else if you want to delete the screen.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        public bool Disposed { get; set; }

        /// <summary>
        /// Used to check if the game has just started to check to put the title screen on the screen
        /// </summary>
        public static bool Beginning { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [fading out].
        /// If a screen is fading out, the screen under it on the stack 
        /// will get updated. Otherwise, the screen under it on the stack
        /// will not get updated.
        /// </summary>
        /// <value><c>true</c> if [fading out]; otherwise, <c>false</c>.</value>
        public bool FadingOut { get; private set; }

        public GameScreen()
        {
            Beginning = true;
            this.Disposed = false;
            this.FadingOut = false;
        }

        public void Draw()
        {
            if (MovingObjectManager.GameWin)
            {
                Maze.level++;
                Maze.Draw();
                MovingObjectManager.GameWin = false;
                MovingObjectManager.NewLevel = true;

            }
            Maze.Draw();
            MovingObjectManager.Draw();
            InfoBar.Draw();
        }

        public void Update()
        {

        }
    }
}
