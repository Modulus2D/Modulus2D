using Ionic.Zlib;
using Lidgren.Network;
using Modulus2D.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modulus2D.Network
{

    public class ClientSystem : EntitySystem
    {
        NetClient client;

        public ClientSystem(string host, int port)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Network");
            client = new NetClient(config);

            client.Start();
            client.Connect(host, port);
        }

        public override void Update(float deltaTime)
        {
            NetOutgoingMessage message = client.CreateMessage();
        }
    }
}
