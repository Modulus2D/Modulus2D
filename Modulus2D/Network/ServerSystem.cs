using Lidgren.Network;
using Modulus2D.Entities;

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
