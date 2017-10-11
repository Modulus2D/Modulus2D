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
    // TODO: Better name?
    public class Networker
    {
        NetPeer peer;
        NetIncomingMessage message;
        BinaryFormatter formatter = new BinaryFormatter();

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
                                UpdatePacket packet = (UpdatePacket)formatter.Deserialize(updateStream);

                                break;
                            case (byte)DataType.Event:
                                MemoryStream eventStream = new MemoryStream(message.ReadBytes(message.LengthBytes - 1));
                                EventPacket evt = (EventPacket) formatter.Deserialize(eventStream);

                                break;
                        }

                        break;

                    case NetIncomingMessageType.StatusChanged:
                        switch (message.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                Console.WriteLine("Connection");
                                break;
                            case NetConnectionStatus.Disconnected:
                                Console.WriteLine("Disconnection");
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

        public NetOutgoingMessage CreateUpdate(UpdatePacket packet)
        {
            NetOutgoingMessage message = peer.CreateMessage();

            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            // Write data type
            message.Write((byte)DataType.Update);

            // Write packet
            formatter.Serialize(stream, packet);
            message.Write(stream.ToArray());

            return message;
        }
    }
}
