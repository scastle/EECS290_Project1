using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.Menus.MenuDelegates
{
    class HowToPlayDelegate : IMenuDelegate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HowToPlayDeleage"/> class.
        /// </summary>
        public HowToPlayDelegate()
            : base()
        {
        }

        /// <summary>
        /// Runs this instance, performing the action once,
        /// and thereafter exiting the method.
        /// </summary>
        public void Run()
        {

            Game1.screens.Play(new Screens.HowToPlayScreen());

        }
    }
}
