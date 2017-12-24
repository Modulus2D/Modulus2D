using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modulus2D.Network
{
    /// <summary>
    /// Called when a player connects to the server
    /// </summary>
    /// <param name="connection"></param>
    public delegate void Connected(NetPlayer player);

    /// <summary>
    /// Called when a player disconnects from the server
    /// </summary>
    /// <param name="connection"></param>
    public delegate void Disconnected(NetPlayer player);

    /// <summary>
    /// Server-specific networking system
    /// </summary>
    public class ServerSystem : NetSystem
    {
        public event NetUpdate UpdateReceived;

        public event Connected Connected;
        public event Disconnected Disconnected;
        
        // Lidgren
        private NetServer server;
        private NetIncomingMessage message;
        
        // Networked player
        private Dictionary<NetConnection, NetPlayer> players;
        
        // Buffered entities
        private Dictionary<uint, BufferedEntity> bufferedEntities;
        
        // Current net ID
        private uint currentNetId = 0;

        public ServerSystem(int port) : base()
        {
            players = new Dictionary<NetConnection, NetPlayer>();
            bufferedEntities = new Dictionary<uint, BufferedEntity>();

            NetPeerConfiguration config = new NetPeerConfiguration(Identifier)
            {
                Port = port
            };

            server = new NetServer(config);
            server.Start();
        }

        public override void OnAdded()
        {
            base.OnAdded();

            World.AddRemovedListener<NetComponent>((entity) =>
            {
                Remove(netComponents.Get(entity).Id);
            });
        }

        public override void Update(float deltaTime)
        {
            while ((message = server.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        PacketType type = (PacketType)message.ReadByte();

                        switch (type)
                        {
                            case PacketType.Update:
                                uint count = message.ReadUInt32();

                                for(int i = 0; i < count; i++) {
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

                        break;

                    case NetIncomingMessageType.StatusChanged:
                        switch (message.SenderConnection.Status)
                        {
                            case NetConnectionStatus.Connected:
                                NetPlayer player = new NetPlayer()
                                {
                                    connection = message.SenderConnection
                                };
                                players.Add(message.SenderConnection, player);

                                SendBuffer(player);

                                Connected?.Invoke(player);
                                
                                break;
                            case NetConnectionStatus.Disconnected:
                                Disconnected?.Invoke(players[message.SenderConnection]);

                                break;
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
                NetOutgoingMessage message = server.CreateMessage();
                message.Write((byte)PacketType.Update);
                message.Write(networkedEntities.Count);

                foreach (Entity entity in networkedEntities.Values)
                {
                    netComponents.Get(entity).Write(message);
                }

                server.SendToAll(message, NetDeliveryMethod.UnreliableSequenced);

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
        /// Allocates a network ID for a new entity
        /// </summary>
        /// <returns></returns>
        public uint AllocateId()
        {
            currentNetId++;
            return (currentNetId - 1);
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
        /// Send an event to all players
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public void SendEvent(string name, params object[] args)
        {
            server.SendToAll(CreateEvent(name, args), NetDeliveryMethod.ReliableOrdered);
        }

        /// <summary>
        /// Send an event to a specific player
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public void SendEvent(NetPlayer player, string name, params object[] args)
        {
            server.SendMessage(CreateEvent(name, args), player.connection, NetDeliveryMethod.ReliableOrdered);
        }

        private NetOutgoingMessage CreateEvent(string name, params object[] args)
        {
            NetOutgoingMessage message = server.CreateMessage();

            message.Write((byte)PacketType.Event);

            message.Write(name);

            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, args);

            message.Write((int)stream.Length);
            message.Write(stream.ToArray());

            return message;
        }

        public Entity Create(string name, params object[] args)
        {
            // Create entity
            Entity entity = World.Create();
            NetComponent network = new NetComponent(AllocateId());
            Console.WriteLine(network.Id);
            entity.AddComponent(network);

            // Add to networked entities
            networkedEntities.Add(network.Id, entity);

            // Buffer entity
            bufferedEntities.Add(network.Id, new BufferedEntity
            {
                name = name,
                args = args
            });

            // Call creator
            creators[name](entity, args);

            // Send message to clients
            NetOutgoingMessage message = server.CreateMessage();

            message.Write((byte)PacketType.Create);

            message.Write(name);
            message.Write(network.Id);

            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, args);

            message.Write((int)stream.Length);
            message.Write(stream.ToArray());

            server.SendToAll(message, NetDeliveryMethod.ReliableOrdered);

            return entity;
        }

        public void Remove(uint id)
        {
            networkedEntities.Remove(id);

            bufferedEntities.Remove(id);

            // Send message to clients
            NetOutgoingMessage message = server.CreateMessage();

            message.Write((byte)PacketType.Remove);
            
            message.Write(id);

            server.SendToAll(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendBuffer(NetPlayer player)
        {
            // Send message to clients
            NetOutgoingMessage message = server.CreateMessage();

            message.Write((byte)PacketType.Buffer);
            message.Write(bufferedEntities.Count);

            foreach (KeyValuePair<uint, BufferedEntity> pair in bufferedEntities)
            {
                message.Write(pair.Value.name);
                message.Write(pair.Key);
                
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, pair.Value.args);

                message.Write((int)stream.Length);
                message.Write(stream.ToArray());
            }

            server.SendMessage(message, player.connection, NetDeliveryMethod.ReliableOrdered);
        }
    }

    class BufferedEntity
    {
        public string name;
        public object[] args;
    }
}
