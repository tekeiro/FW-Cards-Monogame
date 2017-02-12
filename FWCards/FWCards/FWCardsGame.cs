using FWCards.Components.Player;
using FWCards.Config;
using FWCards.Model;
using FWCards.Model.Cards;
using FWCards.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Tiled;

namespace FWCards
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FWCardsGame : Nez.Core
    {
        private MapScene mapScene;

        public FWCardsGame() : base(windowTitle: "FW Cards", width: 800, height: 600)
        { }
        
        
        protected override void Initialize()
        {
            base.Initialize();
            Window.AllowUserResizing = true;

            Debug.log("Setup Input");
            var inputService = new InputService();
            Core.services.AddService(typeof(InputService), inputService);

            Debug.log("Setup Screen");
            Screen.setSize(800, 600);

            Debug.log("Create Scene");
            mapScene = new MapScene();
            Debug.log("Setup Scene");
            mapScene.setDesignResolution(800, 600, Scene.SceneResolutionPolicy.ExactFit);
            Debug.log("Set Map");
            mapScene.changeMap(Assets.MAP01);

            Debug.log("Added Player Entity");
            var grayEntity = EntityFactory.CreateGrayMapPlayer(mapScene);

            mapScene.setMapStartPositionForEntity(grayEntity);
            var mapComp = grayEntity.getComponent<MapPlayerComponent>();

            GameDB gameDb = new GameDB();
            Core.services.AddService(typeof(GameDB), gameDb);
            gameDb.loadDatabase();

            GameData gameData = new GameData();
            Core.services.AddService(typeof(GameData), gameData);
            gameData.initialize();

            scene = mapScene;
        }
    }
}
