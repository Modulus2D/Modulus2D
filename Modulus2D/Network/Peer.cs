using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Network
{
    public delegate void NetEvent(IUpdate update);
    public delegate void NetUpdate(UpdatePacket packet);
    public delegate void NetAdd(Dictionary<uint, uint> builders);
    public delegate void NetRemove(List<uint> entities);
    public delegate void NetConnect(NetConnection connection);
    public delegate void NetDisconnect(NetConnection connection);

    /// <summary>
    /// Basic networking class, neither client nor server
    /// </summary>
    public class Peer
    {
        public event NetConnect Connect;
        public event NetDisconnect Disconnect;
        public event NetUpdate Update;
        public event NetAdd Add;
        public event NetRemove Remove;

        private NetPeer peer;
        private NetIncomingMessage message;
        private BinaryFormatter formatter;
        private Dictionary<string, NetEvent> events;

        public Peer(NetPeer peer)
        {
            this.peer = peer;

            formatter = new BinaryFormatter();
            events = new Dictionary<string, NetEvent>();
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
                            case (byte)PacketType.Update:
                                {

                                    MemoryStream stream = new MemoryStream(message.ReadBytes(message.LengthBytes - 1));
                                    Update((UpdatePacket)formatter.Deserialize(stream));

                                }
                                break;

                            case (byte)PacketType.Event:
                                {
                                    MemoryStream stream = new MemoryStream(message.ReadBytes(message.LengthBytes - 1));
                                    EventPacket packet = (EventPacket)formatter.Deserialize(stream);

                                    if (events.TryGetValue(packet.name, out NetEvent listener))
                                    {
                                        listener(packet.update);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unknown event received: " + packet.name);
                                    }
                                }
                                break;

                            case (byte)PacketType.Add:
                                {
                                    MemoryStream stream = new MemoryStream(message.ReadBytes(message.LengthBytes - 1));
                                    AddPacket packet = (AddPacket)formatter.Deserialize(stream);
                                    Add(packet.builders);
                                }
                                break;

                            case (byte)PacketType.Remove:
                                {
                                    MemoryStream stream = new MemoryStream(message.ReadBytes(message.LengthBytes - 1));
                                    RemovePacket packet = (RemovePacket)formatter.Deserialize(stream);
                                    Remove(packet.entities);
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
                                Disconnect(message.SenderConnection);
                                break;
                        }
                        break;

                    case NetIncomingMessageType.DebugMessage:
                        Console.WriteLine("Network Debug: " + message.ReadString());
                        break;

                    default:
                        break;
                }

                peer.Recycle(message);
            }
        }

        public void RegisterEvent(string name, NetEvent listener)
        {
            events.Add(name, listener);
        }

        public void UnregisterEvent(string name)
        {
            events.Remove(name);
        }

        public NetOutgoingMessage CreatePacket(Packet packet, PacketType type)
        {
            NetOutgoingMessage message = peer.CreateMessage();

            MemoryStream stream = new MemoryStream();

            // Write data type
            message.Write((byte)type);

            // Write packet
            formatter.Serialize(stream, packet);
            message.Write(stream.ToArray());

            return message;
        }
    }
}
