using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.Menus;
using RecordRobot.Menus.MenuDelegates;
using RecordRobot.GameElements;

namespace RecordRobot.Menus
{
    public class SettingsMenu : Menu
    {
        private String DifSet;

        public MenuEntry numRecords { get; private set; }

        public MenuEntry difficulty { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleMenu"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="spacing">The spacing.</param>
        public SettingsMenu(Vector2 position, float spacing)
            : base(position)
        {
            switch ((int) Settings.DifficultyLevel)
            {
                case 0:
                    DifSet = "Easy";
                    break;
                case 1:
                    DifSet = "Medium";
                    break;
                case 2:
                    DifSet = "Hard";
                    break;
            }
            difficulty = new MenuEntry("Difficulty: " + DifSet, position, new DifficultyDelegate());

            numRecords = new MenuEntry("Number of Records: " + Settings.NumRecords, position + new Vector2(0, spacing), new NumRecordsDelegate());

            MenuEntry mainMenu = new MenuEntry("Main Menu", position + new Vector2(0, spacing * 2), new MainMenuDelegate());

            difficulty.UpperMenu = mainMenu;
            difficulty.LowerMenu = numRecords;

            numRecords.UpperMenu = difficulty;
            numRecords.LowerMenu = mainMenu;

            mainMenu.UpperMenu = numRecords;
            mainMenu.LowerMenu = difficulty;

            this.Add(difficulty);
            this.Add(numRecords);
            this.Add(mainMenu);
        }
    }
}
