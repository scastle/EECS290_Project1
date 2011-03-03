using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RecordRobot.RRClasses;
using RecordRobot.GameElements;

namespace RecordRobot.MovingObjects
{
    public class Record : Mover
    {
        /*
         * I feel like most of the methods for moving around in here are going to be the same as the Robot
         * --- these could maybe be put in the mover class.
         * the difference in the record class will be determining its direction.
         * for different AIs use an enum and logic here, or another class that will compute next direction on its own
         * 
         */
        public bool CanDamage;

        public bool Gathered;

        public int DeathCount;

        public bool CountDown;

        public bool NotStuck;

        public int Value;

        public Direction CurrentDirection;

        public Point RelativePosition;

        private bool CanGo;

        private Point OldPosition;

        public RecordColor Color;

        private Texture2D OriginalTexture;

        public Record(int x, int y, RecordColor c)
        {
            CanDamage = true;
            CountDown = false;
            NotStuck = true;
            this.Position.X = x;
            this.Position.Y = y;
            this.Color = c;
            this.Value = ScoreManager.GetScore(c);
            if (Settings.DifficultyLevel != Settings.DifficultySettings.easy)
                Settings.RecordSpeed = 2;
            else
                Settings.RecordSpeed = 1;
            this.Speed = Settings.RecordSpeed;
            this.CurrentDirection = Direction.None;
            this.OldPosition = this.Position;
            //CanGo = false;
            switch (c)
            {
                case RecordColor.red:
                    this.Texture = Textures.RedRecord;
                    break;

            }
            switch (c)
            {
                case RecordColor.red:
                    this.Texture = Textures.RedRecord;
                    break;
                case RecordColor.orange:
                    this.Texture = Textures.OrangeRecord;
                    break;
                case RecordColor.yellow:
                    this.Texture = Textures.YellowRecord;
                    break;
                case RecordColor.green:
                    this.Texture = Textures.GreenRecord;
                    break;
                case RecordColor.blue:
                    this.Texture = Textures.BlueRecord;
                    break;
                case RecordColor.violet:
                    this.Texture = Textures.VioletRecord;
                    break;

            }
            this.OriginalTexture = Texture;
            Random rand = new Random();
            int r = rand.Next(4);
            switch (r)      //This is used to determine a random initial direction for the records
            {
                case 0:
                    this.Direction = Direction.Up;
                    break;
                case 1:
                    this.Direction = Direction.Down;
                    break;
                case 2:
                    this.Direction = Direction.Left;
                    break;
                case 3:
                    this.Direction = Direction.Right;
                    break;
            }
            if (Maze.grid == null)
                Console.WriteLine("THE GRID IS NOT INSTANTIATED");


            while (!Maze.CanGo(this.Position, this.Direction))   //This will choose a random direction if the initial random direction pointed the record towards a wall
            {
                if ((int)this.Direction == 2)
                {
                    this.Direction = Direction.Right;
                }
                //if (!Maze.CanGo(this.Position, this.Direction))
                this.Direction++;
                //else
                //CanGo = true;
            }
            //this.Direction = Direction.None;

        }

