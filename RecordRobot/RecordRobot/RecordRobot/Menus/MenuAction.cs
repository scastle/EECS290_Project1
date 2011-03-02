using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.Menus.MenuDelegates;
namespace RecordRobot.Menus
{
    public class MenuAction
    {

        /// <summary>
        /// This is the delegate holding the action or set of actions to be performed.
        /// </summary>
        private IMenuDelegate menuDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAction"/> class.
        /// </summary>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="menuDelegate">The menu delegate.</param>
        public MenuAction(IMenuDelegate menuDelegate)
        {
            this.menuDelegate = menuDelegate;
        }

        /// <summary>
        /// Tries to run the delegate. This should be called every cycle, as
        /// this will check the controller to see if the corresponding action
        /// has been performed. If so, and if the delegate is not null, then
        /// the delegated action is performed.
        /// </summary>
        public void TryRunDelegate()
        {
            if (this.menuDelegate != null)
            {
                if (Controls.Enter())
                {
                    this.menuDelegate.Run();
                }
            }
        }
    }
}
