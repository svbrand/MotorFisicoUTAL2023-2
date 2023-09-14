using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class GameObjectManager
    {
        public static List<GameObject> AllNewGameObjects = new List<GameObject>();
        public static List<GameObject> AllGameObjects = new List<GameObject>();
        public static List<GameObject> AllDeadGameObjects = new List<GameObject>();
        public static List<EmptyUpdatable> AllNewEmptyUpdatables = new List<EmptyUpdatable>();
        public static List<EmptyUpdatable> AllEmptyUpdatables = new List<EmptyUpdatable>();
        public static List<EmptyUpdatable> AllDeadEmptyUpdatables = new List<EmptyUpdatable>();
        public static List<UtalText> AllText = new List<UtalText>();
        public static List<UtalText> AllDeadText = new List<UtalText>();

        public static void Update()
        {
            foreach(GameObject go in AllNewGameObjects)
            {
                AllGameObjects.Add(go);
                go.Start();
            }
            AllNewGameObjects = new List<GameObject>();
            foreach(GameObject go in AllGameObjects)
            {
                go.Update();
            }
            foreach(EmptyUpdatable eu in AllEmptyUpdatables)
            {
                eu.Update();
            }
            
        }
        public static void DeadUpdate()
        {
            foreach (GameObject go in GameObjectManager.AllDeadGameObjects)
            {
                GameObjectManager.AllGameObjects.Remove(go);
                go.OnDestroy();
            }
            GameObjectManager.AllDeadGameObjects = new List<GameObject>();
            foreach(UtalText text in GameObjectManager.AllDeadText)
            {
                AllText.Remove(text);
            }
            AllDeadText = new List<UtalText>();
            foreach(EmptyUpdatable eu in AllDeadEmptyUpdatables)
            {
                AllEmptyUpdatables.Remove(eu);
            }
            AllDeadEmptyUpdatables = new List<EmptyUpdatable>();
        }
        /// <summary>
        /// Creates a new Image containing the same image only rotated
        /// </summary>
        /// <param name=""image"">The <see cref=""System.Drawing.Image"/"> to rotate
        /// <param name=""offset"">The position to rotate from.
        /// <param name=""angle"">The amount to rotate the image, clockwise, in degrees
        /// <returns>A new <see cref=""System.Drawing.Bitmap"/"> of the same size rotated.</see>
        /// <exception cref=""System.ArgumentNullException"">Thrown if <see cref=""image"/"> 
        /// is null.</see>
        /// from https://stackoverflow.com/questions/2163829/how-do-i-rotate-a-picture-in-winforms
        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }
    }
}
