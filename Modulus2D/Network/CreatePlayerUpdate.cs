using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    [Serializable]
    class CreatePlayerUpdate : IUpdate
    {
        public uint id;
        public bool isMine = false;
    }
}
