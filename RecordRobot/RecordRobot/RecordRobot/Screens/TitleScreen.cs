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
    class TitleScreen : GameScreen
    {

        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;

        /// <summary>
        /// This is the menu used for the pause screen.
        /// </summary>
        private TitleMenu menu;

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
        public TitleScreen()
            : base()
        {
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            this.menu = new TitleMenu(new Vector2(200, 200), 100);

            this.textDrawPosition = new Vector2(250, 200);
            //this.textDrawOrigin = Drawer.font.MeasureString("Paused") / 2f;
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
            // Write "RecordRobot" at the center of the screen.
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.Font, "Record Robot", this.textDrawPosition, Color.White);
            Game1.spriteBatch.End();
            // Draw menu
            this.menu.Draw();
        }
    }
}
