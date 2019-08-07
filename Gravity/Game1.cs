using Gravity.Assets.Scripts;
using Gravity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Gravity
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Scene currentScene;
        private Scene nextScene;
        private string[] args;
        private NetworkManager netManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            IsMouseVisible = true;
            graphics.ApplyChanges();


            args = Environment.GetCommandLineArgs();

            //if (args.Length > 1)
            //{
            //    netManager = new NetworkManager(args[1] == "0" ? true : false) { Hostname = args.Length > 2 ? args[2] : "localhost"};
            //}
            //else
            //{
            //    netManager = new NetworkManager(true);
            //}

            netManager = new NetworkManager(false) { Hostname = "localhost"};
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
            currentScene = new GameScene(Content, graphics.GraphicsDevice, this) { BackgroundColor = Color.Black };
        }

        protected override void Update(GameTime gameTime)
        {
            netManager.Update();
            if (Keyboard.GetState().IsKeyDown(Keys.F11))
                graphics.ToggleFullScreen();



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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            currentScene.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
