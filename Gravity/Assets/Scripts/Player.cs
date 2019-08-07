using Gravity.Scenes;
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
        private Weapon weapon;
        public bool FacingRight;
        public Color Colour;
        public bool isMain;
        public Player(Texture2D texture) : base(texture) {
            Type = typeof(Player);
        }
        public Player(Texture2D texture, Vector2 position) : base(texture)
        {
            Type = typeof(Player);
            Position = position;
            LinearVelocity = 300;
        }

        public override void Update(GameTime gameTime)
        {
            isMain = ID.ToString() == GameScene.PlayerID;

            //var direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
            if (isMain)
            {
                Move(gameTime);
                Look();
            }


            CollisionCheck();

        }

        private void Look()
        {
            //Rotate the sprite to flip;
            var mouse = Mouse.GetState();
            distance.X = Position.X - mouse.X;
            distance.Y = Position.Y - mouse.Y;
            _rotation = (float)Math.Atan2(0, distance.X);
            FacingRight = distance.X > 0 ? true : false;
        }

        private void Move(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;


            var move = new Vector2(0, 0);

            if (keyboard.IsKeyDown(Keys.D))
                move.X = 1;

            if (keyboard.IsKeyDown(Keys.A))
                move.X = -1;

            if (keyboard.IsKeyDown(Keys.W))
                move.Y = -1;

            if (keyboard.IsKeyDown(Keys.S))
                move.Y = 1;

            if (move.X != 0 && move.Y != 0)
                Position += move * .8f * LinearVelocity * delta;
            else
                Position += move * LinearVelocity * delta;
        }

        private void CollisionCheck()
        {
            foreach (var item in GameScene.components)
            {
                if (item == this)
                    continue;

                if (item.Type == typeof(Weapon))
                {
                    var wp = (Weapon)item;
                    if (wp.HitBox.Intersects(this.HitBox) && wp.picked == false)
                    {
                        weapon = wp.Pick(this);
                    }
                }

            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (FacingRight)
                spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, scale, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, scale, SpriteEffects.FlipVertically, 0);

        }

    }
}