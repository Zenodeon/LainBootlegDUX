using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace LainBootlegDUX.GameContent
{
    public static class MathU
    {
        public static float MapClampRanged(float value, float InMinimum, float InMaximum, float OutMinimum, float OutMaximum)
        {
            var InRange = InMaximum - InMinimum;
            var OutRange = OutMaximum - OutMinimum;
            return ((value - InMinimum) * OutRange / InRange) + OutMinimum;
        }

        public static Vector2 MapClampRanged(float value, float InMinimum, float InMaximum, Vector2 OutMinimum, Vector2 OutMaximum)
            => MapClampRanged(Vector2.One * value, Vector2.One * InMinimum, Vector2.One * InMaximum, OutMinimum, OutMaximum);

        public static Vector2 MapClampRanged(float value, Vector2 InMinimum, Vector2 InMaximum, Vector2 OutMinimum, Vector2 OutMaximum)
            => MapClampRanged(Vector2.One * value, InMinimum, InMaximum, OutMinimum, OutMaximum);

        public static Vector2 MapClampRanged(Vector2 value, Vector2 InMinimum, Vector2 InMaximum, Vector2 OutMinimum, Vector2 OutMaximum)
        {
            float x = MapClampRanged(value.x, InMinimum.x, InMaximum.x, OutMinimum.x, OutMaximum.x);
            float y = MapClampRanged(value.y, InMinimum.y, InMaximum.y, OutMinimum.y, OutMaximum.y);

            return new Vector2(x, y);
        }

        public static float RoundOff(this float value, float roundToValue, RoundType roundType = RoundType.Nearest)
        {
            float valueToRound = value / roundToValue;
            return RoundOff(valueToRound, roundType) * roundToValue;
        }

        public static int RoundOff(this float value, RoundType roundType = RoundType.Nearest)
        {
            switch (roundType)
            {
                case RoundType.Ceil:
                    return (int)MathF.Ceiling(value);

                case RoundType.Nearest:
                    return (int)MathF.Round(value);

                case RoundType.Floor:
                    return (int)MathF.Floor(value);

                default:
                    return -1;
            }
        }

        public static Vector2 SnapToGrid(this Vector2 vector, Vector2 size, RoundType roundType)
        {
            return new Vector2(vector.x.RoundOff(size.x, roundType), vector.y.RoundOff(size.y, roundType));
        }

        public static Vector2Int RoundToVectorInt(this Vector2 vector) => new Vector2Int((int)vector.x, (int)vector.y);
        public static Vector2Int RoundToVector2Int(this Vector2 vector, RoundType roundType) => new Vector2Int(vector.x.RoundOff(roundType), vector.y.RoundOff(roundType));

        public enum RoundType
        {
            Ceil,
            Nearest,
            Floor
        }
    }
}
