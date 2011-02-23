using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.MovingObjects
{
    class MovingObjectManager
    {
        /// <summary>
        /// All moving objects to be updated and drawn are placed in this list.
        /// </summary>
        public static List<Mover> Objects;

        public static void Draw()
        {
            if (Objects == null)
            {
                Objects = new List<Mover>();
                Objects.Add(new Robot(45, 45));
            }
            foreach (Mover m in Objects)
            {
                m.Draw();
            }
        }

        public static void Update()
        {
            if (Objects == null)
            {
                Objects = new List<Mover>();
                Objects.Add(new Robot(45,45));
            }
            foreach (Mover m in Objects)
            {
                m.Update();
            }
        }
    }
}
