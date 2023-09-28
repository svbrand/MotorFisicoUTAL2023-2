using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class RectCollider : Collider
    {
        public Vector2 size;        
        public RectCollider(Rigidbody rigidbody, Vector2 size) : base(rigidbody)
        {
            this.size = size;
        }

        public override bool CheckCollision(Collider other, out Collision collision)
        {
            Vector2 sizeScaled = new Vector2(this.size.x * rigidbody.transform.scale.x, this.size.y * rigidbody.transform.scale.y);
            collision = new Collision();
            Vector2 closestPoint;
            CircleCollider otherC = other as CircleCollider;
            if (otherC != null)
            {
                //check if corners are in circle
                {
                    Vector2[] corners = new Vector2[4];
                    Vector2 pos = rigidbody.transform.position;
                    corners[0] = new Vector2(pos.x - sizeScaled.x / 2, pos.y - sizeScaled.y / 2);
                    corners[1] = new Vector2(pos.x + sizeScaled.x / 2, pos.y - sizeScaled.y / 2);
                    corners[2] = new Vector2(pos.x - sizeScaled.x / 2, pos.y + sizeScaled.y / 2);
                    corners[3] = new Vector2(pos.x + sizeScaled.x / 2, pos.y + sizeScaled.y / 2);
                    foreach (Vector2 v in corners)
                    {
                        Vector2 ClosestPointToBorder;
                        /*if (otherC.CheckPointInCircle(v, out ClosestPointToBorder))
                        {
                            CollisionPoint = (v + ClosestPointToBorder) * 0.5f;
                            if (!otherC.rigidbody.isStatic)
                            {
                                otherC.rigidbody.transform.position += CollisionPoint-v;
                            }
                            if (!rigidbody.isStatic)
                            {
                                rigidbody.transform.position += CollisionPoint-ClosestPointToBorder;
                            }
                            return true;
                        }*/
                    }
                }

                //check distance from boxed circle inside rectangle
                {
                    Vector2[] corners = new Vector2[4];
                    Vector2 pos = otherC.rigidbody.transform.position;
                    corners[0] = new Vector2(pos.x, pos.y - otherC.radius);
                    corners[1] = new Vector2(pos.x + otherC.radius, pos.y);
                    corners[2] = new Vector2(pos.x, pos.y + otherC.radius);
                    corners[3] = new Vector2(pos.x - otherC.radius, pos.y);
                    List<Collision> collisionCandidates = new List<Collision>();
                    foreach (Vector2 v in corners)
                    {
                        Collision localCollision = new Collision();
                        if (CheckPointInRec(v, out closestPoint))
                        {
                            //find closest border, it's the closestPoint
                            localCollision.CollisionPoint = (closestPoint+v)*0.5f;
                            if (!otherC.rigidbody.isStatic)
                            {
                                localCollision.OffsetVectorOther = (closestPoint - v);
                                localCollision.CollisionPoint = closestPoint;
                                if (otherC.isSolid && isSolid)
                                {
                                    otherC.rigidbody.transform.position += localCollision.OffsetVectorOther;
                                }
                            }
                            if (!rigidbody.isStatic)
                            {
                                localCollision.OffsetVector = v - closestPoint;
                                if (isSolid)
                                {
                                    rigidbody.transform.position += localCollision.OffsetVector;
                                }
                            }
                            collisionCandidates.Add(localCollision);
                            //return true;
                        }
                    }
                    if (collisionCandidates.Count > 0)
                    {
                        //No se cual elegir
                        collision = collisionCandidates[0];
                        //Console.WriteLine("Colisiones Detectadas " + collisionCandidates.Count);
                        return true;
                    }
                }

                //check stranger cases
            }

            RectCollider otherRC = other as RectCollider;
            if (otherRC != null)
            {
                Vector2[] corners = new Vector2[4];
                Vector2 pos = rigidbody.transform.position;
                corners[0] = new Vector2(pos.x - sizeScaled.x / 2, pos.y - sizeScaled.y / 2);
                corners[1] = new Vector2(pos.x + sizeScaled.x / 2, pos.y - sizeScaled.y / 2);
                corners[2] = new Vector2(pos.x - sizeScaled.x / 2, pos.y + sizeScaled.y / 2);
                corners[3] = new Vector2(pos.x + sizeScaled.x / 2, pos.y + sizeScaled.y / 2);
                foreach (Vector2 v in corners)
                {
                    if (otherRC.CheckPointInRec(v, out closestPoint))
                    {
                        collision.CollisionPoint = (closestPoint+v)*0.5f;
                        return true;
                    }
                }
                pos = otherRC.rigidbody.transform.position;
                corners[0] = new Vector2(pos.x - otherRC.size.x / 2, pos.y - otherRC.size.y / 2);
                corners[1] = new Vector2(pos.x + otherRC.size.x / 2, pos.y - otherRC.size.y / 2);
                corners[2] = new Vector2(pos.x - otherRC.size.x / 2, pos.y + otherRC.size.y / 2);
                corners[3] = new Vector2(pos.x + otherRC.size.x / 2, pos.y + otherRC.size.y / 2);
                foreach(Vector2 v in corners)
                {
                    if (CheckPointInRec(v, out closestPoint))
                    {
                        collision.CollisionPoint = (v + closestPoint) * 0.5f;
                        if (!otherRC.rigidbody.isStatic) { 
                            collision.OffsetVector = collision.CollisionPoint - v;
                            otherRC.rigidbody.transform.position += collision.OffsetVector;
                        }
                        if (rigidbody.isStatic)
                        {
                            collision.OffsetVectorOther = collision.CollisionPoint - closestPoint;
                            if (isSolid)
                            {
                                rigidbody.transform.position += collision.OffsetVectorOther;
                            }
                        }
                        return true;
                    }
                }
            }
            collision.CollisionPoint = new Vector2(0, 0);
            return false;
        }
        public bool CheckPointInRec(Vector2 point, out Vector2 pointMovedToClosestBorder)
        {
            Vector2 sizeScaled = new Vector2(this.size.x * rigidbody.transform.scale.x, this.size.y * rigidbody.transform.scale.y);
            Vector2 center = rigidbody.transform.position;
            if(point.x > center.x-sizeScaled.x/2 && point.x < center.x + sizeScaled.x / 2 &&
               point.y > center.y - sizeScaled.y / 2 && point.y < center.y + sizeScaled.y / 2)
            {
                List<Vector2> borderPoints = new List<Vector2>
                {
                    new Vector2(center.x - sizeScaled.x / 2, point.y),
                    new Vector2(center.x + sizeScaled.x / 2, point.y),
                    new Vector2(point.x, center.y + sizeScaled.y / 2),
                    new Vector2(point.x, center.y - sizeScaled.y / 2)
                };
                float distancesSquared = 0;
                Vector2 bp1 = borderPoints[0];
                pointMovedToClosestBorder = bp1;
                distancesSquared = (bp1.x - point.x) * (bp1.x - point.x) + (bp1.y - point.y) * (bp1.y - point.y);
                foreach (Vector2 bp in borderPoints)
                {
                    float newMinCandidate = (bp.x - point.x) * (bp.x - point.x) + (bp.y - point.y) * (bp.y - point.y);
                    if (newMinCandidate < distancesSquared)
                    {
                        distancesSquared = newMinCandidate;
                        pointMovedToClosestBorder = bp;
                    }
                }
                return true;
            }
            pointMovedToClosestBorder = new Vector2(0, 0);
            return false;
        }
    }
}
