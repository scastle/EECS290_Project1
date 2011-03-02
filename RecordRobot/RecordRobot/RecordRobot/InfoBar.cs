using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Globalization;
using Microsoft.Xna.Framework.Graphics;
using RecordRobot.RRClasses;

namespace RecordRobot
{
    public class InfoBar
    {
        public static int Height = 30;
        public static int Width = 600;
        private static SpriteFont font;

        public static Vector2 LivesPosition = new Vector2(0, 450);
        public static Vector2 ScorePosition = new Vector2(150, 450);
        public static Vector2 TimePosition = new Vector2(300, 450);
        public static Vector2 RecordPosition = new Vector2(400, 450);

        public static void Draw()
        {
            TimeSpan elapsedTime = DateTime.Now - Game1.Time;
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(Textures.InfobarBackground, new Vector2(0, 450), Color.Black);
            Game1.spriteBatch.DrawString(Game1.Font, "Lives: " + MovingObjects.MovingObjectManager.RobotPlayer.Lives, LivesPosition, Color.White);
            Game1.spriteBatch.DrawString(Game1.Font, "Score: " + ScoreManager.CurrentScore, ScorePosition, Color.White);
            Game1.spriteBatch.DrawString(Game1.Font, "Time: " + elapsedTime.Minutes + ":" + (elapsedTime.Seconds % 60).ToString("d2"), TimePosition, Color.White);
            
            MovingObjects.Mover[] m = MovingObjects.MovingObjectManager.Objects.ToArray();
            for (int i = 0; i <= Game1.CurrentLevel.NumRecords; i++)
            {
                if (m[i] is MovingObjects.Record)
                {
                    Game1.spriteBatch.Draw(m[i].Texture, new Vector2(400 + i * 30, 455), Color.White);
                }
            }
            Game1.spriteBatch.End();
        }
    }
}
