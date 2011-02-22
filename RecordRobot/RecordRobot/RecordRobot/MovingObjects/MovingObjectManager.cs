using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot.MovingObjects
{
    class MovingObjectManager
    {
        public static List<Mover> Objects;

        public static void Draw()
        {
            if (Objects == null)
            {
                Objects = new List<Mover>();
                Objects.Add(new Robot());
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
                Objects.Add(new Robot());
            }
            foreach (Mover m in Objects)
            {
                m.Update();
            }
        }
    }
}
