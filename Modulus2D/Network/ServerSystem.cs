using Lidgren.Network;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Physics;
using Modulus2D.Player.Platformer;
using System;

namespace Modulus2D.Network
{
    public class ServerSystem : EntitySystem
    {
        NetServer server;
        NetworkSystem networkSystem;
        Networker networker;

        private float updateTime = 0.025f; // 40 UPS
        private float accumulator = 0f;

        public ServerSystem(NetworkSystem networkSystem, int port)
        {
            this.networkSystem = networkSystem;

            // TODO: Not hard-coded?
            NetPeerConfiguration config = new NetPeerConfiguration("Modulus")
            {
                Port = port
            };

            server = new NetServer(config);
            server.Start();

            networker = new Networker(server);

            networker.Connect += OnConnect;
            networker.Disconnect += OnDisconnect;
            networker.Update += OnUpdate;
        }

        public void OnConnect(NetConnection connection)
        {
            Console.WriteLine("Connect");

            CreatePlayerUpdate update = new CreatePlayerUpdate();

            // Create player
            Entity entity = World.Create();
            entity.AddComponent(new TransformComponent());

            PlayerComponent player = new PlayerComponent();
            entity.AddComponent(player);

            PhysicsComponent physics = new PhysicsComponent();
            entity.AddComponent(physics);
            physics.CreateCircle(0.5f, 1f);

            NetworkComponent network = new NetworkComponent();
            entity.AddComponent(network);

            update.id = network.Id;

            // Transmit physics information and receive player information
            network.AddTransmitter(physics);
            network.AddReceiver(player);

            foreach (NetConnection currentConnection in server.Connections)
            {
                if (currentConnection == connection)
                {
                    update.isMine = true;
                } else
                {
                    update.isMine = false;
                }

                server.SendMessage(networker.CreateEvent("CreatePlayer", update), currentConnection, NetDeliveryMethod.ReliableOrdered);
            }
        }

        public void OnDisconnect()
        {

        }

        public void OnUpdate(UpdatePacket packet)
        {
            networkSystem.Receive(packet);
        }

        public override void Update(float deltaTime)
        {
            networker.ReadMessages();

            accumulator += deltaTime;

            // Send update
            if (accumulator > updateTime)
            {
                server.SendToAll(networker.CreateUpdate(networkSystem.Transmit()), NetDeliveryMethod.UnreliableSequenced);

                accumulator = 0f;
            }
        }
    }
}
