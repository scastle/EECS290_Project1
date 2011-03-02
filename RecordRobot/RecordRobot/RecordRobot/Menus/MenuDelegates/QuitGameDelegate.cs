using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.Menus.MenuDelegates
{
        /// <summary>
        /// A delegate used for exiting the game.
        /// </summary>
        public class QuitGameDelegate : IMenuDelegate
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="QuitGameDeleage"/> class.
            /// </summary>
            public QuitGameDelegate()
                : base()
            {
            }

            /// <summary>
            /// Runs this instance, performing the action once,
            /// and thereafter exiting the method.
            /// </summary>
            public void Run()
            {
                Game1.ExitGame();
            }
        }
    
}
