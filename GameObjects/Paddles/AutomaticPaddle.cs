using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bricker.GameObjects.Paddles
{
    internal class AutomaticPaddle: Paddle
    {
        public AutomaticPaddle(BrickerGameManager game, Vector2 velocity, Texture2D texture, Vector2 position) : base(game, velocity, texture, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
