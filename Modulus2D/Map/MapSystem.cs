using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Math;
using Modulus2D.Physics;
using System;
using System.Collections.Generic;
using TiledSharp;

namespace Modulus2D.Map
{
    public class MapSystem : EntitySystem
    {
        private EntityFilter filter;
        private ComponentStorage<MapComponent> mapComponents;
        private ComponentStorage<PhysicsComponent> physicsComponents;

        private SpriteBatch batch;

        // private static Vector2 scale = new Vector2(1 / 16f);

        public MapSystem()
        {
        }

        public MapSystem(SpriteBatch batch)
        {
            this.batch = batch;
        }

        public override void OnAdded()
        {
            mapComponents = World.GetStorage<MapComponent>();
            physicsComponents = World.GetStorage<PhysicsComponent>();

            filter = new EntityFilter(mapComponents, physicsComponents);
            
            World.AddCreatedListener<MapComponent>(AddMap);
        }

        /// <summary>
        /// Reload all maps
        /// </summary>
        public void ReloadAll()
        {
            foreach (int id in World.Iterate(filter))
            {
                Load(mapComponents.Get(id), physicsComponents.Get(id));
            }
        }

        private void AddMap(Entity entity)
        {
            PhysicsComponent physics = entity.AddComponent(new PhysicsComponent());
            physics.Body.BodyType = FarseerPhysics.Dynamics.BodyType.Static;
            MapComponent map = entity.GetComponent<MapComponent>();

            // Load map
            Load(map, physics);
        }

        private void Load(MapComponent map, PhysicsComponent physics)
        {
            // Reset body if reloading
            if (map.Map != null)
            {
                for (int i = 0; i < physics.Body.FixtureList.Count; i++)
                {
                    physics.Body.DestroyFixture(physics.Body.FixtureList[i]);
                }
            }

            map.Map = new TmxMap(map.Filename);

            // Load tileset
            map.Tiles = map.Map.Tilesets[0];

            // Constants
            map.TileWorldWidth = map.Tiles.TileWidth * SpriteBatch.PixelsToMeters;
            map.TileWorldHeight = map.Tiles.TileHeight * SpriteBatch.PixelsToMeters;

            // Add collision
            foreach (TmxLayer layer in map.Map.Layers)
            {
                foreach (TmxLayerTile tile in map.Map.Layers[0].Tiles)
                {
                    if (tile.Gid != 0)
                    {
                        int frame = tile.Gid - 1;

                        // Add collision if necessary
                        foreach(TmxTilesetTile group in map.Tiles.Tiles)
                        {
                            if (group.Id == frame)
                            {
                                foreach (TmxObject collider in group.ObjectGroups[0].Objects)
                                {
                                    float width = (float)collider.Width * (SpriteBatch.PixelsToMeters);
                                    float height = (float)collider.Height * (SpriteBatch.PixelsToMeters);
                                    float x = (float)collider.X * SpriteBatch.PixelsToMeters;
                                    float y = (float)collider.Y * SpriteBatch.PixelsToMeters;
                                    
                                    physics.CreateBox(width, height, new Vector2(tile.X * map.TileWorldWidth + x, -tile.Y * map.TileWorldHeight - y), 1f, 0f);
                                }
                            }
                        }
                    }
                }
            }

            if (batch != null)
            {
                map.Texture = new Texture(map.Tiles.Image.Source);

                // Constants
                map.UvUnitX = (float)map.Tiles.TileWidth / map.Texture.Width;
                map.UvUnitY = (float)map.Tiles.TileHeight / map.Texture.Height;
            }
        }

        public override void Update(float deltaTime)
        {
            if(batch == null)
            {
                return;
            }

            foreach (int id in World.Iterate(filter))
            {
                MapComponent mc = mapComponents.Get(id);
                TmxMap map = mc.Map;

                Texture texture = mc.Texture;
                
                foreach (TmxLayer layer in map.Layers)
                {
                    foreach (TmxLayerTile tile in map.Layers[0].Tiles)
                    {
                        if (tile.Gid != 0)
                        {
                            int frame = tile.Gid - 1;
                            int columns = mc.Tiles.Columns.Value;

                            int column = frame % columns;
                            int row = (int)System.Math.Floor(frame / (double)columns);

                            float uvX = column * mc.UvUnitX;
                            float uvY = row * mc.UvUnitY;

                            batch.Draw(mc.Texture, new Vector2(tile.X * mc.TileWorldWidth, -tile.Y * mc.TileWorldHeight), Vector2.One, 
                                new Vector2(uvX, uvY), new Vector2(uvX + mc.UvUnitX, uvY + mc.UvUnitY), 0f);
                        }
                    }
                }
            }
        }
    }
}