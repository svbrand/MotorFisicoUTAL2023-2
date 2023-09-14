using CanvasDrawing.UtalEngine2D_2023_1;
using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class Bullet : GameObject
    {
        public Vector2 Direction;
        public float timeToDie = 5f;
        public bool isEnemy = false;
        public Image[] animationImages;
        public int indexAnim;
        public float timeBTFrames = 0.2f;
        public float animTimer = 0.2f;
        public Bullet(Vector2 dir, float force, Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0, bool isEnemy = false) : base(newSprite, newSize, true, false,xPos, yPos)
        {
            this.isEnemy = isEnemy;
            //rigidbody.colliders[0].isSolid = false;
            //rigidbody.isStatic = true;
            this.Direction = dir;
            //this.Speed = force;
            rigidbody.force += dir*force;
            this.rigidbody.mass = 10;
            animationImages = new Image[5];
            animationImages[0] = newSprite;
            animationImages[1] = Properties.Resources.bala_01;
            animationImages[2] = Properties.Resources.bala_02;
            animationImages[3] = Properties.Resources.bala_03;
            animationImages[4] = Properties.Resources.bala_04;
        }
        public override void OnCollisionEnter(GameObject other, Collision collision)
        {
            new GameObject(Properties.Resources.SmallSven, new Vector2(10, 10), false, false, collision.CollisionPoint.x, collision.CollisionPoint.y, 2f);            
        }

        public override void Update()
        {
            base.Update();
            animTimer -= Time.deltaTime;
            if (animTimer <= 0)
            {
                indexAnim++;
                indexAnim %= animationImages.Length;
                renderer.rotatedSprite = animationImages[indexAnim];
                animTimer = timeBTFrames;
            }


            //transform.position += Direction * Speed * Time.deltaTime;
            
            /*if (timeToDie > 0)
            {
                timeToDie -= Time.deltaTime;
                if (timeToDie <= 0) {
                    GameEngine.Destroy(this);
                }
            }*/
        }
    }
}
