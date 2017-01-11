using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Components.Player;
using Nez;

namespace FWCards.Config
{
    public static class EntityFactory
    {

        public static Entity CreateGrayMapPlayer(Scene scene)
        {
            var grayMapPlayer = scene.createEntity("gray-map-player");
            
            var walkAnims = new Dictionary<MapPlayerComponent.Animations, int[]>
            {
                { MapPlayerComponent.Animations.WalkBottom, new int[] {6, 7, 8} },
                { MapPlayerComponent.Animations.WalkLeft, new int[] {18, 19, 20} },
                { MapPlayerComponent.Animations.WalkRight, new int[] {30, 31, 32} },
                { MapPlayerComponent.Animations.WalkTop, new int[] {42, 43, 44} },
            };
            var mapPlayerComp = new MapPlayerComponent(7, walkAnims);
            grayMapPlayer.addComponent(mapPlayerComp);

            return grayMapPlayer;
        }

    }
}
