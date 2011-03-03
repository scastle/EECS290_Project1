using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.GameElements;

namespace RecordRobot.Menus.MenuDelegates
{
    class DifficultyDelegate : IMenuDelegate
    {/// <summary>
        /// Initializes a new instance of the <see cref="DifficultyDeleage"/> class.
        /// </summary>
        public DifficultyDelegate()
            : base()
        {
        }

        /// <summary>
        /// Runs this instance, performing the action once,
        /// and thereafter exiting the method.
        /// </summary>
        public void Run()
        {
            if (Settings.DifficultyLevel != Settings.DifficultySettings.hard)
                Settings.DifficultyLevel++;
            else
                Settings.DifficultyLevel = Settings.DifficultySettings.easy;

            for (int i = Game1.screens.Count - 1; i >= 0; i--)
            {
                Game1.screens[i].Disposed = true;
            }
            Game1.toSettings();
        }
    }
}
