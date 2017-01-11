using FWCards.Config;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Scene mapScene;

        public FWCardsGame() : base(windowTitle: "FW Cards")
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Window.AllowUserResizing = true;

            var inputService = new InputService();
            Core.services.AddService(typeof(InputService), inputService);


            Screen.setSize(800, 600);

            mapScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);
            mapScene.setDesignResolution(800, 600, Scene.SceneResolutionPolicy.ExactFit);

            var tiledEntity = mapScene.createEntity("map");
            var tiledMap = mapScene.content.Load<TiledMap>(@"Maps/map01");
            tiledEntity.addComponent(new TiledMapComponent(tiledMap));

            var grayEntity = EntityFactory.CreateGrayMapPlayer(mapScene);

            scene = mapScene;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
