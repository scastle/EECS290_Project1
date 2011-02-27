using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RecordRobot.MovingObjects
{
    class Record : Mover
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

        public Direction CurrentDirection;

        public RecordColor Color;

        private bool CanGo;

        private Texture2D OriginalTexture;

        public Record(int x, int y, RecordColor c)
        {
            CanDamage = true;
            CountDown = false;
            this.Position.X = x;
            this.Position.Y = y;
            this.Color = c;
            this.Speed = Settings.RecordSpeed;
            this.CurrentDirection = Direction.None;
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
            //Random rand = new Random();     // I am not putting in an ai that knows where the robot is yet, so this is used in helping choose the direction
            int r;
            if (Maze.grid == null)
                Console.WriteLine("THE GRID IS NOT INSTANTIATED");

            if (Maze.IsIntersection(this.Position))
            {
                this.CurrentDirection = this.Direction;
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

            if (CountDown)
                DeathCount++;

            if (DeathCount < 100 && CountDown)
            {
                if ((DeathCount > 20 && DeathCount < 40) || (DeathCount > 60 && DeathCount < 80))
                    this.Texture = OriginalTexture;
                else
                    this.Texture = Textures.GreyRecord;
                
            }

            if(DeathCount == 100)
            {
                this.Texture = Textures.GreyRecord;
                this.Color = RecordColor.grey;
                this.CanDamage = true;
                Gathered = false;
            }

            base.UpdatePosition();
                        
        }

        public void ChangeToGrey()
        {
            CountDown = true;
            CanDamage = false;
        }
    }
}
