using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TK1.Reflection
{
    public class TypeCheck
    {
        public static bool IsBool(object value)
        {
            return (value as bool?) != null;
        }
        public static bool IsCollection(object value)
        {
            bool result = false;
            result = (value as IEnumerable) != null;
            if (result)
                result = (value as string) == null;
            return result;
        }
        public static bool IsDouble(object value)
        {
            return (value as double?) != null;
        }
        public static bool IsEnum(object value)
        {
            return value.GetType().IsEnum;
        }
        public static bool IsInt(object value)
        {
            return (value as int?) != null;
        }
        public static bool IsFloat(object value)
        {
            return (value as float?) != null;
        }
        public static bool IsNumber(object value)
        {
            return IsDouble(value) | IsInt(value) | IsFloat(value);
        }
        public static bool IsString(object value)
        {
            return (value as string) != null;
        }

    }
}
