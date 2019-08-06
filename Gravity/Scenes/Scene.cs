using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Scenes
{
    public abstract class Scene
    {
        public Color BackgroundColor { get; set; }

        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected Game1 game;

        protected Scene(ContentManager content, GraphicsDevice graphicsDevice, Game1 game)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.game = game;
            BackgroundColor = Color.Purple;
            Start();
        }
        public abstract void Start();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        public abstract void FixedUpdate(GameTime gameTime);


    }
}