        public override void Update()
        {
            TimeSpan elapsedTime = DateTime.Now - Game1.Time;
            //Random rand = new Random();     // I am not putting in an ai that knows where the robot is yet, so this is used in helping choose the direction
            int r;
            if (Maze.grid == null)
                Console.WriteLine("THE GRID IS NOT INSTANTIATED");
            if (Maze.IsDeadEnd(this.Position))
                this.Direction = (Direction)((int)this.Direction * -1);
            this.RelativePosition = MovingObjectManager.GetRelativePosition(this.Color);
            if (Maze.IsIntersection(this.Position))
            {
                this.CurrentDirection = this.Direction;
                if ((Settings.DifficultyLevel == Settings.DifficultySettings.easy || (this.Color != RecordColor.grey && this.Color != MovingObjectManager.nextColor)) || 
                    (Settings.DifficultyLevel != Settings.DifficultySettings.hard && (this.Color == RecordColor.grey)) &&
                    ((int)Settings.DifficultyLevel > (int)Settings.DifficultySettings.easy && (this.Color == MovingObjectManager.nextColor)))
                {
                    do   //This do while loop will choose a random direction to go at an intersection (until we want to implement an ai that will chase or flee from the robot which is not a priority for the demo)
                    {
                        r = Game1.rand.Next(4);
                        switch (r)
                        {
                            case 0:
                                this.Direction = Direction.Right;
                                break;
                            case 1:
                                this.Direction = Direction.Left;
                                break;
                            case 2:
                                this.Direction = Direction.Up;
                                break;
                            case 3:
                                this.Direction = Direction.Down;
                                break;
                        }

                        if (Maze.CanGo(this.Position, this.Direction))
                        {
                            CanGo = true;
                            if ((int)this.CurrentDirection + (int)this.Direction == 0)
                                CanGo = false;
                        }

                    } while (!CanGo);
                    CanGo = false;
                }
                else 
                {
                    
                    MovingObjectManager.SetRelativeDirection(this.Color);
                    if (this.Color == MovingObjectManager.nextColor)
                    {
                        this.AIChoice1 = (Direction)((int)this.AIChoice1 * -1);
                        this.AIChoice2 = (Direction)((int)this.AIChoice2 * -1);
                    }
                    if (Maze.CanGo(this.Position, this.AIChoice1))
                        this.Direction = this.AIChoice1;
                    else if (Maze.CanGo(this.Position, this.AIChoice2))
                        this.Direction = this.AIChoice2;
                    else
                    {
                        r = Game1.rand.Next(2);
                        if (r == 0)
                        {
                            this.Direction = (Direction)((int)this.AIChoice1 * -1);
                            if (!Maze.CanGo(this.Position, this.Direction))
                                this.Direction = (Direction)((int)this.AIChoice2 * -1);
                        }
                        else
                        {
                            this.Direction = (Direction)((int)this.AIChoice2 * -1);
                            if (!Maze.CanGo(this.Position, this.Direction))
                                this.Direction = (Direction)((int)this.AIChoice1 * -1);
                        }
                    }
                }
            }
            else if ((this.Color == RecordColor.grey || this.Color == MovingObjectManager.nextColor) && (RelativePosition.X < 59 && RelativePosition.Y < 59) && Settings.DifficultyLevel != Settings.DifficultySettings.easy)
            {
                if(Settings.DifficultyLevel == Settings.DifficultySettings.hard || this.Color == MovingObjectManager.nextColor)
                    MovingObjectManager.AIChangeDirection(this.Color);
            }

            //if (elapsedTime.Milliseconds % 100 == 0)
            //{
            //    if (elapsedTime.Milliseconds % 2000 == 0)
            //        NotStuck = true;
            //    if (this.OldPosition.X - this.Position.X < 5 && this.OldPosition.Y - this.Position.Y < 5)
            //        NotStuck = false;
            //    this.OldPosition = this.Position;                
            //}

            if (CountDown)
                DeathCount++;

            if (DeathCount < 100 && CountDown)
            {
                if ((DeathCount > 20 && DeathCount < 40) || (DeathCount > 60 && DeathCount < 80))
                    this.Texture = OriginalTexture;
                else
                    this.Texture = Textures.GreyRecord;

            }

            if (DeathCount == 100)
            {
                this.Texture = Textures.GreyRecord;
                this.Color = RecordColor.grey;
                this.CanDamage = true;
                this.Value = ScoreManager.GetScore(Color);
                Gathered = false;
            }

            base.UpdatePosition();

        }

        public void ChangeToGrey()
        {
            CountDown = true;
            this.Value = 0;
            CanDamage = false;
        }
    }
}
