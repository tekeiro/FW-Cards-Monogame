using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using FWCards.Scenes;
using FWCards.Utils.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Tiled;

namespace FWCards.Utils.Map
{
    /// <summary>
    /// Process a TiledMap to obtain specific objects and
    /// other structures that are specific of this game.
    /// </summary>
    public class FWMapProcessor
    {
        //-----------  CONSTANTS  ----------------
        //-- START POINT
        private static readonly string START_POINT = "startPoint";
        

        //------------------  MEMBERS  ------------------
        private TiledMap _map;
        private MapScene _scene;
        
        private Dictionary<string, TiledObject> portals = new Dictionary<string, TiledObject>();

        //------------------  CONSTRUCTOR  ------------------



        //------------------  PROPERTIES  ------------------
        public Vector2 PlayerStartPoint { get; private set; } = new Vector2(0f, 0f);

        public Color BackgroundColor { get; private set; } = Color.CornflowerBlue;

        public string BackgroundPath { get; private set; } = null;


        //------------------  METHODS  ------------------
        /// <summary>
        /// Process Tiled Map and fill all interesant objects.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="mapScene"></param>
        public void Process(TiledMap map, MapScene mapScene)
        {
            _map = map;
            _scene = mapScene;
            portals.Clear();

            // Propiedades de Mapa
            if (map.properties.ContainsKey(Constants.BACKGROUND_COLOR))
            {
                BackgroundColor = Converter.ParseTiledHexColor(
                    map.properties[Constants.BACKGROUND_COLOR], Color.CornflowerBlue);
            }
            if (map.properties.ContainsKey(Constants.BACKGROUND_IMG))
            {
                BackgroundPath = map.properties[Constants.BACKGROUND_IMG];
            }

            // Capa de Objetos
            foreach (var objGroup in map.objectGroups)
            {
                foreach (var obj in objGroup.objects)
                {
                    // Fetch startPoint
                    if (obj.type == START_POINT)
                    {
                        PlayerStartPoint = new Vector2(obj.position.X, obj.position.Y);
                    }

                    // Portals are in FWTiledMapComponent
                    if (obj.type == Constants.PORTAL_TYPE)
                    {
                        portals[obj.name] = obj;
                    }
                }
            }
        }
        


        //------------------  HELPERS  ------------------
        /// <summary>
        /// Find first Object given a Type in all Objects Layer of Tiled Map.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TiledObject findFirstObjectByType(string type)
        {
            foreach (var objGroup in _map.objectGroups)
            {
                foreach (var obj in objGroup.objects)
                {
                    if (obj.type == type)
                        return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a list of TiledObject with all Object in all Object Layers
        /// in Tiled Map with type given.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<TiledObject> findAllObjectsByType(string type)
        {
            List<TiledObject> mapObjects = new List<TiledObject>();

            foreach (var objGroup in _map.objectGroups)
            {
                foreach (var obj in objGroup.objects)
                {
                    if (obj.type == type)
                        mapObjects.Add(obj);
                }
            }
            return mapObjects;
        }

        public TiledObject getPortalObjectByName(string name) =>
            portals[name ?? ""];

    }
}
