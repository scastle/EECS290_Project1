using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RecordRobot.MovingObjects
{
    public enum Direction
    {
        None = 0, Up = 1, Down = -1, Left = 2, Right = -2
    }

    public enum RecordColor
    {
        red = 0, orange = 1, yellow = 2, green = 3, blue = 4, indigo = 5, violet = 6, grey = -1
    }

    abstract class Mover
    {
        /// <summary>
        /// The speed of the moving object.
        /// </summary>
        public int Speed;

        public Texture2D Texture;

        /// <summary>
        /// The current location of the object.
        /// </summary>
        public Point Position;

        /// <summary>
        /// The current direction the robot is moving.
        /// </summary>
        public Direction Direction;

        public abstract void Update();

        /// <summary>
        /// Updates the objects location based on the current direction.
        /// </summary>
        public void UpdatePosition()
        {
            switch (this.Direction)
            {
                case Direction.Up: this.Position.Y -= Speed; break;
                case Direction.Down: this.Position.Y += Speed; break;
                case Direction.Left: this.Position.X -= Speed; break;
                case Direction.Right: this.Position.X += Speed; break;
            }
        }

        public virtual void Draw()
        {
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(Texture, new Vector2(Position.X - Texture.Width / 2, Position.Y - Texture.Height / 2), Color.White);
            Game1.spriteBatch.End();
        }
    }
}
