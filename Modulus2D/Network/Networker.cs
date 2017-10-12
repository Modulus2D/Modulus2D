using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    public delegate void NetworkEvent(IUpdate update);
    public delegate void NetworkUpdate(UpdatePacket packet);
    public delegate void Connect(NetConnection connection);
    public delegate void Disconnect();

    // TODO: Better name?
    public class Networker
    {
        public event Connect Connect;
        public event Disconnect Disconnect;
        public event NetworkUpdate Update;

        NetPeer peer;
        NetIncomingMessage message;
        BinaryFormatter formatter = new BinaryFormatter();

        private Dictionary<string, NetworkEvent> events = new Dictionary<string, NetworkEvent>();

        public Networker(NetPeer peer)
        {
            this.peer = peer;
        }

        public void ReadMessages()
        {
            while ((message = peer.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        // Get data type
                        switch (message.ReadByte())
                        {
                            case (byte)DataType.Update:
                                MemoryStream updateStream = new MemoryStream(message.ReadBytes(message.LengthBytes - 1));
                                Update((UpdatePacket)formatter.Deserialize(updateStream));

                                break;
                            case (byte)DataType.Event:
                                MemoryStream eventStream = new MemoryStream(message.ReadBytes(message.LengthBytes - 1));
                                EventPacket eventPacket = (EventPacket) formatter.Deserialize(eventStream);

                                if (events.TryGetValue(eventPacket.name, out NetworkEvent listener))
                                {
                                    listener(eventPacket.update);
                                } else
                                {
                                    Console.WriteLine("Unknown event received: " + eventPacket.name);
                                }

                                break;
                        }

                        break;

                    case NetIncomingMessageType.StatusChanged:
                        switch (message.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                Connect(message.SenderConnection);
                                break;
                            case NetConnectionStatus.Disconnected:
                                Disconnect();
                                break;
                        }
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        Console.WriteLine(message.ReadString());
                        break;

                    default:
                        // Console.WriteLine("Unhandled message with type: " + message.MessageType);
                        break;
                }

                peer.Recycle(message);
            }
        }

        public void RegisterEvent(string name, NetworkEvent listener)
        {
            events.Add(name, listener);
        }

        public void UnregisterEvent(string name)
        {
            events.Remove(name);
        }

        public NetOutgoingMessage CreateEvent(string name, IUpdate update)
        {
            NetOutgoingMessage message = peer.CreateMessage();

            MemoryStream stream = new MemoryStream();

            // Write data type
            message.Write((byte)DataType.Event);

            EventPacket packet = new EventPacket()
            {
                name = name,
                update = update
            };

            // Write packet
            formatter.Serialize(stream, packet);
            message.Write(stream.ToArray());

            return message;
        }

        public NetOutgoingMessage CreateUpdate(UpdatePacket packet)
        {
            NetOutgoingMessage message = peer.CreateMessage();

            MemoryStream stream = new MemoryStream();

            // Write data type
            message.Write((byte)DataType.Update);

            // Write packet
            formatter.Serialize(stream, packet);
            message.Write(stream.ToArray());

            return message;
        }
    }
}
