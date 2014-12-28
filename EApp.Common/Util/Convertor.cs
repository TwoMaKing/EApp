using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EApp.Common.Util
{
    public sealed class Convertor
    {
        private Convertor() { }

        public static int? ConvertToInteger(object value) 
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            try
            {
                int intValue = Convert.ToInt32(value);

                return intValue;
            }
            catch
            {
                return null;
            }
        }

        public static int? ConvertToInteger(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            int intValue;

            bool parseSuccess = int.TryParse(value, out intValue);

            if (!parseSuccess)
            {
                return null;
            }

            return intValue;
        }

        public static DateTime? ConvertToDateTime(object value)
        {
            return Convertor.ConvertToDateTime(value, CultureInfo.CurrentCulture.DateTimeFormat);
        }

        public static DateTime? ConvertToDateTime(object value, IFormatProvider formatProvider) 
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return null;
            }

            DateTime resultDateTime;

            bool parsedSuccessfully = DateTime.TryParse(value.ToString(),
                                                        formatProvider, 
                                                        DateTimeStyles.None, 
                                                        out resultDateTime);

            if (!parsedSuccessfully)
            {
                return null;
            }

            return DateTimeUtil.ToDateTime(resultDateTime);
        }

    }
}
