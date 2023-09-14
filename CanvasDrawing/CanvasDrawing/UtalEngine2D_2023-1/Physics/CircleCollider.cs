using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class CircleCollider : Collider
    {
        public float radius;
        public CircleCollider(Rigidbody rigidbody, float radius) : base(rigidbody)
        {
            this.radius = radius;
        }

        public override bool CheckCollision(Collider other, out Collision collision)
        {
            collision = new Collision();
            CircleCollider otherC = other as CircleCollider;
            if(otherC != null)
            {
                Vector2 distVector = otherC.rigidbody.transform.position - rigidbody.transform.position;
                Vector2 dist2Vector = rigidbody.transform.position - otherC.rigidbody.transform.position;
                float squareDist = distVector.x * distVector.x + distVector.y * distVector.y;
                if (squareDist < (radius + otherC.radius) * (radius + otherC.radius))
                {
                    Vector2 CollisionPoint1 = (distVector * (1 / (otherC.radius + radius))) * radius + rigidbody.transform.position;
                    Vector2 CollisionPoint2 = (dist2Vector * (1 / (otherC.radius + radius))) * otherC.radius + otherC.rigidbody.transform.position;
                    collision.CollisionPoint = (CollisionPoint1 + CollisionPoint2) * 0.5f;
                    rigidbody.transform.position += (collision.CollisionPoint - CollisionPoint1);
                    otherC.rigidbody.transform.position += (collision.CollisionPoint - CollisionPoint2);
                    return true;
                }

            }
            RectCollider otherRC = other as RectCollider;
            if(otherRC != null)
            {
                return otherRC.CheckCollision(this, out collision);
            }
            collision.CollisionPoint = new Vector2(0, 0);
            return false;
        }
        public bool CheckPointInCircle(Vector2 point, out Vector2 closestPointBorder)
        {
            Vector2 distVector = point - rigidbody.transform.position;
            float squareDist = distVector.x * distVector.x + distVector.y * distVector.y;
            if (squareDist < radius * radius)
            {
                closestPointBorder = rigidbody.transform.position + distVector.Normalized() * radius;
                return true;
            }
            closestPointBorder = new Vector2(0, 0);
            return false;
        }
    }
}
