using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gravity.Scenes
{
    public class MenuScene : Scene
    {
        private Texture2D background;

        #region Constructors
        public MenuScene(ContentManager content, GraphicsDevice graphicsDevice, Game1 game) : base(content, graphicsDevice, game)
        {
           
        }
        public MenuScene(ContentManager content, GraphicsDevice graphicsDevice, Game1 game, Color backgroundColor) : base(content, graphicsDevice, game)
        {
            BackgroundColor = backgroundColor;
        }
        #endregion

        public override void Start()
        {
            background = content.Load<Texture2D>("Sprites/bg_menu");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(BackgroundColor);

            spriteBatch.Draw(background, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                game.ChangeScene(new GameScene(content, graphicsDevice, game) { BackgroundColor = Color.White});
            }
        }
        public override void FixedUpdate(GameTime gameTime)
        {
            
        }

    }
}
