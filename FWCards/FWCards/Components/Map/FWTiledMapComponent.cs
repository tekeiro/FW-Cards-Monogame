using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using FWCards.Utils;
using FWCards.Utils.Collider;
using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;

namespace FWCards.Components.Map
{
    /// <summary>
    /// Component to store a reference and useful methods for a TiledMap
    /// </summary>
    public class FWTiledMapComponent : Component, IUpdatable
    {
        //-----------  CONSTANTS  ----------------
        //-- PORTAL
        private static readonly string PORTAL_MAP = "Map";
        private static readonly string PORTAL_AREA = "Area";
        private static readonly string PORTAL_PHYLAY = "PhysicsLayer";

        //-------  MEMBERS  ------------
        private Dictionary<string, PortalBoxCollider> portals = new Dictionary<string, PortalBoxCollider>();


        //-------  CONSTRUCTOR  ------------
        public FWTiledMapComponent(TiledMap map)
        {
            Map = map;
        }


        //-------  PROPERTIES  ------------
        public TiledMap Map { get; set; }

        public IEnumerable<PortalBoxCollider> Portals => portals.Values;


        //-------  PORTALS  METHODS ----------
        public PortalBoxCollider findColliderByName(string name)
            => portals[name];

        //-------  METHODS  ------------
        public void update()
        {
            Map.update();
        }

        public override void onAddedToEntity()
        {
            foreach (var objGroup in Map.objectGroups)
            {
                foreach (var obj in objGroup.objects)
                {
                    // Fetch portals
                    if (obj.type == Constants.PORTAL_TYPE)
                    {
                        string portalMap = obj.properties.GetOrElse(PORTAL_MAP);
                        string portalArea = obj.properties.GetOrElse(PORTAL_AREA);
                        int portalPhysicLayer = Converter.ParseInt(obj.properties.GetOrElse(PORTAL_PHYLAY), 1 << 0);

                        var portalCollider = new PortalBoxCollider(
                            new RectangleF(obj.position.X, obj.position.Y,
                                obj.width, obj.height)
                        );
                        portalCollider.physicsLayer = portalPhysicLayer;
                        portalCollider.MapName = portalMap;
                        portalCollider.AreaName = portalArea;
                        portalCollider.entity = entity;
                        portalCollider.TiledObjReference = obj;

                        portals.Add(obj.name, portalCollider);
                        Physics.addCollider(portalCollider);
                    }
                }
            }
        }

        public override void onRemovedFromEntity()
        {
            foreach (var portal in portals)
            {
                Physics.removeCollider(portal.Value);
            }
            portals.Clear();
        }

        //-----------  DEBUG RENDER  ------------------
        public override void debugRender(Graphics graphics)
        {
            foreach (var group in Map.objectGroups)
                renderObjectGroup(group, graphics);
        }

        private void renderObjectGroup(TiledObjectGroup group, Graphics graphics)
        {
            var renderPosition = entity.transform.position;

            foreach (var obj in group.objects)
            {
                if (!obj.visible)
                    continue;

                switch (obj.tiledObjectType)
                {
                    case TiledObject.TiledObjectType.Ellipse:
                        graphics.batcher.drawCircle(
                            new Vector2(
                                renderPosition.X + obj.x + obj.width * 0.5f,
                                renderPosition.Y + obj.y + obj.height * 0.5f
                             ),
                            obj.width * 0.5f,
                            group.color
                        );
                        break;
                    case TiledObject.TiledObjectType.Image:
                        throw new NotImplementedException("Image layers are not yet supported");
                    case TiledObject.TiledObjectType.Polygon:
                        graphics.batcher.drawPoints(renderPosition, obj.polyPoints, group.color, true);
                        break;
                    case TiledObject.TiledObjectType.Polyline:
                        graphics.batcher.drawPoints(renderPosition, obj.polyPoints, group.color, false);
                        break;
                    case TiledObject.TiledObjectType.None:
                        graphics.batcher.drawHollowRect(renderPosition.X + obj.x, renderPosition.Y + obj.y, obj.width, obj.height, group.color);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
