using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    
    public abstract class Collider
    {
        public Rigidbody rigidbody;
        public bool isSolid = true;
        public Collider(Rigidbody rigidbody)
        {
            this.rigidbody = rigidbody;
        }
        public abstract bool CheckCollision(Collider other, out Collision collision);

    }
}
