using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordRobot.Screens;

namespace RecordRobot.GameElements
{
    /// <summary>
    /// This is a stack of screens used in the game.
    /// Do not Add or Remove screens yourself. Use the "Play"
    /// method to pop a new screen on the stack, and if you want
    /// to remove a screen, set the GameScreen.Disposed value
    /// to true for a screen and it will get removed for you.
    /// </summary>
    public class ScreenContainer : List<GameScreen>
    {
        /// <summary>
        /// Gets a value indicating whether the game is paused.
        /// </summary>
        /// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
        public bool IsPaused { get; set; }

        public bool IsSettings { get; set; }

        //The last time a screen was popped
        public long screenChanged { get; set; }


        public bool Beginning { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the game is in Title Screen.
        /// </summary>
        /// <value><c>true</c> if this instance is in Title; otherwise, <c>false</c>.</value>
        public bool IsTitle { get; set; }

        /// <summary>
        /// This is the screen that will be added as soon as this instance updates.
        /// </summary>
        private GameScreen toAdd;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenContainer"/> class.
        /// </summary>
        public ScreenContainer()
            : base()
        {
            this.IsTitle = false;
            this.IsPaused = false;
            this.IsSettings = false;
            this.toAdd = null;

        }


        /// <summary>
        /// Plays the specified Screen.
        /// This will not happen until Update is called.
        /// </summary>
        /// <param name="toAdd">To new GameScreen to be added.</param>
        public void Play(GameScreen toAdd)
        {
            this.toAdd = toAdd;
        }

        /// <summary>
        /// Adds the screen in "toAdd" to the stack.
        /// </summary>
        private void Add()
        {
            if (this.toAdd != null)
            {
                this.Add(this.toAdd);
                this.toAdd = null;
            }
        }

        /// <summary>
        /// Finds all screens which have the value of
        /// "Disposed" set to true and removes them.
        /// </summary>
        private void Kill()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Disposed)
                {
                    if ((this[i] as PauseScreen) != null)
                    {
                        this.IsPaused = false;
                    }
                    if ((this[i] as TitleScreen) != null)
                    {
                        this.IsTitle = false;
                    }
                    if ((this[i] as SettingsScreen) != null)
                    {
                        this.IsSettings = false;
                    }
                    screenChanged = DateTime.Now.Ticks;
                    Remove(this[i]);
                }
            }
        }

        /// <summary>
        /// Removes all screens from this stack. Update
        /// must be called after this for this to take effect.
        /// </summary>
        public void KillAll()
        {
            foreach (GameScreen screen in this)
            {
                screen.Disposed = true;
            }
        }

        /// <summary>
        /// Pauses the game and adds a Pause Screen to the stack.
        /// </summary>
        public void Pause()
        {
            this.Add(new PauseScreen());
        }


        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            this.Kill();
            this.Add();

            // Update the screens from top to bottom, stopping when a
            // screen is found that is not "fading out".
            for (int i = Count - 1; i >= 0; i--)
            {
                this[i].Update();
                if (!this[i].FadingOut)
                {
                    break;
                }
            }

            // Check if the game is being paused, and there is no pause screen on the stack.
            if (!this.IsTitle && !this.IsPaused && !this.IsSettings && Controls.PauseGame())
            {
                this.IsPaused = true;
                this.Pause();
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            foreach (GameScreen screen in this)
            {
                screen.Draw();
            }
        }
    }
}
