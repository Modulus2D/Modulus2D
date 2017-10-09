using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modulus2D.Network
{
    public class ServerSystem : EntitySystem
    {
        NetServer server;
        NetIncomingMessage message;

        public ServerSystem(int port)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Network")
            {
                Port = port
            };

            server = new NetServer(config);
            server.Start();
        }
        
        public override void Update(float deltaTime)
        {
            while ((message = server.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
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
                        Console.WriteLine("unhandled message with type: "
                            + message.MessageType);
                        break;
                }

                server.Recycle(message);
            }
        }
    }
}
