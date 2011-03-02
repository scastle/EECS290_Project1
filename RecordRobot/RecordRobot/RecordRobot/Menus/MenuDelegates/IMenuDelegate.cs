using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.Menus.MenuDelegates
{
    /// <summary>
    /// The base interface for creating Menu Delegate
    /// public classes. Note that these are not "real" delegates,
    /// as using this technique is more memory efficient.
    /// Call the Run method of an instance to run the delegated
    /// action.
    /// </summary>
    public interface IMenuDelegate
    {
        /// <summary>
        /// Runs this instance, performing the action once,
        /// and thereafter exiting the method.
        /// </summary>
        void Run();
    }
}
