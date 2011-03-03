using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.GameElements;
using Microsoft.Xna.Framework;
using RecordRobot.Clock;

namespace RecordRobot.Screens
{
    class GameOverScreen : GameScreen
    {
        /// <summary>
        /// This is the time (DateTime, not GameClock) 
        /// that the screen is created.
        /// </summary>
        private long initialTime;


        /// <summary>
        /// This is the duration of the screen
        /// </summary>
        private long duration;

        /// <summary>
        /// Where to write text
        /// </summary>
        private Vector2 textDrawPosition;


        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverScreen"/> class.
        /// </summary>
        public GameOverScreen()
            : base()
        {
            Game1.screens.IsTitle = false;
            Game1.screens.IsPaused = false;
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            duration = 50000000;
            textDrawPosition = new Vector2(260, 230);

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

            //exit screen and go to main menu after certain time has passed
            if (DateTime.Now.Ticks > duration + initialTime)
            {
                
                this.Disposed = true;
                Game1.ToTitle();

            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            // Write "Game Over" at the center of the screen.
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.DrawString(Game1.Font, "Game Over", this.textDrawPosition, Color.White);
            Game1.spriteBatch.End();

        }
    }
}
