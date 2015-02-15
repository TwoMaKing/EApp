using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Util
{
    public static class StringExtension
    {
        public static bool HasValue(this string @string)
        {
            return !string.IsNullOrEmpty(@string) &&
                   !string.IsNullOrWhiteSpace(@string);
        }
    }
}
