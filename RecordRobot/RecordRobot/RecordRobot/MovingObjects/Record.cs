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

        public enum RecordColor
        {
            red = 0, orange = 1, yellow = 2, green = 3, blue = 4, indigo = 5, violet = 6, grey = -1
        }

        public Direction NextDirection;
        public RecordColor color;

        public Record(int x, int y, RecordColor c)
        {
            this.Position.X = x;
            this.Position.Y = y;
            this.color = c;
        }
        public override void Update()
        {
        }
    }
}
