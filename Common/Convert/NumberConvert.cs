using System;

namespace Common.Convert
{
    public static class  NumberConvert
    {
        public static int ToInt(this string str)
        {
            return System.Convert.ToInt32(str);
        }

        public static long ToLong(this string str)
        {
            return System.Convert.ToInt64(str);
        }

        public static decimal ToDecimal(this string str)
        {
            return System.Convert.ToDecimal(str);
        }

        public static double ToDouble(this string str)
        {
            return System.Convert.ToDouble(str);
        }
    }
}
