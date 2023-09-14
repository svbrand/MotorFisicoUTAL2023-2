using CanvasDrawing.UtalEngine2D_2023_1;
using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class PlayerFrame : Frame
    {
        public static PlayerFrame Instance;
        public float timerSlowDown = 0;
        public PlayerFrame(float Speed, Image newsprite, Vector2 newSize, float x = 0, float y = 0) : base(Speed, newsprite, newSize, x, y)
        {
            Instance = this;
            rigidbody.isStatic = true;
            rigidbody.colliders[0].isSolid = false;
        }
        public void SlowDownTime()
        {
            Time.timeScale = 0.3f;
            timerSlowDown = 2;
        }
        public override void OnCollisionEnter(GameObject other, Collision CollisionPoint)
        {
            //renderer.sprite.Dispose();
            Console.WriteLine("Choque");
            //GameEngine.Destroy(other);}
            
        }
        public override void OnCollisionStay(GameObject other)
        {
            if (GameEngine.KeyUp(System.Windows.Forms.Keys.Space))
            {
                 other.rigidbody.Velocity = transform.Right * 10;
            }
        }
        public override void Update()
        {
            if(timerSlowDown > 0)
            {
                timerSlowDown -= Time.realDeltaTime;
                if (timerSlowDown <= 0)
                {
                    Time.timeScale = 1;
                }
            }
            transform.rotation += Time.deltaTime*100;
            Vector2 textPosition = GameEngine.WorldToCameraPos(transform.position);
            float speed = 300 * Time.deltaTime;
            text.x = textPosition.x;
            text.y = textPosition.y;
            if (GameEngine.KeyPress(System.Windows.Forms.Keys.Oemplus)){
                GameEngine.MainCamera.scale += Time.deltaTime;
            }
            if (GameEngine.KeyPress(System.Windows.Forms.Keys.OemMinus))
            {
                GameEngine.MainCamera.scale -= Time.deltaTime;
            }
            //timerMove += Time.deltaTime;
            //if (timerMove >= 1)
            {
                //if (timerMove >= 2)
                //{
                    timerMove -= 1;
                //}
                if (GameEngine.KeyUp(System.Windows.Forms.Keys.Space))
                {
                    Bullet b = new Bullet(transform.Right, 50f, Properties.Resources.bala,
                        new Vector2(50, 50), transform.position.x, transform.position.y);
                    SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.piu);
                    simpleSound.Play();
                }
                Vector2 auxLastPos = transform.position;
                if (GameEngine.KeyPress(System.Windows.Forms.Keys.W))
                {
                    if (transform.position.y - 25 > 0)
                    {
                        transform.position.y -= speed;
                    }
                }
                if (GameEngine.KeyPress(System.Windows.Forms.Keys.S))
                {
                    if (transform.position.y + 25 < 600)
                    {
                        transform.position.y += speed;
                    }
                }              
            }
        }
    }
}
