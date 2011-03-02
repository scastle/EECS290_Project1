using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.Menus.MenuDelegates;

namespace RecordRobot.Menus
{
    public class MenuEntry
    {
        /// <summary>
        /// This is the delegate holding the action or set of actions to be performed.
        /// </summary>
        private IMenuDelegate menuDelegate;

        private Color textColor;
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
        public Vector2 position { get; private set; }

        public MenuEntry(string text, Vector2 position, IMenuDelegate menuDelegate)
        {
            this.menuDelegate = menuDelegate;
            this.text = text;
            this.position = position;
            this.textColor = Color.White;
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

        public virtual void Update(bool highlighted)
        {
            if (highlighted)
            {
                //show that its highlighted
                this.textColor = Color.Red;
            }
            else
                this.textColor = Color.White;
        }

        public virtual void Draw(bool highlighted)
        {
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.Font, this.text, this.position, textColor);
            Game1.spriteBatch.End();
        }
    }
}
