using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Components.Map;
using FWCards.Components.Player;
using FWCards.Scenes;
using Nez;
using Nez.Sprites;

namespace FWCards.Config
{
    public static class EntityFactory
    {

        public static Entity CreateGrayMapPlayer(MapScene scene)
        {
            var grayMapPlayer = scene.createEntity("gray-map-player");

            var mapPlayerData = new MapPlayerCompData();
            mapPlayerData.animations = new Dictionary<MapPlayerComponent.Animations, int[]>
            {
                { MapPlayerComponent.Animations.WalkBottom, new [] {6, 7, 8} },
                { MapPlayerComponent.Animations.WalkLeft, new [] {18, 19, 20} },
                { MapPlayerComponent.Animations.WalkRight, new [] {30, 31, 32} },
                { MapPlayerComponent.Animations.WalkTop, new [] {42, 43, 44} },
                { MapPlayerComponent.Animations.Idle, new []{ 7 } },
            };
            mapPlayerData.indexIdle = 7;
            mapPlayerData.charsSubtextures = scene.MapCharactersSubtextures;

            var mapPlayerComp = new MapPlayerComponent(mapPlayerData);

            grayMapPlayer.addComponent(
                new Sprite<MapPlayerComponent.Animations>(scene.MapCharactersSubtextures[mapPlayerData.indexIdle])
            );
            grayMapPlayer.addComponent(mapPlayerComp);
            grayMapPlayer.addComponent(new BoxCollider(-12, 6, 24, 10));
            grayMapPlayer.addComponent(new FWTiledMover());
            grayMapPlayer.addComponent(new FollowCamera(grayMapPlayer));

            return grayMapPlayer;
        }

    }
}
