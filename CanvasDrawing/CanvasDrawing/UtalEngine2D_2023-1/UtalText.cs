using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class UtalText
    {
        public string drawString = "Sample Text";
        public float x = 150.0F;
        public float y = 50.0F;
        System.Drawing.Font drawFont;
        System.Drawing.SolidBrush drawBrush;
        System.Drawing.StringFormat drawFormat;
        Color DefaultColor = Color.Black;

        public UtalText(string text, float x, float y, Font font = null)
        {
            Init(text, x, y, Color.Black, font);
        }
        public UtalText(string text, float x, float y, Color color, Font font = null)
        {
            Init(text, x, y, color, font);
        }
        public void Init(string text, float x, float y, Color color, Font font = null)
        {
            if (font == null)
            {
                font = new Font("Arial", 16);
            }
            drawString = text;
            this.x = x;
            this.y = y;
            GameObjectManager.AllText.Add(this);
            drawFont = new System.Drawing.Font("Arial", 16);
            drawBrush = new System.Drawing.SolidBrush(color);
            drawFormat = new System.Drawing.StringFormat();

        }
        public void DrawString(Graphics graphics)
        {
            System.Drawing.Graphics formGraphics = graphics;//this.CreateGraphics();                             
            
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }
    }
}
