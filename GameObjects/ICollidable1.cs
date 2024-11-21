using MonoGame.Extended;

namespace Bricker.GameObjects
{
    internal interface ICollidable
    {
        IShapeF Bounds { get; }
    }
}