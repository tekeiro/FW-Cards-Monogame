using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Components.Map;
using Nez;
using Nez.Tiled;

namespace FWCards.Scenes
{
    /// <summary>
    /// Scene that manages a map
    /// </summary>
    public class MapScene : Scene
    {
        //-----------  CONSTANTS  ----------------
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

        private Entity mapEntity;
        private List<Entity> mapLayersEntities = new List<Entity>();

        private RenderLayerRenderer mapLayerRenderer = null;
        


        //-----------  PROPERTIES  ----------------
        public Entity MapEntity { get { return mapEntity; } }

        public IEnumerable<Entity> MapLayersEntities { get { return mapLayersEntities; } }

        //-----------  CONSTRUCTOR  ----------------


        //-----------  METHODS  ----------------
        public void changeMap(string mapAssetPath)
        {
            _mapAssetPath = mapAssetPath;
            _tiledMap = content.Load<TiledMap>(mapAssetPath);

            destroyAllEntities();
            mapEntity = createEntity("map");
            mapEntity.addComponent(new FWTiledMapComponent(_tiledMap));

            int iLayer = 0;
            foreach (var layer in _tiledMap.layers)
            {
                var layerEntity = createEntity("layer_" + iLayer + "_" + layer.name);
                layerEntity.addComponent(new FWTiledMapLayerRenderer(layer, _tiledMap));
                mapLayersEntities.Add(layerEntity);
                //layerEntity.transform.setParent(mapEntity.transform);

                iLayer++;
            }
        }

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

        public override void initialize()
        {
            base.initialize();

            changeRenderLayers(PREDEFINED_RENDER_LAYERS);
        }
    }
}
