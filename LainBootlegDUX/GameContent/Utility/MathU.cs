using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
