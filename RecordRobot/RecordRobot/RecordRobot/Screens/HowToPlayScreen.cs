using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.GameElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RecordRobot.Clock;


namespace RecordRobot.Screens
{
    class HowToPlayScreen : GameScreen
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
        /// This is the texture displayed in the screen
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Where to draw screen
        /// </summary>
        private Vector2 drawPosition;


        /// <summary>
        /// Initializes a new instance of the <see cref="HowToScreen"/> class.
        /// </summary>
        public HowToPlayScreen()
            : base()
        {
            // Note: Do not use GameClock, it will be paused!
            this.initialTime = DateTime.Now.Ticks;
            drawPosition = new Vector2(0, 0);
            this.duration = 1000000;
            this.texture = Textures.HowToPlay;

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
            if (DateTime.Now.Ticks > initialTime + duration)
            {
                if (Controls.Back() || Controls.Enter())
                {
                    if (Game1.screens.Count > 0)
                    {
                        Game1.screens[Game1.screens.Count - 1].Disposed = true;
                    }
                }
            }
            

            
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            // Draw help screen.
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(this.texture, this.drawPosition, Color.White);
            Game1.spriteBatch.End();

        }
    }
}
