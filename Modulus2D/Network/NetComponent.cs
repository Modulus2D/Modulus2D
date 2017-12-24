using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    /// <summary>
    /// A network component for receiving and transmitting information. Transmitters and receivers must be
    /// added in the same order on the server and the client.
    /// </summary>
    public class NetComponent : IComponent
    {
        private uint id;

        private List<INetView> receivers;
        private List<INetView> transmitters;

        /// <summary>
        /// Network ID to uniquely identify entity
        /// </summary>
        public uint Id { get => id; set => id = value; }

        public NetComponent(uint id)
        {
            this.id = id;

            receivers = new List<INetView>();
            transmitters = new List<INetView>();
        }
        
        public void AddReceiver(INetView receiver)
        {
            receivers.Add(receiver);
        }

        public void AddTransmitter(INetView transmitter)
        {
            transmitters.Add(transmitter);
        }

        public void Write(NetBuffer buffer)
        {
            buffer.Write(id);
            buffer.Write(transmitters.Count);

            for (int i = 0; i < transmitters.Count; i++)
            {
                transmitters[i].Transmit(buffer);
            }
        }

        public void Read(NetBuffer buffer)
        {
            int count = buffer.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                receivers[i].Receive(buffer);
            }
        }
    }
}