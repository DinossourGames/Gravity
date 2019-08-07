using Gravity.Assets.Scripts;
using Gravity.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Assets
{
    public abstract class Weapon : Sprite
    {

        private Vector2 distance;
        public bool picked;
        Player parent;
        private float Cadency = 1;
        private float cooldown = 0;
        private float cooldownTime;

        public Color Colour { get; private set; } = Color.White;

        public Weapon(Texture2D texture) : base(texture)
        {
            distance = new Vector2();
            Type = typeof(Weapon);
            Origin = new Vector2(_texture.Width * .9f, _texture.Height / 2);
            cooldownTime = 1000 / Cadency;
        }

        public Weapon(Texture2D texture,float cadencia,Color color) : base(texture)
        {
            distance = new Vector2();
            Type = typeof(Weapon);
            Origin = new Vector2(_texture.Width * .9f, _texture.Height / 2);
            Cadency = cadencia;
            Colour = color;
            cooldownTime = 1000 / Cadency;
        }

        public Weapon Pick(Player parent)
        {
            picked = true;
            this.parent = parent;
            return this;
        }
        public override void Update(GameTime gameTime)
        {

            if (picked)
            {
                //Rotate the sprite to flip;
                var mouse = Mouse.GetState();
                distance.X = Position.X - mouse.X;
                distance.Y = Position.Y - mouse.Y;
                _rotation = (float)Math.Atan2(distance.Y, distance.X);

                var direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

                Position.Y = parent.Position.Y - 25;

                if (parent.FacingRight)
                    Position.X = parent.Position.X - 60;
                else
                    Position.X = parent.Position.X + 60;


                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds >= cooldown)
                    {
                        cooldown = (float)gameTime.TotalGameTime.TotalMilliseconds + cooldownTime;
                        Shoot();
                    }
                }

            }

        }

        private void Shoot()
        {
            Debug.WriteLine("Pew Pew");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (picked)
            {
                if (parent.FacingRight)
                    spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, scale, SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, scale, SpriteEffects.FlipVertically, 0);
            }
            else
            {
                spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, scale, SpriteEffects.None, 0);

            }

        }
    }
}
