using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modulus2D.Network
{
    /// <summary>
    /// Client-specific networking system
    /// </summary>
    public class ClientSystem : NetSystem
    {
        public event NetUpdate UpdateReceived;

        private NetClient client;
        private NetIncomingMessage message;

        public ClientSystem(string host, int port) : base()
        {
            NetPeerConfiguration config = new NetPeerConfiguration(Identifier);
            client = new NetClient(config);
            client.Start();

            client.Connect(host, port);
        }

        public override void Update(float deltaTime)
        {
            while ((message = client.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        PacketType type = (PacketType)message.ReadByte();

                        switch (type)
                        {
                            case PacketType.Update:
                                {
                                    uint count = message.ReadUInt32();

                                    for (int i = 0; i < count; i++)
                                    {
                                        uint id = message.ReadUInt32();

                                        if (networkedEntities.TryGetValue(id, out Entity entity))
                                        {
                                            netComponents.Get(entity).Read(message);
                                        }
                                    }

                                    UpdateReceived?.Invoke((float)stopwatch.Elapsed.TotalSeconds);
                                    stopwatch.Restart();

                                    break;
                                }

                            case PacketType.Event:
                                {
                                    string name = message.ReadString();

                                    MemoryStream stream = new MemoryStream(message.ReadBytes(message.ReadInt32()));
                                    object[] args = (object[])formatter.Deserialize(stream);

                                    netEvents[name](args);
                                    break;
                                }

                            case PacketType.Create:
                                {
                                    string name = message.ReadString();
                                    uint id = message.ReadUInt32();

                                    MemoryStream stream = new MemoryStream(message.ReadBytes(message.ReadInt32()));
                                    object[] args = (object[])formatter.Deserialize(stream);
                                    
                                    if (creators.TryGetValue(name, out NetCreate creator))
                                    {
                                        // Create entity
                                        Entity entity = World.Create();
                                        NetComponent network = new NetComponent(id);
                                        entity.AddComponent(network);
                                        
                                        // Add to networked entities
                                        networkedEntities.Add(network.Id, entity);

                                        creator(entity, args);
                                    }

                                    break;
                                }

                            case PacketType.Remove:
                                {
                                    uint id = message.ReadUInt32();

                                    if (networkedEntities.TryGetValue(id, out Entity entity))
                                    {
                                        entity.Destroy();
                                        networkedEntities.Remove(id);
                                    }
                                    
                                    break;
                                }

                            case PacketType.Buffer:
                                {
                                    int count = message.ReadInt32();

                                    Console.WriteLine(count);

                                    for(int i = 0; i < count; i++)
                                    {
                                        string name = message.ReadString();
                                        uint id = message.ReadUInt32();

                                        MemoryStream stream = new MemoryStream(message.ReadBytes(message.ReadInt32()));
                                        object[] args = (object[])formatter.Deserialize(stream);

                                        if (creators.TryGetValue(name, out NetCreate creator))
                                        {
                                            // Create entity
                                            Entity entity = World.Create();
                                            NetComponent network = new NetComponent(id);
                                            entity.AddComponent(network);

                                            // Add to networked entities
                                            if (networkedEntities.TryGetValue(id, out Entity spawned)) {
                                                Console.WriteLine(id + " Already here");
                                            } else
                                            {
                                                networkedEntities.Add(id, entity);
                                                creator(entity, args);
                                            }
                                        }
                                    }

                                    break;
                                }
                        }

                        break;

                    default:
                        break;
                }
            }

            accumulator += deltaTime;

            // Send update
            if (accumulator > updateTime)
            {
                NetOutgoingMessage message = client.CreateMessage();
                message.Write((byte)PacketType.Update);
                message.Write(networkedEntities.Count);

                foreach (Entity entity in networkedEntities.Values)
                {
                    netComponents.Get(entity).Write(message);
                }

                client.SendMessage(message, NetDeliveryMethod.UnreliableSequenced);

                accumulator = 0f;
            }
        }

        /// <summary>
        /// Register a networked event
        /// </summary>
        /// <param name="name"></param>
        /// <param name="netEvent"></param>
        public void RegisterEvent(string name, NetEvent netEvent)
        {
            netEvents.Add(name, netEvent);
        }

        /// <summary>
        /// Register an entity creator
        /// </summary>
        /// <param name="name"></param>
        /// <param name="creator"></param>
        public void RegisterEntity(string name, NetCreate creator)
        {
            creators.Add(name, creator);
        }

        /// <summary>
        /// Disconnect from the server
        /// </summary>
        public void Disconnect()
        {
            client.Disconnect(null);
        }
    }
}
