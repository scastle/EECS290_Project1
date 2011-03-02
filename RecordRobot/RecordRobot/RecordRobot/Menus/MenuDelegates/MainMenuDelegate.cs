using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RecordRobot.Menus.MenuDelegates
{
    /// <summary>
    /// A delegate used for returning to the main menu.
    /// </summary>
    public class MainMenuDelegate : IMenuDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuDeleage"/> class.
        /// </summary>
        public MainMenuDelegate()
            : base()
        {
        }

        /// <summary>
        /// Runs this instance, performing the action once,
        /// and thereafter exiting the method.
        /// </summary>
        public void Run()
        {
            for (int i = Game1.screens.Count - 1; i >= 0; i--)
            {
                Game1.screens[i].Disposed = true;
            }
            Game1.ToTitle();

        }
    }
}
