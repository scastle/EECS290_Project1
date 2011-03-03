using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.GameElements;
using RecordRobot.Screens;

namespace RecordRobot.Menus.MenuDelegates
{
    class NumRecordsDelegate : IMenuDelegate
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

        /// <summary>
        /// Initializes a new instance of the <see cref="NumRecordsDeleage"/> class.
        /// </summary>
        public NumRecordsDelegate()
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
                    if (Settings.NumRecords < 6)
                        Settings.NumRecords++;
                    else
                        Settings.NumRecords = 1;
                }
                this.initialTime = DateTime.Now.Ticks;
            }
            Game1.SettingsScreenMenu.menu.numRecords.text = "Number of Records: " + Settings.NumRecords;
        }
    }
}
