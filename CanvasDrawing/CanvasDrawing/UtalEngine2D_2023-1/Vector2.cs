using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public struct Vector2
    {
        public float x;
        public float y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt(x*x + y*y);
        }
        public float SquareMagnitude()
        {
            return (float)(x * x + y * y);
        }
        public Vector2 Normalized()
        {
            return new Vector2(x, y) * (1 / Magnitude());
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x*b, a.y*b);
        }
        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x * 1f/b, a.y * 1f/b);
        }
        public override string ToString()
        {
            return "x " + x + " y " + y;
        }
        public static Vector2 RotateVector(Vector2 v, double angle)
        {
            double len = v.Magnitude();

            if (len == 0)
                return v;

            double refAngle = Get2DAngle(v, new Vector2(1, 0));

            float dx = (float)(len * Math.Cos((refAngle + angle) * Math.PI / 180.0));
            float dy = (float)(-len * Math.Sin((refAngle + angle) * Math.PI / 180.0));

            return new Vector2(dx, dy);
        }
        public static double Get2DAngle(Vector2 a, Vector2 b)
        {
            double cosine = (a.x * b.x + a.y * b.y) / (a.Magnitude() * b.Magnitude());

            if (cosine > 1)
                cosine = 1;
            else if (cosine < -1)
                cosine = -1;

            if ((a.x * b.y - a.y * b.x) < 0)
                return -Math.Acos(cosine) * 180.0 / Math.PI;
            else
                return Math.Acos(cosine) * 180.0 / Math.PI;
        }

    }

}
