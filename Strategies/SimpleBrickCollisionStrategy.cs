using Bricker.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricker.Strategies
{
    internal class SimpleBrickCollisionStrategy : IBrickCollisionStrategy
    {
        private BrickerGameManager _gameManager;

        public SimpleBrickCollisionStrategy(BrickerGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnCollision(Brick brick, IGameObject otherObj)
        {
            brick.IsDestroyed = true;
            brick.Visible = false;
        }
    }
}
