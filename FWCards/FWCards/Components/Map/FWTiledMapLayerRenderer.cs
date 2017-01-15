using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Nez.Tiled;
using FWCards.Utils;

namespace FWCards.Components.Map
{
    /// <summary>
    /// Render a Tiled Map Layer.
    /// 
    /// Properties that you can add to Layer and will be processed:
    ///   fw:layerDepth   {float}   Layer depth to draw
    ///   fw:renderLayer  {int}     Renderer layer to pass to RenderLayerRenderer. See MapScene.
    ///   fw:physicsLayer {int}     Physic Layer to Physics subsystem.
    /// </summary>
    public class FWTiledMapLayerRenderer : RenderableComponent
    {
        //------------  CONSTANTS  ---------------------
        public static readonly string PROP_LAYER_DEPTH = "fw:layerDepth";
        public static readonly string PROP_RENDER_LAYER = "fw:renderLayer";
        public static readonly string PROP_PHYSICS_LAYER = "fw:physicsLayer";

        //------------  MEMBERS  ---------------------
        private TiledLayer _tiledLayer;
        private TiledMap _map;
        private List<Collider> _colliders = new List<Collider>();
        private float _width;
        private float _height;

        //------------  CONSTRUCTOR  ---------------------
        /// <summary>
        /// Create Component that render a Tiled Layer.
        /// Parameter tileLayer must be not null.
        /// </summary>
        /// <param name="tileLayer">Must be not null.</param>
        /// <param name="map">Must be not null.</param>
        public FWTiledMapLayerRenderer(TiledLayer tileLayer, TiledMap map)
        {
            Assert.isNotNull(tileLayer, "Tile Layer must not be null");
            Assert.isNotNull(map, "Map must be not null");
            _tiledLayer = tileLayer;
            _map = map;

            // Fill width and height
            if (_tiledLayer is TiledImageLayer)
            {
                _width = _map.width*_map.tileWidth;
                _height = _map.height*_map.tileHeight;
            }
            else if (_tiledLayer is TiledTileLayer)
            {
                _width = (_tiledLayer as TiledTileLayer).width * _map.tileWidth;
                _height = (_tiledLayer as TiledTileLayer).height * _map.tileHeight;
            }
            else if (_tiledLayer is TiledIsometricTiledLayer)
            {
                _width = (_tiledLayer as TiledIsometricTiledLayer).width * _map.tileWidth;
                _height = (_tiledLayer as TiledIsometricTiledLayer).height * _map.tileHeight;
            }


        }


        //------------  PROPERTIES  ---------------------
        public int PhysicsLayer { get; set; } = 1 << 0;

        public override float width => _width;

        public override float height => _height;

        public IEnumerable<Collider> CollidersCollection => _colliders;


        //------------  METHODS  ---------------------
        


        private void searchForLayerProperties()
        {
            if (_tiledLayer.properties.ContainsKey(PROP_RENDER_LAYER))
                setRenderLayer(Converter.ParseInt(_tiledLayer.properties[PROP_RENDER_LAYER], renderLayer));
            if (_tiledLayer.properties.ContainsKey(PROP_LAYER_DEPTH))
                setLayerDepth(Converter.ParseFloat(_tiledLayer.properties[PROP_LAYER_DEPTH], layerDepth));
            if (_tiledLayer.properties.ContainsKey(PROP_PHYSICS_LAYER))
                PhysicsLayer = Converter.ParseInt(_tiledLayer.properties[PROP_PHYSICS_LAYER], PhysicsLayer);
        }


        //------------  EVENTS  ---------------------
        public override void render(Graphics graphics, Camera camera)
        {
            if (_tiledLayer.visible)
            {
                _tiledLayer.draw(graphics.batcher, entity.transform.position + _localOffset, layerDepth, camera.bounds);
            }
        }

        public override void onAddedToEntity()
        {
            searchForLayerProperties();
            addColliders();
        }

        public override void onRemovedFromEntity()
        {
            removeColliders();
        }

        public override void onEntityTransformChanged(Transform.Component comp)
        {
            if (comp == Transform.Component.Position)
            {
                removeColliders();
                addColliders();
            }
        }


        //------------  COLLIDERS  ----------------
        public void addColliders()
        {
            TiledTile[] tiles;

            if (_tiledLayer is TiledTileLayer)
                tiles = (_tiledLayer as TiledTileLayer).tiles;
            else if (_tiledLayer is TiledIsometricTiledLayer)
                tiles = (_tiledLayer as TiledIsometricTiledLayer).tiles;
            else
                tiles = new TiledTile[0];

            foreach (var tile in tiles)
            {
                if (tile != null && tile.tilesetTile.objectGroups.Count > 0)
                {
                    foreach (var objGroup in tile.tilesetTile.objectGroups)
                    {
                        foreach (var obj in objGroup.objects)
                        {
                            var tilePos = tile.getWorldPosition(_map);
                            var collider = new BoxCollider(
                                entity.transform.position.X + tilePos.X + obj.x + _localOffset.X,
                                entity.transform.position.Y + tilePos.Y +  obj.y + _localOffset.Y,
                                obj.width,
                                obj.height
                            );
                            collider.physicsLayer = PhysicsLayer;
                            collider.entity = entity;
                            _colliders.Add(collider);

                            Physics.addCollider(collider);
                        }
                    }
                }
            }
        }

        public void removeColliders()
        {
            foreach (var collider in _colliders)
                Physics.removeCollider(collider);
            _colliders.Clear();
        }

        //------------  DEBUG RENDER  ----------------
        public override void debugRender(Graphics graphics)
        {
            if (_colliders != null)
            {
                foreach (var collider in _colliders)
                    collider.debugRender(graphics);
            }
        }
    }
}
