﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RecordRobot
{
    class Controls
    {
        public static MovingObjects.Direction GetDirection()
        {
            // add logic here
            return MovingObjects.Direction.Up;
        }
    }
}
