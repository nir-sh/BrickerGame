using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;

namespace Bricker.GameObjects
{
    public interface IGameObject : IGameComponent, IDrawable, IUpdateable
    {
        bool IsDestroyed { get; set; }

        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
