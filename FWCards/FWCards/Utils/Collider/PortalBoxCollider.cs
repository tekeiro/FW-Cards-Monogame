using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace FWCards.Utils.Collider
{
    /// <summary>
    /// Box collider that store useful information about a Portal
    /// in a TiledMap that player can through accross.
    /// </summary>
    public class PortalBoxCollider : BoxCollider
    {

        public PortalBoxCollider(RectangleF rect, bool isTrigger = true) 
            : base(rect.x, rect.y, rect.width, rect.height)
        {
            this.isTrigger = isTrigger;
        }


        public string MapName { get; set; }
        public string AreaName { get; set; }
        public TiledObject TiledObjReference { get; set; }
    }
}
