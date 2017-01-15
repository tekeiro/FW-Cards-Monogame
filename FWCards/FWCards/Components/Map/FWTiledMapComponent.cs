using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //-------  MEMBERS  ------------
        


        //-------  CONSTRUCTOR  ------------
        public FWTiledMapComponent(TiledMap map)
        {
            Map = map;
        }


        //-------  PROPERTIES  ------------
        public TiledMap Map { get; set; }


        //-------  METHODS  ------------
        public void update()
        {
            Map.update();
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
