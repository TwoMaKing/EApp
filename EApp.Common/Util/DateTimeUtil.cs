using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace EApp.Common.Util
{
    public sealed class DateTimeUtil
    {
        private DateTimeUtil() { }

        private static System.Globalization.CultureInfo cultureInfo =
            new CultureInfo("en-US", false);

        //date time formatter
        private static System.Globalization.DateTimeFormatInfo timeFormat =
            cultureInfo.DateTimeFormat;

        private static string[] shortMonthArray = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public const string TimeFormat_MMDDYYYY = "MM/dd/yyyy";
        public const string TimeFormat_MMDDYYYYHHMMSS = "MM/dd/yyyy HH:mm:ss";

        public const string TIMEFORMAT_STANDARD = "yyyy/MM/dd HH:mm:ss";
        public const string TIMEFORMAT_STANDARD_LONG = "yyyy/MM/dd HH:mm:ss.fff";
        public const string TIMEFORMAT_STANDARD_SHORT = "yyyy/MM/dd HH:mm";
        
        public const string TimeFormat_DDMMMYYYYHHMM = "dd MMM yyyy HH:mm";
        public const string TimeFormat_MMYYYY = "MM/yyyy";


        public static string ToDateTimeString(DateTime? time, string format)
        {
            if (!time.HasValue)
            {
                return null;
            }

            return time.Value.ToString(format, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(DateTime? time, string format)
        {
            string newTimeStr = ToDateTimeString(time, format);

            return ToDateTime(newTimeStr);
        }


        public static DateTime? ToDateTime(string time)
        {
            try
            {
                if (string.IsNullOrEmpty(time) ||
                    string.IsNullOrWhiteSpace(time))
                {
                    return null;
                }

                DateTime date = DateTime.Parse(time, timeFormat);

                return ToDateTime(date);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? ToDateTime(DateTime? time)
        {
            if (!time.HasValue)
            {
                return time;
            }

            DateTime newTime = new DateTime(
                time.Value.Year,
                time.Value.Month,
                time.Value.Day,
                time.Value.Hour,
                time.Value.Minute,
                time.Value.Second);

            return newTime;
        }

    }
}
