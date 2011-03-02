using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.GameElements;
using RecordRobot.Menus;
using RecordRobot.Clock;

namespace RecordRobot.Screens
{
    /// <summary>
    /// This instanciates a new Pause Screen.
    /// </summary>
    public class PauseScreen : GameScreen
    {
        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;

        /// <summary>
        /// This is the menu used for the pause screen.
        /// </summary>
        private PauseMenu menu;

        /// <summary>
        /// Where to write "Paused"
        /// </summary>
        private Vector2 textDrawPosition;

        /// <summary>
        /// The center of the word "Paused".
        /// </summary>
        private Vector2 textDrawOrigin;

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseScreen"/> class.
        /// </summary>
        public PauseScreen()
            : base()
        {
            Game1.screens.IsPaused = true;
            Game1.screens.IsTitle = false;
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            this.menu = new PauseMenu(new Vector2(300, 100), 50);

            this.textDrawPosition = new Vector2(300, 50);
        }

        /// <summary>
        /// Updates this instance. This makes sure that GameClock is paused,
        /// and it also updates the menu.
        /// </summary>
        public override void Update()
        {
            base.Update();

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
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.Font, "Paused", this.textDrawPosition, Color.White);
            Game1.spriteBatch.End();
            // Write "Paused" at the center of the screen.
            /*Drawer.DrawString(
                "Paused",
                this.textDrawPosition,
                Color.Black,
                0f,
                this.textDrawOrigin,
                0.35f,
                SpriteEffects.None,
                1f);
            */
            // Draw menu
            this.menu.Draw();
        }
    }
}
