using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Nez.Sprites;

namespace FWCards.Components.Map
{
    public class FillCameraComponent : Component, IUpdatable
    {
        private Sprite spriteComp;

        public override void onAddedToEntity()
        {
            spriteComp = entity.getComponent<Sprite>();
        }

        public void update()
        {
            var camera = entity.scene.camera;
            
        }
    }
}
