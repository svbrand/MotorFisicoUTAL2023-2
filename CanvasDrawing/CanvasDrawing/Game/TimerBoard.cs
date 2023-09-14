using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class TimerBoard : EmptyUpdatable
    {
        UtalText text = new UtalText("Timer", 0,0);
        public override void Update()
        {
            text.drawString = (1 / Time.deltaTime).ToString();
        }
    }
}
