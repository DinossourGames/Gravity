﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gravity.Assets
{
    public abstract class Component
    {
        public Type Type = typeof(Component);
        public Guid ID { get; set; } = Guid.NewGuid();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
