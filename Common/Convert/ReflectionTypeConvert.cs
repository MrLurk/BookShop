using System;
namespace Common.Convert
{
    public class ReflectionTypeConvert
    {
        public static dynamic Convert(Type toType, object value)
        {
            if (toType == typeof(Int32))
                return System.Convert.ToInt32(value);
            if (toType == typeof(Int16))
                return System.Convert.ToInt16(value);
            if (toType == typeof(Int64))
                return System.Convert.ToInt64(value);
            if (toType == typeof(byte))
                return System.Convert.ToByte(value);
            if (toType == typeof(string))
                return System.Convert.ToString(value);
            if (toType == typeof(bool))
                return System.Convert.ToBoolean(value);
            if (toType == typeof(float) || toType == typeof(double))
                return System.Convert.ToDouble(value);
            if (toType == typeof(decimal))
                return System.Convert.ToDecimal(value);
            if (toType == typeof(char))
                return System.Convert.ToChar(value);
            throw new Exception("格式转换异常！");
        }
    }
}
