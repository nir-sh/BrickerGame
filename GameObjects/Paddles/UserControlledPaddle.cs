using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricker.GameObjects.Paddles
{
    internal class UserControlledPaddle : Paddle
    {
        public UserControlledPaddle(BrickerGameManager game, Vector2 velocity, Texture2D texture, Vector2 position) : base(game, velocity, texture, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            //todo: write update by user input
            var kstate = Keyboard.GetState();


            if (kstate.IsKeyDown(Keys.Left) && _rectangle.Left > 0)
            {
                Position = new Point2((_rectangle.Position - Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds).X, _rectangle.Y);
                _rectangle = new RectangleF(Position, _rectangle.Size);
            }
            if (kstate.IsKeyDown(Keys.Right) && _rectangle.Right < GameManager.GraphicsDevice.PresentationParameters.BackBufferWidth)
            {
                Position = new Point2((_rectangle.Position + Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds).X, _rectangle.Y);

                _rectangle = new RectangleF(Position, _rectangle.Size);
            }
            base.Update(gameTime);
        }
    }
}
