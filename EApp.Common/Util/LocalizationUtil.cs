using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace EApp.Common.Util
{
    public static class LocalizationUtil
    {
        private const string NUMERIC_COMMA = ",";

        private const string DECIMAL_SUFFIX_MILLIONS = "M";

        private const string DECIMAL_SUFFIX_THOUSAND = "K";

        private static System.Globalization.CultureInfo cultureInfo =
            new CultureInfo("en-US", false);

        //number formatter
        private static System.Globalization.NumberFormatInfo numberFormat =
            cultureInfo.NumberFormat;

        //date time formatter
        private static System.Globalization.DateTimeFormatInfo timeFormat =
            cultureInfo.DateTimeFormat;

        private static object lockObj = new object();

        public static string FormatRateToString(decimal? rateValue, int decimalNum)
        {
            if (!rateValue.HasValue)
            {
                return null;
            }

            decimal tempRateValue = rateValue.Value * 100;

            lock (lockObj)
            {
                numberFormat.NumberDecimalDigits = decimalNum;

                return tempRateValue.ToString("N", numberFormat);
            }
        }

        public static decimal? FormatStringToRate(string rateString)
        {
            if (rateString == null || rateString.Trim().Length == 0)
                return null;

            decimal retDecimal;

            lock (lockObj)
            {
                numberFormat.NumberDecimalDigits = 2;

                retDecimal = decimal.Parse(rateString.Trim(), numberFormat);
            }

            return FormatTo4Decimals(retDecimal / 100);
        }

        public static decimal? FormatToDecimals(decimal? originalValue, int decimalNum)
        {
            if (!originalValue.HasValue)
            {
                return null;
            }

            return decimal.Round(originalValue.Value, decimalNum, MidpointRounding.AwayFromZero);
        }

        public static decimal? FormatTo2Decimals(decimal originalValue)
        {
            return FormatToDecimals(originalValue, 2);
        }

        public static decimal? FormatTo4Decimals(decimal originalValue)
        {
            return FormatToDecimals(originalValue, 4);
        }

        public static decimal? FormatStringToDecimal(string decimalString, int decimalNum)
        {
            if (decimalString == null || decimalString.Trim().Length == 0)
                return null;

            decimalString = decimalString.Trim().Replace(NUMERIC_COMMA, string.Empty).Replace(
                            DECIMAL_SUFFIX_MILLIONS, string.Empty).Replace(
                            DECIMAL_SUFFIX_THOUSAND, string.Empty);

            decimal retDecimal;

            lock (lockObj)
            {
                numberFormat.NumberDecimalDigits = decimalNum;

                retDecimal = Decimal.Parse(decimalString, numberFormat);
            }

            return FormatToDecimals(retDecimal, decimalNum);
        }

        public static decimal? FormatStringToDecimal(string decimalString)
        {
            if (decimalString == null || decimalString.Trim().Length == 0)
                return null;

            decimalString = decimalString.Trim().Replace(NUMERIC_COMMA, string.Empty).Replace(
                            DECIMAL_SUFFIX_MILLIONS, string.Empty).Replace(
                            DECIMAL_SUFFIX_THOUSAND, string.Empty);

            decimal retDecimal;

            lock (lockObj)
            {
                NumberFormatInfo numberFormat = cultureInfo.NumberFormat;

                Decimal.TryParse(decimalString, NumberStyles.None, numberFormat, out retDecimal);
            }

            return retDecimal;
        }

        public static decimal? FormatStringTo2Decimal(string decimalString)
        {
            return FormatStringToDecimal(decimalString, 2);
        }

        public static decimal? FormatStringTo4Decimal(string decimalString)
        {
            return FormatStringToDecimal(decimalString, 4);
        }

        public static decimal? FormatStringTo6Decimal(string decimalString)
        {
            return FormatStringToDecimal(decimalString, 6);
        }

    }
}
