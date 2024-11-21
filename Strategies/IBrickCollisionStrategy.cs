using Bricker.GameObjects;
using System.Collections.Generic;

namespace Bricker.Strategies
{
    public interface IBrickCollisionStrategy
    {
        public void OnCollision(Brick brick, IGameObject otherObj);
    }
}