using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Gravity.Assets.Scripts
{
    public class Player : Sprite
    {
        private Vector2 distance;

        public Player(Texture2D texture) : base(texture) { }
        public Player(Texture2D texture, Vector2 position) : base(texture)
        {
            Position = position;
        }


        public override void Update(GameTime gameTime)
        {
            //mouse.X - (Position.X + _texture.Width /2)
            var mouse = Mouse.GetState();
            distance.X = Position.X - mouse.X;
            distance.Y = Position.Y - mouse.Y;
            _rotation = (float)Math.Atan2(0, distance.X);


            var direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

           



        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (distance.X > 0)
                spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, scale, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, scale, SpriteEffects.FlipVertically, 0);
            

            
            

        }

    }
}