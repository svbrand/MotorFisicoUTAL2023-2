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
    public class Frame : GameObject
    {
        public static int LASTID = 0;
        public float Speed = 1;
        public float timerMove = 0;
        public int myId = 0;
        static Random rand;
        public Camera myCamera;
        public UtalText text;
        public Vector2 lastPos;
        public int vida = 3;
        public Frame(float Speed, Image newsprite, Vector2 newSize, float x = 0, float y = 0) : base(newsprite, newSize, x, y)
        {
            FrameManager.AllFrames.Add(this);
            myId = LASTID++;
            myCamera = new Camera();
            this.Speed = Speed;
            renderer.size = newSize;
            if (rand == null)
            {
                rand = new Random();
            }
            text = new UtalText(LASTID.ToString() + ":" + vida, 10, 10 * LASTID, Color.Black);
            Vector2 textPosition = GameEngine.WorldToCameraPos(transform.position);
            text.x = textPosition.x;
            text.y = textPosition.y;
            //lastPos = transform.position;

        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            GameEngine.Destroy(text);
        }
        public override void OnCollisionEnter(GameObject other, Collision collision)
        {

            if(other is Bullet)
            {
                /*Bullet b = other as Bullet;
                if (b.isEnemy)
                {
                    return;
                }
                vida--;
                PlayerFrame.Instance.SlowDownTime();
                text.drawString = LASTID.ToString() + ":" + vida;
                GameEngine.Destroy(other);
                if (vida <= 0)
                {
                    GameEngine.Destroy(this);
                    
                }*/
            }
            else
            {
                //Bullet b = new Bullet(new Vector2(1, 0), 0f, Properties.Resources.bala, new Vector2(10, 10), CollisionPoint.x, CollisionPoint.y);
            }
            //renderer.sprite.Dispose();
            /*GameEngine.Destroy(this);
            FrameManager.AllFrames.Remove(this);
            Bullet b = other as Bullet;
            if (b != null)
            {
                GameEngine.Destroy(b);
            }*/
        }
       
        public override void Update()
        {
            Vector2 textPosition = GameEngine.WorldToCameraPos(transform.position);
            text.x = textPosition.x;
            text.y = textPosition.y;           
            myCamera.Position = transform.position;

            if (GameEngine.KeyUp(System.Windows.Forms.Keys.E))
            {
                Vector2 dir = PlayerFrame.Instance.transform.position - transform.position;                
                Bullet b = new Bullet(dir.Normalized(), 50f, Properties.Resources.bala,
                        new Vector2(50, 50), transform.position.x, transform.position.y, true);
            }
            
        }
    }
}
