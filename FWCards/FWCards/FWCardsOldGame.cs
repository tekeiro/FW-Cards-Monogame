using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCards.Config;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace FWCards
{
    public class FWCardsOldGame : Nez.Core
    {

        public FWCardsOldGame() : base(windowTitle: "Fw Cards", 
            width: 800*2, height: 600*2)
        { }

        protected override void Initialize()
        {
            base.Initialize();
            Window.AllowUserResizing = true;

            Debug.log("Init Input");
            var inputService = new InputService();
            Core.services.AddService(typeof(InputService), inputService);

            Debug.log("Screen Size");
            Screen.setSize(800, 600);

            Debug.log("Create Scene");
            var sc = Scene.createWithDefaultRenderer(Color.CornflowerBlue);
            Debug.log("Added Entity mapEntity");
            var mapEntity = sc.createEntity("map");
            Debug.log("Load Map");
            var map = sc.content.Load<TiledMap>(Assets.MAP01);
            Debug.log("Add Component");
            mapEntity.addComponent(new TiledMapComponent(map, "Terrain"));
            

            scene = sc;
        }
    }
}
