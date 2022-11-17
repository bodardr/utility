using System;

namespace Bodardr.Utility.Runtime
{
    public static class TypeExtensions
    {
        public static bool IsStaticType(this Type type)
        {
            if (type == null)
                return false;
            
            return type.IsAbstract && type.IsSealed;
        }
        
        /// <summary>
        /// Taken from : https://stackoverflow.com/questions/1749966/c-sharp-how-to-determine-whether-a-type-is-a-number
        /// </summary>
        /// <param name="o"></param>
        /// <returns>if it is a numeric type</returns>
        public static bool IsNumericType(this object o)
        {   
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}