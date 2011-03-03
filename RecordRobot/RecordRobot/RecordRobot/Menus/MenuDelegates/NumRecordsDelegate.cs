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
        /// Initializes a new instance of the <see cref="NumRecordsDeleage"/> class.
        /// </summary>
        public NumRecordsDelegate()
            : base()
        {
        }

        /// <summary>
        /// Runs this instance, performing the action once,
        /// and thereafter exiting the method.
        /// </summary>
        public void Run()
        {
            if (Settings.NumRecords < 6)
                Settings.NumRecords++;
            else
                Settings.NumRecords = 1;

            //for (int i = Game1.screens.Count - 1; i >= 0; i--)
            //{
            //    Game1.screens[i].Disposed = true;
            //}
            //Game1.toSettings();

            Game1.SettingsScreenMenu.menu.numRecords.text = "Number of Records: " + Settings.NumRecords;
        }
    }
}
