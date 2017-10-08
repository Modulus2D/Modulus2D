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
    public class NetworkComponent : IComponent
    {
        private uint id;
        public uint Id { get => id; set => id = value; }

        private List<ITransmit> transmitters;
        private List<IReceive> receivers;

        public void AddTransmitter<T>(T transmitter) where T : ITransmit
        {
            transmitters.Add(transmitter);
        }

        public void AddReceiver<T>(T receiver) where T : IReceive
        {
            receivers.Add(receiver);
        }

        public List<IUpdate> Transmit()
        {
            List<IUpdate> updates = new List<IUpdate>();

            for (int i = 0; i < transmitters.Count; i++)
            {
                updates.Add(transmitters[i].Send());
            }

            return updates;
        }

        public void ReceiveUpdate(List<IUpdate> updates)
        {
            for (int i = 0; i < receivers.Count; i++)
            {
                receivers[i].Receive(updates[i]);
            }
        }
    }
}
