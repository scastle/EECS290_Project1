using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.Menus.MenuDelegates
{
    /// <summary>
    /// This pops the most recent screen off of the screen
    /// stack of GameWorld.
    /// </summary>
    public class QuitTopDelegate : IMenuDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuitTopDelegate"/> class.
        /// </summary>
        public QuitTopDelegate()
            : base()
        {
        }

        /// <summary>
        /// Runs this instance. This pops the most recent screen off of the screen
        /// stack of GameWorld.
        /// </summary>
        public void Run()
        {
            if (Game1.screens.Count > 0)
            {
                Game1.screens[Game1.screens.Count - 1].Disposed = true;
            }
        }
    }                
}
