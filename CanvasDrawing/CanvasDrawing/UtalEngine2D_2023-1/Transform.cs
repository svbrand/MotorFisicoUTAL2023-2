using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class Transform
    {
        public Vector2 position;
        public Vector2 scale;
        public float rotation;
        public Vector2 Up { get { return Vector2.RotateVector(new Vector2(0, -1), -rotation); } }
        public Vector2 Right { get { return Vector2.RotateVector(new Vector2(1, 0), -rotation); } }
    }
}
