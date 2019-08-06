using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gravity.Scenes
{
    public class GameScene : Scene
    {
        public GameScene(ContentManager content, GraphicsDevice graphicsDevice, Game1 game) : base(content, graphicsDevice, game)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(BackgroundColor);
            spriteBatch.Begin();
            spriteBatch.End();
        }

        public override void FixedUpdate(GameTime gameTime)
        {
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
