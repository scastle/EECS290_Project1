using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.Menus.MenuDelegates
{
    public class StartDelegate : IMenuDelegate
    {
        public StartDelegate()
            : base()
        {

        }

        public void Run()
        {
            if (Game1.screens.Count > 0)
            {
                Game1.screens[Game1.screens.Count - 1].Disposed = true;
            }
            Game1.StartGame();
        }

    }
}
