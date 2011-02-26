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
        red = 0, orange=1, yellow = 2, green = 3, blue = 4, violet = 5, grey = -1
    }

    abstract class Mover
    {
        /// <summary>
        /// The speed of the moving object.
        /// </summary>
        public int Speed;

        ///// <summary>
        ///// The speed of the moving object.
        ///// </summary>
        //public static Point RobotPosition;

        public Texture2D BufferTexture;

        public Texture2D Texture;

        /// <summary>
        /// The current location of the object.
        /// </summary>
        public Point Position;

        private Vector2 drawPosition = new Vector2();

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
            drawPosition.X = Position.X - Texture.Width / 2;
            drawPosition.Y = Position.Y - Texture.Height / 2;
            Game1.spriteBatch.Begin();
            Game1.spriteBatch.Draw(Texture, drawPosition, Color.White);
            Game1.spriteBatch.End();
        }
    }
}
