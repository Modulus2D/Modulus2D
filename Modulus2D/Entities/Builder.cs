using Modulus2D.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Entities
{
    public class Builder
    {
        /// <summary>
        /// Used to configure an entity. Do not initialize graphics here if you are using
        /// a dedicated server, as this will be run on both the client and the server.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Build(Entity entity) { }

        /// <summary>
        /// Used to configure graphics for an entity, run only on the client.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void BuildGraphics(Entity entity) { }

        /// <summary>
        /// Used to configure an entity, run only on the client if networking is enabled.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void BuildClient(Entity entity, NetworkComponent network) { }

        /// <summary>
        /// Used to configure an entity, run only on the server if networking is enabled.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void BuildServer(Entity entity, NetworkComponent network) { }
    }
}
