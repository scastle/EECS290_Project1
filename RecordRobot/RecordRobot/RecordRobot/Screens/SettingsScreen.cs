using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.Clock;
using RecordRobot.Menus;
using RecordRobot.GameElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RecordRobot.Screens
{
    public class SettingsScreen : GameScreen
    {

        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;

        /// <summary>
        /// This is the menu used for the pause screen.
        /// </summary>
        public SettingsMenu menu;

        /// <summary>
        /// Where to write "Record Robot"
        /// </summary>
        private Vector2 textDrawPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsScreen"/> class.
        /// </summary>
        public SettingsScreen()
            : base()
        {
            Game1.screens.IsSettings = true;
            Game1.screens.IsTitle = false;
            Game1.screens.IsPaused = false;
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            this.menu = new SettingsMenu(new Vector2(150, 175), 50);

            this.textDrawPosition = new Vector2(150, 100);

        }

        public void Drawer()
        {
            this.Draw();
        }

        /// <summary>
        /// Updates this instance. This makes sure that GameClock is paused,
        /// and it also updates the menu.
        /// </summary>
        public override void Update()
        {
            base.Update();
            this.initialTime = Game1.screens.screenChanged;
            // Only pause the gameclock if the screen is not fading out.
            if (!this.FadingOut)
            {
                GameClock.Pause();
            }
            else
            {
                GameClock.Unpause();
            }

            this.menu.Update();
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            // Write "RecordRobot" at the center of the screen.
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.Font, "Settings", this.textDrawPosition, Color.White);
            Game1.spriteBatch.End();
            // Draw menu
            this.menu.Draw();
        }
    }
}
