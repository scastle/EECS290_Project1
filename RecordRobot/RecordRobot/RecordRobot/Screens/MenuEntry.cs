using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RecordRobot.Screens
{
    public class MenuEntry
    {
        /// <summary>
        /// Gets the text to display for this entry.
        /// </summary>
        /// <value>The text to display.</value>
        public string text { get; private set; }

        /// <summary>
        /// Gets or sets the upper menu, which is the menu entry that will
        /// be highlighted if this menu entry is highlighted and the player
        /// presses Up. If this is null, then no action will be taken.
        /// </summary>
        /// <value>The upper menu.</value>
        public MenuEntry UpperMenu { get; set; }

        /// <summary>
        /// Gets or sets the lower menu, which is the menu entry that will
        /// be highlighted if this menu entry is highlighted and the player
        /// presses Down. If this is null, then no action will be taken.
        /// </summary>
        /// <value>The lower menu.</value>
        public MenuEntry LowerMenu { get; set; }

        /// <summary>
        /// Gets or sets the right menu, which is the menu entry that will
        /// be highlighted if this menu entry is highlighted and the player
        /// presses Right. If this is null, then no action will be taken.
        /// </summary>
        /// <value>The right menu.</value>
        public MenuEntry RightMenu { get; set; }

        /// <summary>
        /// Gets or sets the left menu, which is the menu entry that will
        /// be highlighted if this menu entry is highlighted and the player
        /// presses Left. If this is null, then no action will be taken.
        /// </summary>
        /// <value>The left menu.</value>
        public MenuEntry LeftMenu { get; set; }

        /// <summary>
        /// Gets the position of the menu entry.
        /// </summary>
        /// <value>The position.</value>
        public Point position { get; private set; }

        /// <summary>
        /// Gets the actions associated with this menu entry. For example,
        /// one action might be pressing "A" to select the entry. Adding
        /// an action of Left, Right, Up, or Down will overwrite the 
        /// menu entry navigation.
        /// </summary>
        /// <value>The actions.</value>
        public MenuAction[] actions { get; internal set; }
    }
}
