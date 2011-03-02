using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.MovingObjects;

namespace RecordRobot.Menus
{
    public class Menu : List<MenuEntry>
    {
        /// <summary>
        /// Gets the currently selected menu entry. This is the index
        /// of the menu entry that is highlighted.
        /// </summary>
        /// <value>The currently highlighted menu entry.</value>
        public int CurrentSelected { get; private set; }

        /// <summary>
        /// The base this.position of the entire set of menu entries.
        /// </summary>
        /// <value>The position.</value>
        public Vector2 position { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="position">The this.position of the menu system.</param>
        public Menu(Vector2 position)
        {
            this.position = new Vector2(position.X, position.Y);
            this.CurrentSelected = 0;
        }

        /// <summary>
        /// If the associated menu entry is not null, and
        /// exists in the list, the index of the currently 
        /// selected menu entry is changed to the index
        /// of the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        private void TrySet(MenuEntry entry)
        {
            if (entry != null)
            {
                int temp = this.CurrentSelected;
                this.CurrentSelected = this.IndexOf(entry);

                if (this.CurrentSelected == -1)
                {
                    this.CurrentSelected = temp;
                }
            }
        }


        /// <summary>
        /// Updates this instance. This checks to see if the user has made an
        /// up or down selection, and if so, it tries to change the
        /// highlighted menu entry accordingly.
        /// </summary>
        public virtual void Update()
        {
                       
            if (Controls.GetDirection() == Direction.Up)
            {
                this.TrySet(this[this.CurrentSelected].UpperMenu);
            }
    
            if (Controls.GetDirection() == Direction.Down)
            {
                this.TrySet(this[this.CurrentSelected].LowerMenu);
            }

            //update newly highlighted option, and make sure others are not highlighted
            for (int i = 0; i < this.Count; i++)
            {
                if (this.CurrentSelected == i)
                {
                    this[this.CurrentSelected].Update(true);
                    this[this.CurrentSelected].TryRunDelegate();
                }
                else
                    this[i].Update(false);
            }
                
        }

        /// <summary>
        /// Draws each menu entry in the set.
        /// </summary>
        public virtual void Draw()
        {
            foreach (MenuEntry entry in this)
            {
                entry.Draw();
            }
        }
    }
}
