using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Components.Map;
using FWCards.Config;
using FWCards.Utils.Collider;
using FWCards.Utils.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;

namespace FWCards.Scenes
{
    /// <summary>
    /// Scene that manages a map
    /// </summary>
    public class MapScene : Scene
    {
        

        /// <summary>
        /// Array with all possible RenderLayer to sort 
        /// layers in a Tiled Map.
        /// Lower render layer are in front, and higher are in the back.
        /// 
        /// There are 9 Render Layer predefined (from 10 to 1), but you can modify this array
        /// calling changeRenderLayer at any time.
        /// Render layer 0 is used for UI.
        /// </summary>
        public static readonly int[] PREDEFINED_RENDER_LAYERS = new int[]
        {
            10, 9, 8, 7, 6, 5, 4, 3, 2, 1
        };

        /// <summary>
        /// Render order of Renderer that draw TiledMap layers.
        /// </summary>
        public static readonly int RENDER_ORDER_MAP = 1;


        //-----------  MEMBER  -------------------
        private string _mapAssetPath;
        private TiledMap _tiledMap;
        private FWMapProcessor _mapProcessor;

        private Entity mapEntity;
        private readonly List<Entity> mapLayersEntities = new List<Entity>();

        private RenderLayerRenderer mapLayerRenderer = null;

        private readonly Texture2D charactersTexture;
        private readonly List<Subtexture> charactersSubtextures;


        //-----------  PROPERTIES  ----------------
        public Entity MapEntity { get { return mapEntity; } }

        public IEnumerable<Entity> MapLayersEntities => mapLayersEntities;

        public Texture2D MapCharactersTexture => charactersTexture;

        public IReadOnlyList<Subtexture> MapCharactersSubtextures => charactersSubtextures;

        public FWMapProcessor MapProcessor => _mapProcessor;

        //-----------  CONSTRUCTOR  ----------------
        public MapScene()
        {
            charactersTexture = content.Load<Texture2D>(Assets.MAIN_CHARS);
            charactersSubtextures = Subtexture.subtexturesFromAtlas(charactersTexture, Assets.CHARS_WIDTH, Assets.CHARS_HEIGHT);
        }

        //-----------  EVENTS  ----------------
        public override void initialize()
        {
            base.initialize();
            _mapProcessor = new FWMapProcessor();

            changeRenderLayers(PREDEFINED_RENDER_LAYERS);
        }

        //-----------  METHODS  ----------------
        public void changeMap(string mapAssetPath)
        {
            clearMapEntities();

            _mapAssetPath = mapAssetPath;
            _tiledMap = content.Load<TiledMap>(mapAssetPath);
            
            _mapProcessor.Process(_tiledMap, this);

            clearColor = _mapProcessor.BackgroundColor;
            if (_mapProcessor.BackgroundPath != null)
            {
                var backgroundTxr = content.Load<Texture2D>(_mapProcessor.BackgroundPath);
                var bkgEntity = createEntity("__map_background");
                var bkgSprite = bkgEntity.addComponent(new Sprite());
                bkgSprite.setSubtexture(new Subtexture(backgroundTxr));
                bkgSprite.entity.transform.position = new Vector2(0f, 0f);
                
            }
            
            mapEntity = createEntity("__map");
            mapEntity.addComponent(new FWTiledMapComponent(_tiledMap));

            int iLayer = 0;
            foreach (var layer in _tiledMap.layers)
            {
                var layerEntity = createEntity("__layer_" + iLayer + "_" + layer.name);
                layerEntity.addComponent(new FWTiledMapLayerRenderer(layer, _tiledMap));
                mapLayersEntities.Add(layerEntity);
                //layerEntity.transform.setParent(mapEntity.transform);

                iLayer++;
            }
        }

        public Vector2 getPositionForPortalArea(string areaName, Vector2 pos, PortalBoxCollider oldPortal)
        {
            var result = new Vector2(0f, 0f);
            var portalObj = _mapProcessor.getPortalObjectByName(areaName);
            if (portalObj != null)
            {
                if (portalObj.width >= portalObj.height)
                {
                    var xDelta = pos.X - oldPortal.bounds.x;
                    xDelta = (xDelta/oldPortal.width) * portalObj.width;
                    result.X = portalObj.position.X + xDelta;
                    result.Y = portalObj.position.Y;

                    if (pos.Y > portalObj.position.Y)
                        result.Y -= 5;
                    else
                        result.Y += 5;
                }
                else
                {
                    var yDelta = pos.Y - oldPortal.bounds.y;
                    yDelta = (yDelta/oldPortal.height)*portalObj.height;
                    result.X = portalObj.position.X;
                    result.Y = portalObj.position.Y + yDelta;

                    if (pos.X > portalObj.position.X)
                        result.X -= 5;
                    else
                        result.X += 5;
                }
            }
            return result;
        }

        /// <summary>
        /// Change Render Layers used for draw all layers of Map.
        /// This method remove actual RenderLayerRenderer and creates
        /// a new one.
        /// </summary>
        /// <param name="renderLayers"></param>
        public void changeRenderLayers(int[] renderLayers)
        {
            if (mapLayerRenderer != null)
            {
                removeRenderer(mapLayerRenderer);
                mapLayerRenderer.unload();
            }
            mapLayerRenderer = new RenderLayerRenderer(RENDER_ORDER_MAP, renderLayers);

            addRenderer(mapLayerRenderer);
        }

        /// <summary>
        /// Search for a StartPosition marker in TiledMap and update
        /// entity passed with that position.
        /// </summary>
        /// <param name="entity"></param>
        public void setMapStartPositionForEntity(Entity entity)
        {
            entity.transform.position = new Vector2(
                _mapProcessor.PlayerStartPoint.X, _mapProcessor.PlayerStartPoint.Y
            );
        }


        //-----------  PRIVATE METHODS  -----------------
        private void clearMapEntities()
        {
            if (mapEntity != null)
            {
                mapEntity.destroy();
            }
            foreach (var mapLayerEntity in mapLayersEntities)
            {
                mapLayerEntity.destroy();
            }
            mapLayersEntities.Clear();
        }
    }
}
