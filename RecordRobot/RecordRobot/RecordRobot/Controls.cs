using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RecordRobot.MovingObjects;

namespace RecordRobot
{
    public class Controls
    {
        public static KeyboardState State;

        public static Direction GetDirection()
        {
            State = Keyboard.GetState();
            if (State.IsKeyDown(Keys.Up) || State.IsKeyDown(Keys.W))
            {
                return Direction.Up;
            }
            else if (State.IsKeyDown(Keys.Down) || State.IsKeyDown(Keys.S))
            {
                return Direction.Down;
            }
            else if (State.IsKeyDown(Keys.Left) || State.IsKeyDown(Keys.A))
            {
                return Direction.Left;
            }
            else if (State.IsKeyDown(Keys.Right) || State.IsKeyDown(Keys.D))
            {
                return Direction.Right;
            }
            else
            {
                return Direction.None;
            }
        }

        public static void PauseGame()
        {
            if (State.IsKeyDown(Keys.Escape))
                Game1.GamePaused = true;
        }

        public static void SkipLevel()
        {
            TimeSpan elapsedTime = DateTime.Now - Game1.Time;
            if (State.IsKeyDown(Keys.P) && elapsedTime.Milliseconds % 100 == 0)
                MovingObjectManager.GameWin = true;
        }
    }
}
