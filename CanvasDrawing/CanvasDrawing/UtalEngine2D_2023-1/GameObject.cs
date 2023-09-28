using CanvasDrawing.UtalEngine2D_2023_1;
using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class GameObject
    {
        float deathTime = -1;
        bool mustDie = false;
        public class Renderer
        {
            public Image sprite;
            public Image rotatedSprite;
            public float lastRot;
            public Vector2 position;
            public Vector2 size;
            public float rotation;
        }

        public Rigidbody rigidbody;

        public Transform transform = new Transform();
        public Renderer renderer = new Renderer();
        public GameObject(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0, float deathTime = -1)
        {
            Init(newSprite, newSize, true, false, xPos, yPos, deathTime);
        }
        public GameObject(Image newSprite, Vector2 newSize, bool hasCollider = true, bool isRect = false, float xPos = 0, float yPos = 0, float deathTime = -1)
        {
            Init(newSprite, newSize, hasCollider, isRect, xPos, yPos, deathTime);
        }
        public void Init(Image newSprite, Vector2 newSize, bool hasCollider = true, bool isRect = false, float xPos = 0, float yPos = 0, float deathTime = -1)
        {
            if (deathTime > 0)
            {
                this.deathTime = deathTime;
                mustDie = true;
            }
            transform.position = new Vector2(xPos, yPos);
            if (hasCollider)
            {
                rigidbody = new Rigidbody();
                rigidbody.SetTransform(transform);
                if (isRect)
                {
                    rigidbody.CreateRectCollider(newSize);
                }
                else
                {
                    rigidbody.CreateCircleCollider(newSize.x / 2);
                }
                rigidbody.OnCollision = OnCollisionEnter;
                rigidbody.OnCollisionExit = OnCollisionExit;
                rigidbody.OnCollisionStay = OnCollisionStay;
                rigidbody.GetOnCollisionObject = GetOnCollision;
            }
            GameObjectManager.AllNewGameObjects.Add(this);
            renderer.sprite = newSprite;
            renderer.size = newSize;
            renderer.rotatedSprite = newSprite;
        }

        public void OnCollisionEnter(Object otherO, Collision collision)
        {
            GameObject otherGO = otherO as GameObject;
            OnCollisionEnter(otherGO, collision);
        }
        public void OnCollisionStay(Object otherO)
        {
            GameObject otherGO = otherO as GameObject;
            OnCollisionStay(otherGO);
        }
        public void OnCollisionExit(Object otherO)
        {
            GameObject otherGO = otherO as GameObject;
            OnCollisionExit(otherGO);
        }
        public Object GetOnCollision()
        {
            return this;
        }
        public virtual void OnCollisionEnter(GameObject other, Collision collision)
        {

        }

        public virtual void OnCollisionStay(GameObject other)
        {

        }
        public virtual void OnCollisionExit(GameObject other)
        {

        }
        public virtual void Start()
        {

        }
        public virtual void Update()
        {
            if (mustDie)
            {
                deathTime -= Time.deltaTime;
                if (deathTime < 0)
                {                    
                    GameEngine.Destroy(this);
                }
            }
        }

        public virtual void OnDestroy()
        {
            PhysicsEngine.Destroy(rigidbody);
        }
        public void Draw(Graphics graphics, Camera camera)
        {
            int xOffset = camera.xSize / 2;
            int yOffset = camera.ySize / 2;
            
            if(renderer == null)
            {
                return;
            }
            transform.rotation %= 360;
            renderer.rotation = transform.rotation;
            if(renderer.lastRot != renderer.rotation)
            {
                float rotate = renderer.rotation - renderer.lastRot;
                renderer.lastRot = renderer.rotation;
                Point center = new Point((int)((renderer.size.x*transform.scale.x) / 2), (int)(renderer.size.y * transform.scale.y) / 2);
                renderer.rotatedSprite = GameObjectManager.RotateImage(renderer.sprite, center, renderer.rotation);
            }
            if (!float.IsNaN(transform.position.x))
            {
                graphics.DrawImage(renderer.rotatedSprite,
                    (transform.position.x - camera.Position.x - renderer.size.x*transform.scale.x / 2) / camera.scale + xOffset,
                    (transform.position.y - camera.Position.y - renderer.size.y * transform.scale.y / 2) / camera.scale + yOffset,
                    renderer.size.x*transform.scale.x / camera.scale,
                    renderer.size.y * transform.scale.y / camera.scale);
            }
        }
    }
}
