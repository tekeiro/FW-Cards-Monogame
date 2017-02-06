using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Components.Map;
using FWCards.Config;
using FWCards.Scenes;
using FWCards.Utils.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;

namespace FWCards.Components.Player
{
    /// <summary>
    /// Data class to initialize 
    /// MapPlayerComponent 
    /// </summary>
    public class MapPlayerCompData
    {
        public int indexIdle;
        public Dictionary<MapPlayerComponent.Animations, int[]> animations;
        public IReadOnlyList<Subtexture> charsSubtextures;
    }

    /// <summary>
    /// Component that represents a Controllable Player in
    /// a Tiled Map
    /// </summary>
    public class MapPlayerComponent : Component, IUpdatable, ITriggerListener
    {
        public static readonly int PLAYER_RENDER_LAYER = 8;

        public enum Animations
        {
            Idle,
            WalkLeft,
            WalkRight,
            WalkTop,
            WalkBottom
        }

        // -----------  MEMBERS  ---------------------
        //-- COMPS
        private Sprite<Animations> _sprite;
        private FWTiledMover _mover;
        //-- ANIMATIONS
        private MapPlayerCompData _playerData;
        //-- REFS
        private InputService Input;

        private bool inPortalArea = false;

        // -----------  CONSTRUCTOR  ---------------------
        public MapPlayerComponent(MapPlayerCompData playerData)
        {
            _playerData = playerData;

            Input = Core.services.GetService<InputService>();
        }

        // -----------  PROPERTIES  ---------------------
        public float WalkSpeed = 150f;
        


        //------------  EVENTS  ------------------
        public override void onAddedToEntity()
        {
            
            _sprite = entity.getComponent<Sprite<Animations>>();
            _sprite.setRenderLayer(PLAYER_RENDER_LAYER);

            foreach (var animPair in _playerData.animations)
            {
                var regions = new List<Subtexture>();
                foreach (var animIndex in animPair.Value)
                {
                    regions.Add(_playerData.charsSubtextures[animIndex]);
                }
                _sprite.addAnimation(animPair.Key, new SpriteAnimation(regions));
            }

            _mover = entity.getComponent<FWTiledMover>();
            _sprite.play(Animations.Idle);
        }


        //--------------  UPDATE  --------------
        public void update()
        {
            // Calculate Speed based on Input and Time
            var vel = new Vector2(
                Input.HorizontalAxis.value * WalkSpeed * Time.deltaTime,
                Input.VerticalAxis.value * WalkSpeed * Time.deltaTime
            );
            var animation = Animations.Idle;

            // Try to move and Check for Collisions
            var colResult = _mover.move(vel);

            // Check for Animation
            if (vel != Vector2.Zero)
            {
                if (vel.X > 0f)
                    animation = Animations.WalkRight;
                else if (vel.X < 0f)
                    animation = Animations.WalkLeft;

                if (vel.Y > 0f)
                    animation = Animations.WalkBottom;
                else if (vel.Y < 0f)
                    animation = Animations.WalkTop;

                if (_sprite.currentAnimation != animation)
                    _sprite.play(animation);
                else if (_sprite.currentAnimation == animation 
                    && !_sprite.isPlaying)
                    _sprite.unPause();
                
            }
            else 
                _sprite.pause();

            // Move Sprite
            if (!colResult.collides)
                entity.transform.position += vel;
            
        }

        private TiledObject portalDisabled = null;

        //-------------  TRIGGER LISTENER  ---------------
        private bool isColliderDisabled(Collider other)
        {
            if (portalDisabled == null)
                return false;
            else
            {
                var portalCol = other as PortalBoxCollider;
                return portalCol.TiledObjReference == portalDisabled;
            }

        }

        public void onTriggerEnter(Collider other, Collider local)
        {

            if (other is PortalBoxCollider)
            {
                var portal = other as PortalBoxCollider;
                Debug.info("Player collides with portal to map {0} and area {1}", portal.MapName, portal.AreaName);
            }

            if (other is PortalBoxCollider && !inPortalArea 
                && !isColliderDisabled(other))
            {
                inPortalArea = true;

                var portal = other as PortalBoxCollider;
                var mapScene = entity.scene as MapScene;
                mapScene.changeMap("Maps/" + portal.MapName);
                var position = mapScene.getPositionForPortalArea(portal.AreaName, entity.transform.position, portal);
                entity.transform.position = new Vector2(position.X, position.Y);

                portalDisabled = mapScene.MapProcessor.getPortalObjectByName(portal.AreaName);
                Debug.info("Portal disabled {0}", portalDisabled?.name ?? "N/A");
            }
        }

        public void onTriggerExit(Collider other, Collider local)
        {
            if (other is PortalBoxCollider)
            {
                var portal = other as PortalBoxCollider;
                Debug.info("Exited of portal to map {0} and area {1}", portal.MapName, portal.AreaName);
            }

            if (other is PortalBoxCollider && inPortalArea 
                && isColliderDisabled(other) )
            {
                var portal = other as PortalBoxCollider;
                portalDisabled = null;
                inPortalArea = false;
                Debug.info("No portal disabled");
            }
        }
    }
}
