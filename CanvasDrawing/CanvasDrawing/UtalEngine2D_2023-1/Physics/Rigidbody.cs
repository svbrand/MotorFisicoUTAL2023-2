using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class Rigidbody
    {
        public List<Rigidbody> CollisionStayList = new List<Rigidbody>();
        public Transform transform;
        public Vector2 lastPos;
        public List<Collider> colliders = new List<Collider>();
        public Vector2 Velocity;

        public bool isStatic = false;

        public Vector2 force = new Vector2(0, 0);
        public float mass = 1;
        public bool stopGravityThisFrame = false;


        public delegate void CollisionEnterDelegate(Object o, Collision collision);
        public delegate void CollisionDelegate(Object o);
        public CollisionEnterDelegate OnCollision;
        public CollisionDelegate OnCollisionStay;
        public CollisionDelegate OnCollisionExit;
        public delegate Object GetOnCollisionObjectDelegate();
        public GetOnCollisionObjectDelegate GetOnCollisionObject;

        public Rigidbody()
        {
            PhysicsEngine.allNewRigidbodies.Add(this);            
        }      
        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            lastPos = transform.position;
        }
        public void CreateCircleCollider(float radius)
        {
            colliders.Add(new CircleCollider(this, radius));
        }
        public void CreateRectCollider(Vector2 size)
        {
            colliders.Add(new RectCollider(this, size));
        }

        public void Update()
        {
            if (isStatic)
            {
                return;
            }
            Vector2 accel = force*(1/mass);
            force = new Vector2(0,0);
            Vector2 oldVelocity = Velocity;
            Velocity += accel;
            transform.position += (oldVelocity+Velocity)/2f * Time.deltaTime * PhysicsEngine.pixelsPerMeter;
        }

        public bool CheckCollision(Rigidbody otherRigid, out Collision collision)
        {
            collision = new Collision();
            foreach(Collider myC in colliders)
            {
                foreach(Collider otherC in otherRigid.colliders)
                {
                    if (myC.CheckCollision(otherC, out collision))
                    {
                        if (CollisionStayList.Contains(otherC.rigidbody))
                        {
                            OnCollisionStay?.Invoke(otherRigid.GetOnCollisionObject?.Invoke());
                            otherRigid.OnCollisionStay?.Invoke(GetOnCollisionObject?.Invoke());
                        }
                        else
                        {
                            CollisionStayList.Add(otherC.rigidbody);
                            otherRigid.CollisionStayList.Add(this);
                        }
                        if(myC.isSolid && otherC.isSolid)
                        {
                            bool checkSecondColl = false;
                            Vector2 toOtherDirection = collision.CollisionPoint - transform.position;
                            if (!otherC.rigidbody.isStatic)
                            {
                                checkSecondColl = true;
                                //otherC.rigidbody.transform.position = otherC.rigidbody.lastPos;
                                if (isStatic)
                                {
                                    CheckCollisionAgainstStatic(otherRigid, collision);                                    
                                    //otherC.rigidbody.Velocity = toOtherDirection.Normalized() * otherC.rigidbody.Velocity.Magnitude() * 0.9f;
                                }
                            }
                            if (!isStatic)
                            {
                                checkSecondColl = true;
                                //transform.position = lastPos;
                                if (otherC.rigidbody.isStatic)
                                {                                        
                                    //Velocity = new Vector2(0, 0) - toOtherDirection.Normalized() * Velocity.Magnitude() * 0.9f;                                       
                                    CheckCollisionAgainstStatic(this, collision);
                                }
                            }
                           if(!isStatic && !otherC.rigidbody.isStatic)
                            {
                                Velocity = new Vector2(0, 0) - toOtherDirection.Normalized() * Velocity.Magnitude() * (0.9f/2f);
                                otherC.rigidbody.force -= Velocity*mass;
                                otherC.rigidbody.Velocity = toOtherDirection.Normalized() * otherC.rigidbody.Velocity.Magnitude() * (0.9f/2f);
                                force -= otherC.rigidbody.Velocity*otherC.rigidbody.mass;
                            }
                            Collision TrashPoint;
                            if(checkSecondColl && myC.CheckCollision(otherC, out TrashPoint))
                            {
                                if (!otherC.rigidbody.isStatic)
                                {
                                    otherC.rigidbody.transform.position += toOtherDirection * Time.deltaTime*0.5f;
                                    otherC.rigidbody.force += toOtherDirection * Time.deltaTime * 0.5f;
                                }
                                if (!isStatic)
                                {
                                    transform.position -= toOtherDirection * Time.deltaTime*0.5f;
                                    force -= toOtherDirection * Time.deltaTime * 0.5f;
                                }
                            }

                        }
                        if (OnCollision != null && otherRigid.GetOnCollisionObject != null)
                        {
                            OnCollision(otherRigid.GetOnCollisionObject(), collision);                            
                        }
                        if(GetOnCollisionObject != null && otherRigid.OnCollision != null)
                        {
                            otherRigid.OnCollision(GetOnCollisionObject(), collision);
                        }
                        return true;
                    }
                    else
                    {
                        if (CollisionStayList.Contains(otherRigid))
                        {
                            OnCollisionExit?.Invoke(otherRigid.GetOnCollisionObject?.Invoke());
                            CollisionStayList.Remove(otherRigid);
                            otherRigid.OnCollisionExit?.Invoke(GetOnCollisionObject?.Invoke());
                            otherRigid.CollisionStayList.Remove(this);
                        }
                    }
                }
            }
            collision.CollisionPoint = new Vector2(0, 0);
            return false;
        }
        public void CheckCollisionAgainstStatic(Rigidbody rb1, Collision collision)
        {

            if (collision.CollisionPoint.x == 0 && collision.CollisionPoint.y == 0)
            {
                Console.WriteLine("Bad Collision Point");
                return;
            }         
            Vector2 toOtherDirection = collision.CollisionPoint - rb1.transform.position;
            Debug.WriteLine("Pos " + rb1.transform.position.x.ToString() + " - " + rb1.transform.position.y);
            Debug.WriteLine("Collision Point " + collision.CollisionPoint.x.ToString() + " - " + collision.CollisionPoint.y);
            Debug.WriteLine(toOtherDirection.x.ToString() + " - " + toOtherDirection.y);
            //Normal force
            if (rb1.Velocity.y * PhysicsEngine.gravity.y > 0)
            {
                //rb1.force -= PhysicsEngine.gravity * mass * Time.deltaTime;
                rb1.stopGravityThisFrame = true;
                if (rb1.Velocity.SquareMagnitude() < 1f)
                {
                    if (rb1.Velocity.y * PhysicsEngine.gravity.y > 0)
                    {
                        //rb1.stopGravityThisFrame = true;
                    }
                    return;
                }
            }
            if (toOtherDirection.y * rb1.Velocity.y > 0)// && Math.Abs(toOtherDirection.x) < Math.Abs(toOtherDirection.y))
            {
                rb1.Velocity.y *= -1f;
            }

            if (toOtherDirection.x * rb1.Velocity.x > 0 && Math.Abs(toOtherDirection.x) > Math.Abs(toOtherDirection.y))
            {
                rb1.Velocity.x *= -1f;

            }
           
        }
    }
}
