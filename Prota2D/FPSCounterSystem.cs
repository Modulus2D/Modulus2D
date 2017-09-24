using Prota2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prota2D
{
    public class FPSCounterSystem : IEntitySystem
    {
        private float averageFps = 0f;
        private float totalTicks = 0f;
        private float printTimer = 0f;

        public float printFrequency = 1f;

        public void Update(EntityWorld world, float deltaTime)
        {
            totalTicks += 1f;
            printTimer += deltaTime;
            
            averageFps = averageFps * (totalTicks - 1) / totalTicks + 1f / (deltaTime * totalTicks);
            
            Console.WriteLine("FPS: " + averageFps);
        }
    }
}
