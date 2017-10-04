using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Core
{
    public class FPSCounterSystem : EntitySystem
    {
        private float averageFps = 0f;
        private float totalTicks = 0f;
        private float printTimer = 0f;

        public float printFrequency = 1f;
        
        public override void Update(EntityWorld world, float deltaTime)
        {
            totalTicks += 1f;
            printTimer += deltaTime;
            
            averageFps = averageFps * (totalTicks - 1) / totalTicks + 1f / (deltaTime * totalTicks);
            
            if (printTimer > printFrequency)
            {
                Console.WriteLine("FPS: " + averageFps);

                printTimer = 0f;
            }
        }
    }
}
