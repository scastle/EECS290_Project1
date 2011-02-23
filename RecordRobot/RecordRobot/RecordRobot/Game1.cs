using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RecordRobot
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Texture2D Robot;
        public static Texture2D RobotLeft;
        public static Texture2D RobotRight;
        public static Texture2D mazepath;
        public static Texture2D mazewall;
        public static Texture2D RedRecord;
        public static Texture2D GreyRecord;
        public static Texture2D BlueRecord;
        public static Texture2D GreenRecord;
        public static Texture2D VioletRecord;
        public static Texture2D YellowRecord;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Robot = this.Content.Load<Texture2D>("Images\\robot-normal");
            RobotLeft = this.Content.Load<Texture2D>("Images\\robot-left");
            RobotRight = this.Content.Load<Texture2D>("Images\\robot-right");
            mazepath = this.Content.Load<Texture2D>("Images\\maze-path");
            mazewall = this.Content.Load<Texture2D>("Images\\maze-wall");
            RedRecord = this.Content.Load<Texture2D>("Images\\record-red");
            GreyRecord = this.Content.Load<Texture2D>("Images\\record-grey");
            BlueRecord = this.Content.Load<Texture2D>("Images\\record-blue");
            GreenRecord = this.Content.Load<Texture2D>("Images\\record-green");
            VioletRecord = this.Content.Load<Texture2D>("Images\\record-violet");
            YellowRecord = this.Content.Load<Texture2D>("Images\\record-yellow");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MovingObjects.MovingObjectManager.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GameScreen.Draw();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
