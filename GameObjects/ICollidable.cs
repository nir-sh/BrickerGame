using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricker.GameObjects;

internal interface ICollidableRectangle
{
    RectangleF Bounds { get; }
    void Accept(ICollisionVisitor visitor);
}
