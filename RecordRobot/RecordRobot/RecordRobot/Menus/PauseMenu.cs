using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.Menus;
using RecordRobot.Menus.MenuDelegates;

namespace RecordRobot.Menus
{
    /// <summary>
    /// An instance of a pause menu. This will support the options
    /// to resume the game, restart the level, go to the options menu,
    /// buy the game (if in trial mode), go to the title sceen, and 
    /// quit the game.
    /// </summary>
    public class PauseMenu : Menu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PauseMenu"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="actions">The actions.</param>
        /// <param name="spacing">The spacing.</param>
        public PauseMenu(Vector2 position, float spacing)
            : base(position)
        {
            MenuEntry resume = new MenuEntry("Resume", position, new QuitTopDelegate());

            MenuEntry quit = new MenuEntry("Quit", position + new Vector2(0, spacing), new QuitGameDelegate());

            MenuEntry main = new MenuEntry("Main Menu", position + new Vector2(0, spacing * 2), new MainMenuDelegate());

            resume.UpperMenu = main;
            resume.LowerMenu = quit;

            quit.UpperMenu = resume;
            quit.LowerMenu = main;

            main.UpperMenu = quit;
            main.LowerMenu = resume;

            this.Add(resume);
            this.Add(quit);
            this.Add(main);
        }
    }
}
