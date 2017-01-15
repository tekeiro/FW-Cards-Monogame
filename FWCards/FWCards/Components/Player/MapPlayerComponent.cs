using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace FWCards.Components.Player
{

    /// <summary>
    /// Component that represents a Controllable Player in
    /// a Tiled Map
    /// </summary>
    public class MapPlayerComponent : Component, IUpdatable
    {
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
        private Sprite<Animations> _animations;
        //-- ANIMATIONS
        private int idleAnim = -1;
        private Dictionary<Animations, int[]> walkAnimations;
        //-- REFS
        private InputService Input;

        // -----------  CONSTRUCTOR  ---------------------
        public MapPlayerComponent(int indexIdle, Dictionary<Animations, int[]> animations)
        {
            idleAnim = indexIdle;
            walkAnimations = new Dictionary<Animations, int[]>(animations);

            Input = Core.services.GetService<InputService>();
        }

        // -----------  PROPERTIES  ---------------------
        public float WalkSpeed = 150f;
        


        //------------  EVENTS  ------------------
        public override void onAddedToEntity()
        {
            var texture = entity.scene.content.Load<Texture2D>(Assets.MAIN_CHARS);
            var subtextures = Subtexture.subtexturesFromAtlas(texture, Assets.CHARS_WIDTH, Assets.CHARS_HEIGHT);

            _animations = entity.addComponent(new Sprite<Animations>(subtextures[idleAnim]));
            _animations.setRenderLayer(8);

            foreach (var animPair in walkAnimations)
            {
                var regions = new List<Subtexture>();
                foreach (var animIndex in animPair.Value)
                {
                    regions.Add(subtextures[animIndex]);
                }
                _animations.addAnimation(animPair.Key, new SpriteAnimation(regions));
            }
            
        }


        //--------------  UPDATE  --------------
        public void update()
        {
            var vel = new Vector2(
                Input.HorizontalAxis.value * WalkSpeed * Time.deltaTime,
                Input.VerticalAxis.value * WalkSpeed * Time.deltaTime
            );
            var animation = Animations.Idle;

            transform.position = new Vector2(
                transform.position.X + vel.X,
                transform.position.Y + vel.Y
            );
        }
    }
}
