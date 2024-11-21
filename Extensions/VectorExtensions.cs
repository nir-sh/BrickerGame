using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bricker.Extensions;

internal static class VectorExtensions
{
    public static Vector2 Y_AxisFlipped(this Vector2 vec)
    {
        return new Vector2(vec.X, vec.Y * -1);
    }

    public static Vector2 X_AxisFlipped(this Vector2 vec)
    {
        return new Vector2(vec.X * -1, vec.Y);
    }

}
