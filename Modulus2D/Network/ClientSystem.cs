using Ionic.Zlib;
using Lidgren.Network;
using Modulus2D.Core;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Physics;
using Modulus2D.Player.Platformer;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modulus2D.Network
{

    public class ClientSystem : EntitySystem
    {
        NetClient client;
        NetworkSystem networkSystem;
        Networker networker;

        private float updateTime = 0.025f; // 40 UPS
        private float accumulator = 0f;

        public ClientSystem(NetworkSystem networkSystem, string host, int port)
        {
            this.networkSystem = networkSystem;

            NetPeerConfiguration config = new NetPeerConfiguration("Modulus");
            client = new NetClient(config);

            client.Start();
            client.Connect(host, port);

            networker = new Networker(client);

            networker.Connect += OnConnect;
            networker.Disconnect += OnDisconnect;
            networker.Update += OnUpdate;

            networker.RegisterEvent("CreatePlayer", CreatePlayer);
        }

        public void OnConnect(NetConnection connection)
        {
            Console.WriteLine("Connect");
        }

        public void OnDisconnect()
        {

        }

        public void OnUpdate(UpdatePacket packet)
        {
            networkSystem.Receive(packet);
        }

        public void CreatePlayer(IUpdate update)
        {
            CreatePlayerUpdate playerUpdate = (CreatePlayerUpdate)update;

            Console.WriteLine("Create player " + playerUpdate.id);

            // Create player
            Texture face = new Texture("Resources/Textures/Face.png");

            Entity entity = World.Create();
            entity.AddComponent(new TransformComponent());
            entity.AddComponent(new SpriteComponent(face));

            PlayerComponent player = new PlayerComponent();
            entity.AddComponent(player);
            
            PhysicsComponent physics = new PhysicsComponent();
            entity.AddComponent(physics);
            physics.CreateCircle(0.5f, 1f);

            NetworkComponent network = new NetworkComponent();
            entity.AddComponent(network);
            network.Id = playerUpdate.id;
            
            // Receive physics information
            network.AddReceiver(physics);

            if (playerUpdate.isMine)
            {
                entity.AddComponent(new PlayerInputComponent());

                // Transmit player information
                network.AddTransmitter(player);
            }
        }

        public override void Update(float deltaTime)
        {
            networker.ReadMessages();

            accumulator += deltaTime;

            // Send update
            if (accumulator > updateTime)
            {
                client.SendMessage(networker.CreateUpdate(networkSystem.Transmit()), NetDeliveryMethod.UnreliableSequenced);

                accumulator = 0f;
            }
        }
    }
}
