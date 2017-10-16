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
        private uint builderId;

        private List<ITransmit> transmitters;
        private List<IReceive> receivers;

        /// <summary>
        /// Network ID to uniquely identify entity
        /// </summary>
        public uint Id { get => id; set => id = value; }

        /// <summary>
        /// Entity builder ID stored for initialization buffer
        /// </summary>
        public uint BuilderId { get => builderId; set => builderId = value; }

        public NetworkComponent()
        {
            transmitters = new List<ITransmit>();
            receivers = new List<IReceive>();
        }

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
            int max = Math.Min(receivers.Count, updates.Count);

            for (int i = 0; i < max; i++)
            {
                receivers[i].Receive(updates[i]);
            }
        }
    }
}
