using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace Bodardr.Utility.Runtime
{
    public static class EnumUtility
    {
        public static TEnum RandomEnumValue<TEnum>() where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum));
            var index = Random.Range(0, values.Length);
            return (TEnum)values.GetValue(index);
        }

        public static TEnum[] FlagsToArray<TEnum>(this TEnum val) where TEnum : Enum
        {
            if (!val.HasFlag(val))
                return Array.Empty<TEnum>();

            var intValue = (int)(object)val;
            var values = Enum.GetValues(typeof(TEnum)).Cast<int>();
            return values.Where(x => (x & intValue) != 0).Cast<TEnum>().ToArray();
        }
    }
}