using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class FrameManager: EmptyUpdatable
    {
        public static List<Frame> AllFrames = new List<Frame>();
        public static int selectedIndex;
        public static FrameManager Instance;
        public override void Update()
        {
            if(GameEngine.KeyUp(System.Windows.Forms.Keys.C)){
                selectedIndex++;
                Console.WriteLine("Key detected");
                if (AllFrames.Count >= selectedIndex)
                {
                    selectedIndex %= AllFrames.Count;
                }
                AllFrames[selectedIndex].myCamera.xSize = GameEngine.MainCamera.xSize;
                AllFrames[selectedIndex].myCamera.ySize = GameEngine.MainCamera.ySize;
                GameEngine.MainCamera = AllFrames[selectedIndex].myCamera;
                
            }
        }
    }
}
