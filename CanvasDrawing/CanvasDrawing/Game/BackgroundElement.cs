using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class BackgroundElement : GameObject
    {
        public BackgroundElement(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize,false, false, xPos, yPos)
        {
        }
    }
}
