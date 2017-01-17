using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Components.Map;
using FWCards.Config;
using FWCards.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;

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
    public class MapPlayerComponent : Component, IUpdatable
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
    }
}
