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
            World.AddCreationListener<MapComponent>(AddMap);
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
            Texture texture = new Texture(tiles.Image.Source);
            map.States = new RenderStates(texture);

            // Create vertices
            map.Vertices = new VertexArray(PrimitiveType.Quads);

            // Reset body
            if (physics.Body != null)
            {
                for (int i = 0; i < physics.Body.FixtureList.Count; i++)
                {
                    physics.Body.DestroyFixture(physics.Body.FixtureList[i]);
                }
            }

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
                        if (tiles.Tiles.Count > frame && tiles.Tiles[frame].ObjectGroups.Count != 0)
                        {
                            foreach (TmxObject collider in tiles.Tiles[frame].ObjectGroups[0].Objects)
                            {
                                float width = (float)collider.Width * (SpriteBatch.PixelsToMeters / 2f);
                                float height = (float)collider.Height * (SpriteBatch.PixelsToMeters / 2f);

                                var shape = new PolygonShape(PolygonTools.CreateRectangle(width, height, new Vector2(tile.X, tile.Y), 0f), 1f);

                                physics.Body.CreateFixture(shape);
                            }
                        }

                        // Draw into array
                        if (render)
                        {
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
                for (int i = 0; i < maps.Count; i++)
                {
                    MapComponent map = maps[i];
                    batch.Draw(map.Vertices, map.States);
                }
            }
        }
    }
}
