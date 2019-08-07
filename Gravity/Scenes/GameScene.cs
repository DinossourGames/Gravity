using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravity.Assets;
using Gravity.Assets.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gravity.Scenes
{
    public class GameScene : Scene
    {
        private Texture2D mouseTexture;
        private Vector2 mousePos;
        List<Component> components;
        public GameScene(ContentManager content, GraphicsDevice graphicsDevice, Game1 game) : base(content, graphicsDevice, game)
        {
        }


        public override void Start()
        {
            mousePos = new Vector2();
            game.IsMouseVisible = false;
            mouseTexture = content.Load<Texture2D>("Sprites/crosshair");
            components = new List<Component>() { new Player(content.Load<Texture2D>("Sprites/player"), new Vector2(300, 300)) { scale = 5 } };
        }

        public override void Update(GameTime gameTime)
        {
            components.ForEach(i => i.Update(gameTime));

            var mouse = Mouse.GetState();

            mousePos.X = mouse.X;
            mousePos.Y = mouse.Y;
        }
        public override void FixedUpdate(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(BackgroundColor);
            components.ForEach(i => i.Draw(gameTime, spriteBatch));
            spriteBatch.Draw(mouseTexture, mousePos, null, Color.White, 0, new Vector2(mouseTexture.Width / 2, mouseTexture.Height / 2), .1f, SpriteEffects.None, 0);
        }



    }
}
