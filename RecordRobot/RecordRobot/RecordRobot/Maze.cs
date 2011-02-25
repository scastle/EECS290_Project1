using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RecordRobot.MovingObjects;

namespace RecordRobot
{
    class Maze
    {
        public static bool[,] grid; //true for paths, false for walls

        public enum CollisionType
        {
            wall = -4, path = -3, robot = -2, greyrecord = -1, redrecord = 0, yellowrecord = 1, greenrecord = 2, bluerecord = 3, violetrecord = 4
        }

        public static int Lives;

        public static CollisionType TargetColor;//used to determine which record is the target, target order is order of rainbow (rygbv)

        public static CollisionType[,] CollisionGrid;

        public static bool RobotFlashing;

        public static void LoadMaze(string file)
        {
            grid = new bool[,]
            {
                {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                {false, true , true , true , false, false, true , true , true , true , false, true , true , true , false},
                {false, true , false, true , true , true , true , false, false, true , true , true , false, true , false},
                {false, true , true , true , false, false, true , true , true , true , false, true , true , true , false},
                {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false}
            };

            CollisionGrid = new CollisionType[,]
            {
                {CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall},
                {CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall},
                {CollisionType.wall, CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.wall},
                {CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall, CollisionType.path , CollisionType.path , CollisionType.path , CollisionType.wall},
                {CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall, CollisionType.wall}
            };


            //string filename = "RecordRobotContent/TextFiles/testmaze.txt";
            //string path = Path.Combine(StorageContainer.TitleLocation, filename);
            //int i ;
            //string lineOfText;
            //System.IO.Stream stream = TitleContainer.OpenStream("RecordRobotContent/TextFiles/testmaze.txt");
            //StreamReader sr = new StreamReader(filename);
            //while ((i = stream.ReadByte()) != -1)
            //{
                // do something
            //}

            /*
            StreamReader re = File.OpenText(file);
            string input = null;
            int r = 0;
            while ((input = re.ReadLine()) != null)
            {
                for (int c = 0; c < input.Length; c++)
                {
                    if (input.Substring(c, 1).Equals("1"))
                    {
                        grid[r, c] = true;
                        Console.Write("1");
                    }
                    else
                    {
                        grid[r, c] = false;
                        Console.Write("0");

                    }
                }
                r++;
                Console.Write("/n");
            }
            re.Close();
            */

        }


        /// <summary>
        /// Checks if the specified point is an intersection
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <returns>true if the point is an intersection</returns>
        public static bool IsIntersection(Point p)
        {
            //return true;
            int r = p.Y / 30;
            int c = p.X / 30;


            //if (r < 1 || c < 1)
            //{
            //    return false; //TEMPORARY FIX
            //}
            if ((grid[r - 1, c] || grid[r + 1, c]) && (grid[r, c - 1] || grid[r, c + 1]) 
                && ((p.X - 15) % 30 == 0 && (p.Y - 15) % 30 == 0))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Checks if the robot can move in the current direction
        /// </summary>
        /// <param name="d">The direction to move</param>
        /// <returns>true if the robot can move in the direction</returns>
        public static bool CanGo(Point p, Direction d)
        {
            //return true;
            int r = p.Y / 30;
            int c = p.X / 30;
            int xsquare = p.X % 30;
            int ysquare = p.Y % 30;

            switch (d)
            {
                case Direction.Up:
                    if (grid[r - 1, c] || ysquare > 15)
                        return true;
                    break;
                case Direction.Down:
                    if (grid[r + 1, c] || ysquare < 15)
                        return true;
                    break;
                case Direction.Left:
                    if (grid[r, c - 1] || xsquare > 15)
                        return true;
                    break;
                case Direction.Right:
                    if (grid[r, c + 1] || xsquare < 15)
                        return true;
                    break;
            }
            return false;
        }


        /// <summary>
        /// Updates the robot's position in the collision grid
        /// </summary>
        /// <param name="p">the position of the robot</param>
        public static void UpdatePosition(Point p, CollisionType col)
        {
            int r = p.Y / 30;
            int c = p.X / 30;
            CollisionGrid[r, c] = col;
        }


        /// <summary>
        /// Checks whether there is a collision with the robot. this is used to tell if the robot collides with a non grey record
        /// </summary>
        /// <param name="p">The position of the record</param>
        /// <returns>true if the robot is colliding with the record</returns>
        public static bool CheckCollisionRobot(Point p)
        {
            int r = p.Y / 30;
            int c = p.X / 30;
            if ((int)CollisionGrid[r, c] == (int)CollisionType.robot)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Checks whether there is a collision with the robot. This is used to tell if the robot collides with a GREY record
        /// </summary>
        /// <param name="p">The position of the robot</param>
        /// <returns>true if the robot is colliding with a grey record</returns>
        public static void CheckCollisionGreyRecord(Point p)
        {
            int r = p.Y / 30;
            int c = p.X / 30;

            if ((int)CollisionGrid[r, c] == (int)CollisionType.robot)
            {
                //Lives--;
            }
        }

        public static void NextTarget(RecordColor c)
        {
            TargetColor = TargetColor + 1;
        }

        public static void RobotDamaged(int c)
        {
            if ((c > 20 && c < 40) || (c > 60 && c < 80))
                RobotFlashing = true;
            else
                RobotFlashing = false;

            if (c >= 99)
                RobotFlashing = false;
        }

        public static void Draw()
        {
            Game1.spriteBatch.Begin();
            for (int r = 0; r <= grid.GetUpperBound(0); r++)
            {
                for (int c = 0; c <= grid.GetUpperBound(1); c++)
                {
                    if (grid[r, c])
                        Game1.spriteBatch.Draw(Game1.mazepath, new Vector2(c * 30, r * 30), Color.Black);
                    else
                        Game1.spriteBatch.Draw(Game1.mazewall, new Vector2(c * 30, r * 30), Color.Blue);
                }
            }
            Game1.spriteBatch.End();
            
        }
    }
}
