using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    public class NetworkSystem : EntitySystem
    {
        private EntityFilter filter = new EntityFilter();

        public NetworkSystem()
        {
            filter.Add<NetworkComponent>();
        }
    }
}
