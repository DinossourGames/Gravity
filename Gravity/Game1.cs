using Gravity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gravity
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D bg;
        private Scene currentScene;
        private Scene nextScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            IsMouseVisible = true;
            graphics.ApplyChanges();
        }

        public void ChangeScene(Scene scene)
        {
            nextScene = scene;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentScene = new MenuScene(Content, graphics.GraphicsDevice, this);
        }

        protected override void Update(GameTime gameTime)
        {

            if (nextScene != null)
            {
                currentScene = nextScene;
                nextScene = null;
            }

            currentScene.Update(gameTime);
            currentScene.FixedUpdate(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {

            currentScene.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
