using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Modulus2D.Entities;
using Modulus2D.Graphics;
using Modulus2D.Physics;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using TiledSharp;

namespace Modulus2D.Map
{
    public class MapSystem : EntitySystem
    {
        // TODO: Seperate MapSystem and MapRenderSystem classes?
        private bool render = false;

        private List<MapComponent> maps = new List<MapComponent>();
        private List<PhysicsComponent> physicsComponents = new List<PhysicsComponent>();

        private SpriteBatch batch;

        public MapSystem()
        {
        }

        public MapSystem(SpriteBatch batch)
        {
            render = true;
            this.batch = batch;
        }

        public override void AddedToWorld()
        {
            World.AddCreatedListener<MapComponent>(AddMap);
        }

        public void ReloadAll()
        {
            for (int i = 0; i < maps.Count; i++)
            {
                Load(maps[i], physicsComponents[i]);
            }
        }

        private void AddMap(Entity entity)
        {
            PhysicsComponent physics = entity.GetComponent<PhysicsComponent>();
            if (physics == null)
            {
                //logger.Error("Attempted to map collider before adding physics");
                return;
            }

            MapComponent map = entity.GetComponent<MapComponent>();

            // Load map
            Load(map, physics);

            maps.Add(map);
            physicsComponents.Add(physics);
        }

        private void Load(MapComponent map, PhysicsComponent physics)
        {
            TmxMap tmxMap = new TmxMap(map.Filename);

            // Load tileset
            TmxTileset tiles = tmxMap.Tilesets[0];
            
            // Reset body
            if (physics.Body != null)
            {
                for (int i = 0; i < physics.Body.FixtureList.Count; i++)
                {
                    physics.Body.DestroyFixture(physics.Body.FixtureList[i]);
                }
            }

            // Add collision
            foreach (TmxLayer layer in tmxMap.Layers)
            {
                foreach (TmxLayerTile tile in tmxMap.Layers[0].Tiles)
                {
                    if (tile.Gid != 0)
                    {
                        int frame = tile.Gid - 1;
                        int columns = tiles.Columns.Value;

                        int column = frame % columns;
                        int row = (int)Math.Floor(frame / (double)columns);

                        float uvX = tiles.TileWidth * column;
                        float uvY = tiles.TileWidth * row;

                        // Add collision if necessary
                        foreach(TmxTilesetTile group in tiles.Tiles)
                        {
                            if (group.Id == frame)
                            {
                                foreach (TmxObject collider in group.ObjectGroups[0].Objects)
                                {
                                    float width = (float)collider.Width * (SpriteBatch.PixelsToMeters / 2f);
                                    float height = (float)collider.Height * (SpriteBatch.PixelsToMeters / 2f);
                                    float x = (float)collider.X * SpriteBatch.PixelsToMeters;
                                    float y = (float)collider.Y * SpriteBatch.PixelsToMeters;

                                    var shape = new PolygonShape(PolygonTools.CreateRectangle(width, height, new Vector2(tile.X + x, tile.Y + y), 0f), 1f);

                                    physics.Body.CreateFixture(shape);
                                }
                            }
                        }
                    }
                }
            }
            
            // Draw into array if rendering enabled
            if (render)
            {
                // Create vertices
                map.Vertices = new VertexArray(PrimitiveType.Quads);

                // Load texture
                Texture texture = new Texture(tiles.Image.Source);
                map.States = new RenderStates(texture);

                foreach (TmxLayer layer in tmxMap.Layers)
                {
                    foreach (TmxLayerTile tile in tmxMap.Layers[0].Tiles)
                    {
                        if (tile.Gid != 0)
                        {
                            int frame = tile.Gid - 1;
                            int columns = tiles.Columns.Value;

                            int column = frame % columns;
                            int row = (int)Math.Floor(frame / (double)columns);

                            float uvX = tiles.TileWidth * column;
                            float uvY = tiles.TileWidth * row;

                            SpriteBatch.DrawRegion(texture, new Vector2(tile.X, tile.Y), new Vector2(uvX, uvY), new Vector2(uvX + tiles.TileWidth, uvY + tiles.TileHeight), map.Vertices);
                        }
                    }
                }
            }
        }

        public override void Update(float deltaTime)
        {
            if(render)
            {
                batch.Begin();

                for (int i = 0; i < maps.Count; i++)
                {
                    MapComponent map = maps[i];
                    batch.Draw(map.Vertices, map.States);
                }

                batch.End();
            }
        }
    }
}