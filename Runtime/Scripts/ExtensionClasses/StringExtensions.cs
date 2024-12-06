using System;
using System.Text;

namespace Bodardr.Utility.Runtime
{
    public static class StringExtensions
    {
        public static string RemoveCloneSuffix(this string str)
        {
            var indexOf = str.IndexOf("(Clone)", StringComparison.InvariantCulture);
            return str[..(indexOf)];
        }

        public static string GetMangledName(this string str)
        {
            StringBuilder strBuilder = new StringBuilder();

            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();

            strBuilder.Append(char.ToUpper(str[0]));

            var spaceChar = ' ';
            for (int i = 1; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                    strBuilder.Append(spaceChar);

                strBuilder.Append(str[i]);
            }

            return strBuilder.ToString();
        }

        public static string ToLowerCamelCase(this string upperCamelCaseStr)
        {
            if (string.IsNullOrWhiteSpace(upperCamelCaseStr))
                return string.Empty;

            var strBuilder = new StringBuilder(upperCamelCaseStr)
            {
                [0] = char.ToLower(upperCamelCaseStr[0])
            };

            return strBuilder.ToString();
        }
    }
}