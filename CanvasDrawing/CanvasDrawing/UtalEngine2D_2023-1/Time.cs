using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class Time
    {
        static DateTime time1 = DateTime.Now;
        static DateTime time2 = DateTime.Now;
        public static double deltaTimeDouble = -1;
        public static float deltaTime = -1;
        public static float realDeltaTime = -1;
        public static float timeScale = 1;

        public static void UpdateDeltaTime()
        {
            // This is it, use it where you want, it is time between
            // two iterations of while loop
            time2 = DateTime.Now;
            deltaTimeDouble = (time2 - time1).TotalMilliseconds / 1000.0;
            realDeltaTime = (float)deltaTimeDouble;
            if (deltaTime > 0.2f)
            {
                deltaTime = 0.2f;
            }
            deltaTime = realDeltaTime * timeScale;
            //Console.WriteLine(deltaTime);  // *float* output {0,2493331}
            //Console.WriteLine(time2.Ticks - time1.Ticks); // *int* output {2493331}
            time1 = time2;
        }
    }
}
