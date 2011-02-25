using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public bool CanLoseLives;

        public bool Collision;

        public bool Gathered;

        public int CollisionCount;

        public int DeathCount;

        public bool CountDown;

        public Direction CurrentDirection;

        //public Direction CurrentDirection;

        public RecordColor color;

        private bool CanGo;

        public Record(int x, int y, RecordColor c)
        {
            CanLoseLives = true;
            Collision = false;
            CollisionCount = 0;
            CountDown = false;
            DeathCount = 0;
            //Maze.LoadMaze("TextFiles\\testmaze.txt");
            //Maze.Draw();
            this.Position.X = x;
            this.Position.Y = y;
            this.color = c;
            this.Speed = 2;
            this.CurrentDirection = Direction.None;
            //CanGo = false;
            switch (c)
            {
                case RecordColor.red:
                    this.Texture = Game1.RedRecord;
                    break;

            }
            switch (c)
            {
                case RecordColor.red:
                    this.Texture = Game1.RedRecord;
                    break;
                case RecordColor.yellow:
                    this.Texture = Game1.YellowRecord;
                    break;
                case RecordColor.green:
                    this.Texture = Game1.GreenRecord;
                    break;
                case RecordColor.blue:
                    this.Texture = Game1.BlueRecord;
                    break;
                case RecordColor.violet:
                    this.Texture = Game1.VioletRecord;
                    break;

            }
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

            //COLLISION DETECTION

            if ((int)this.color != (int)RecordColor.grey && Maze.CheckCollisionRobot(this.Position))
            {


                if ((int)Maze.TargetColor == (int)this.color)
                {
                    this.Texture = Game1.GreyRecord;
                    CountDown = true;
                    Maze.NextTarget(this.color);
                    Collision = true;
                    CanLoseLives = false;
                    Gathered = true;
                }
                else if ((int)Maze.TargetColor != (int)this.color && CanLoseLives && Maze.CheckCollisionRobot(this.Position))
                {
                    Maze.Lives--;
                    Collision = true;
                    CanLoseLives = false;
                }

            }
            else
            {
                if ((DeathCount > 100 && CanLoseLives && !Collision) && Maze.CheckCollisionRobot(this.Position))
                {
                    Maze.CheckCollisionGreyRecord(this.Position);
                    Collision = true;
                    CanLoseLives = false;
                    Maze.Lives--;
                }               
                
            }

            if(Collision)
            {
                CollisionCount++;
            }

            if (CollisionCount > 100)
            {
                CollisionCount = 0;
                Collision = false;
                CanLoseLives = true;
                Gathered = false;
            }

            //if (CollisionCount <= 100 && Collision && (int)this.color != (int)Maze.TargetColor - 1)
            //{
            //    Maze.RobotDamaged(CollisionCount);
            //}

            if (Maze.Lives <= 0)
                MovingObjectManager.GameOver = true;

            if (CountDown)
                DeathCount++;

            if (DeathCount < 100 && CountDown)
            {
                if ((DeathCount > 20 && DeathCount < 40) || (DeathCount > 60 && DeathCount < 80))
                    switch (this.color)
                    {
                        case RecordColor.red:
                            this.Texture = Game1.RedRecord;
                            break;
                        case RecordColor.yellow:
                            this.Texture = Game1.YellowRecord;
                            break;
                        case RecordColor.green:
                            this.Texture = Game1.GreenRecord;
                            break;
                        case RecordColor.blue:
                            this.Texture = Game1.BlueRecord;
                            break;
                        case RecordColor.violet:
                            this.Texture = Game1.VioletRecord;
                            break;
                    }
                else
                    this.Texture = Game1.GreyRecord;
                
            }

            if(DeathCount == 100)
            {
                this.Texture = Game1.GreyRecord;
                this.color = RecordColor.grey;
                CanLoseLives = true;
                Gathered = false;
            }

            //if (Collision && !Gathered)
            //{
            //    Maze.RobotDamaged(CollisionCount);
            //}
            //else
            //    Maze.RobotFlashing = false;

            //if we add objects other than records and the robot, we'll also need to subtract those
            if ((int)Maze.TargetColor == MovingObjectManager.Objects.Count() - 1)
                MovingObjectManager.GameWin = true;


            base.UpdatePosition();
                        
        }
    }
}
