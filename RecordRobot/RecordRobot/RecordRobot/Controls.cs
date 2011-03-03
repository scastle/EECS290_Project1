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

        //returns true if enter is pressed
        //for use by menus
        public static bool Enter()
        {
            State = Keyboard.GetState();
            if(State.IsKeyDown(Keys.Enter))
                return true;
            else
                return false;
        }

        //returns true if back is pressed for menus
        public static bool Back()
        {
            State = Keyboard.GetState();
            if( State.IsKeyDown(Keys.Escape) || State.IsKeyDown(Keys.Back) )
                return true;
            else
                return false;
        }

        public static bool PauseGame()
        {
            if (State.IsKeyDown(Keys.Escape))
                return true;
            else
                return false;

        }

        public static void SkipLevel()
        {
            State = Keyboard.GetState();
            if (State.IsKeyDown(Keys.P))
                MovingObjectManager.NextLevel();
        }

    }
}
