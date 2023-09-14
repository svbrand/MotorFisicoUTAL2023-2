using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public static class PhysicsEngine
    {
        public static List<Rigidbody> allNewRigidbodies = new List<Rigidbody>();
        public static List<Rigidbody> allRigidbodies = new List<Rigidbody>();
        public static List<Rigidbody> allDeadRigidbodies = new List<Rigidbody>();

        public static Vector2 gravity = new Vector2(0, 9.8f);

        public static float pixelsPerMeter = 50;

        public static void Destroy(Rigidbody rigidbody)
        {
            allDeadRigidbodies.Add(rigidbody);
        }
        public static void Update()
        {

            foreach(Rigidbody rb in allNewRigidbodies)
            {
                allRigidbodies.Add(rb);
            }
            foreach(Rigidbody rb in allRigidbodies)
            {
                if (!rb.stopGravityThisFrame)
                {
                    rb.force += gravity * rb.mass * Time.deltaTime;                    
                }              
                //rb.Velocity = rb.Velocity - rb.Velocity * 0.3f * Time.deltaTime; //Debieramos usar delta time para el roce
                rb.stopGravityThisFrame = false;
                rb.Update();

            }
            allNewRigidbodies = new List<Rigidbody>();

            foreach (Rigidbody rb in allDeadRigidbodies)
            {
                allRigidbodies.Remove(rb);
            }
            allDeadRigidbodies = new List<Rigidbody>();

            for (int i=0; i<allRigidbodies.Count; i++)
            {
                for(int j=i+1; j < allRigidbodies.Count; j++)
                {
                    Collision collision;
                    if(allRigidbodies[i].CheckCollision(allRigidbodies[j], out collision)){
                       /* Console.WriteLine("Choque");
                        Console.WriteLine(allRigidbodies[i].transform.position);
                        Console.WriteLine(allRigidbodies[j].transform.position);
                        Console.WriteLine("Punto Colisión "+ CollisionPoint);*/
                    }
                }
            }
            

            foreach(Rigidbody rb in allRigidbodies)
            {
                rb.lastPos = rb.transform.position;
            }
        }
    }
}
