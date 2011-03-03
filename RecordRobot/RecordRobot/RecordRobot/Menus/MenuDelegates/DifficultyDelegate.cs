using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.GameElements;

namespace RecordRobot.Menus.MenuDelegates
{
    class DifficultyDelegate : IMenuDelegate
    {
        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;

        /// <summary>
        /// This is the duration of the screen
        /// </summary>
        private long duration;

        private string DifSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="DifficultyDeleage"/> class.
        /// </summary>
        public DifficultyDelegate()
            : base()
        {
            this.initialTime = DateTime.Now.Ticks;
            this.duration = 3000000;
        }

        /// <summary>
        /// Runs this instance, performing the action once,
        /// and thereafter exiting the method.
        /// </summary>
        public void Run()
        {
            

            if (DateTime.Now.Ticks > initialTime + duration)
            {
                if (Controls.Enter())
                {
                    if (Settings.DifficultyLevel != Settings.DifficultySettings.hard)
                        Settings.DifficultyLevel++;
                    else
                        Settings.DifficultyLevel = Settings.DifficultySettings.easy;
                }
                this.initialTime = DateTime.Now.Ticks;
            }
            switch ((int)Settings.DifficultyLevel)
            {
                case 0:
                    DifSet = "Easy";
                    break;
                case 1:
                    DifSet = "Medium";
                    break;
                case 2:
                    DifSet = "Hard";
                    break;
            }
            Game1.SettingsScreenMenu.menu.difficulty.text = "Difficulty: " + DifSet;
        }
    }
}
