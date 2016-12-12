using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Extension
{
    public static class MathExtension
    {
        public static decimal Abs(this decimal val)
        {
            return Math.Abs(val);
        }

        public static double Abs(this double val)
        {
            return Math.Abs(val);
        }


        public static int Abs(this int val)
        {
            return Math.Abs(val);
        }

        public static float Abs(this float val)
        {
            return Math.Abs(val);
        }

        public static short Abs(this short val)
        {
            return Math.Abs(val);
        }

        public static long Abs(this long val)
        {
            return Math.Abs(val);
        }

        /// <summary>
        /// 排列    x 的阶层
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        public static int Recursive(this int val)
        {
            if (val == 1) return 1;

            return (val)*Recursive(val - 1);
        }

        public static decimal Round(this decimal val, int dPoint = 2,
            MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return System.Math.Round(val, dPoint, rounding);
        }

    }
}
