using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.Menus;
using RecordRobot.Menus.MenuDelegates;

namespace RecordRobot.Menus
{
    class TitleMenu : Menu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PauseMenu"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="actions">The actions.</param>
        /// <param name="spacing">The spacing.</param>
        public TitleMenu(Vector2 position, float spacing)
            : base(position)
        {
            MenuEntry resume = new MenuEntry("Start",position, new StartDelegate());

            MenuEntry quit = new MenuEntry("Quit", position + new Vector2(0, spacing), new QuitGameDelegate());

            resume.UpperMenu = quit;
            resume.LowerMenu = quit;

            quit.UpperMenu = resume;
            quit.LowerMenu = resume;

            this.Add(resume);
            this.Add(quit);
        }
    }
}
