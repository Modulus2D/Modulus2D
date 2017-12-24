using Lidgren.Network;
using Modulus2D.Entities;
using NLog;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modulus2D.Network
{
    /// <summary>
    /// Called when an event is triggered
    /// </summary>
    /// <param name="args"></param>
    public delegate void NetEvent(object[] args);

    /// <summary>
    /// Called when an update is received
    /// </summary>
    public delegate void NetUpdate(float delta);

    /// <summary>
    /// Called when a networked entity is added to the world
    /// </summary>
    /// <param name="entity"></param>
    public delegate void NetCreate(Entity entity, object[] args);

    /// <summary>
    /// System for synchronizing entities across the network
    /// </summary>
    public class NetSystem : EntitySystem
    {
        public static string Identifier = "modulus";
        
        // Net components
        protected EntityFilter filter;
        protected ComponentStorage<NetComponent> netComponents;

        // Networked entities
        protected Dictionary<uint, Entity> networkedEntities;

        // Net events
        protected Dictionary<string, NetEvent> netEvents;

        // Entity creation
        protected Dictionary<string, NetCreate> creators;

        // Binary formatter
        protected BinaryFormatter formatter;

        // Stopwatch for delta
        protected Stopwatch stopwatch;

        protected float updateTime = 1 / 30f;
        protected float accumulator = 0f;

        public NetSystem()
        {
            networkedEntities = new Dictionary<uint, Entity>();

            netEvents = new Dictionary<string, NetEvent>();
            creators = new Dictionary<string, NetCreate>();

            formatter = new BinaryFormatter();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public override void OnAdded()
        {
            netComponents = World.GetStorage<NetComponent>();
            filter = new EntityFilter(netComponents);
        }

        /// <summary>
        /// Gets a networked entity by its net ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entity GetByNetId(uint id)
        {
            return networkedEntities[id];
        }
    }
}
