using CanvasDrawing.Game;
using CanvasDrawing.UtalEngine2D_2023_1;
using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDrawing
{
    public partial class Form1 : Form
    {
        Image testImagen;        


        Frame Sven;
        Frame Sven2;
        // Here we find DeltaTime in while loop

        public Form1()
        {
            testImagen = global::CanvasDrawing.Properties.Resources.SmallSven;
            Image Mix = global::CanvasDrawing.Properties.Resources.Mix_color;

            Image Grass = Properties.Resources.Grass;
            Image Road = Properties.Resources.Road;
            //Sven2 = new Frame(testImagen, new Vector2(100, 100));
            Random rand = new Random();
            /*for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (rand.Next(10) > 5) {
                        new BackgroundElement(Grass, new Vector2(50, 50), i * 50 + 25, j * 50 + 25);
                    }
                    else
                    {
                        new BackgroundElement(Road, new Vector2(50, 50), i * 50 + 25, j * 50 + 25);
                    }
                }
            }
            for (int i = 0; i < 12; i++)
            {
                new Wall(Properties.Resources.SmallSven, new Vector2(50, 50), i * 50 - 15 + 25, 500 + i * 5);
            }
            for (int i = 0; i < 12; i++)
            {
                new Wall(Properties.Resources.SmallSven, new Vector2(50, 50), i * 50 - 15 + 25, 100 + i * 5);
            }
           
            Wall wall = new Wall(Properties.Resources.SmallSven, new Vector2(50, 50), 225, 225);
            wall.rigidbody.isStatic = true;
            */
            for (int i =4; i < 5; i++)
            {
                Frame fr = new Frame(i, testImagen, new Vector2(50, 50), i * 50 + 175, 75 + i * 50);
                fr.rigidbody.Velocity = new Vector2(2, 0);
            }

            GameObject MixGO = new GameObject(Mix, new Vector2(Mix.Width / 10, Mix.Height / 10), true, true, 50, 300);
            MixGO.rigidbody.isStatic = true;

            GameObject MixGO2 = new GameObject(Mix, new Vector2(Mix.Width / 10, Mix.Height / 10), true, true, 550, 330);
            MixGO2.rigidbody.isStatic = true;

            GameObject MixGO3 = new GameObject(Mix, new Vector2(Mix.Width / 2, Mix.Height / 10), true, true, 50, 630);
            MixGO3.rigidbody.isStatic = true;
            //Sven2.transform.position = new Vector2(0, 250);
            DoubleBuffered = true;

            //new GameObject(global::CanvasDrawing.Properties.Resources.Directorio_Sven, new Vector2(50, 50), true, true, 300, 250);


            new PlayerFrame(1, testImagen, new Vector2(50, 50), 200, 175);
            InitializeComponent();
            GameEngine.MainCamera.xSize = GameEngine.MainCamera.ySize = 600;
            GameEngine.InitEngine(this);
            GameEngine.MainCamera.scale = 1f;

            PhysicsEngine.gravity = new Vector2(0f, 10);
            
            GameEngine.MainCamera.Position = new Vector2(300, 300);
            new FrameManager();
            //new UtalText("Hola", 10, 10, Color.Black);
            new TimerBoard();



        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //GameEngine.MainCamera.xSize = Width;
            //GameEngine.MainCamera.ySize = Height;
        }
    }
}
