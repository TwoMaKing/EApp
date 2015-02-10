using System;
using System.Globalization;

namespace EApp.Common.Util
{
    public static class DateTimeUtils
    {
        private static CultureInfo cultureInfo = CultureInfo.CurrentCulture;

        //date time formatter
        private static DateTimeFormatInfo timeFormat = cultureInfo.DateTimeFormat;

        private static string[] shortMonthArray = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public const string TimeFormat_MMDDYYYY = "MM/dd/yyyy";
        public const string TimeFormat_MMDDYYYYHHMMSS = "MM/dd/yyyy HH:mm:ss";

        public const string TimeFormat_STANDARD = "yyyy/MM/dd HH:mm:ss";
        public const string TimeFormat_STANDARD_LONG = "yyyy/MM/dd HH:mm:ss.fff";
        public const string TimeFormat_STANDARD_SHORT = "yyyy/MM/dd HH:mm";
        
        public const string TimeFormat_DDMMMYYYYHHMM = "dd MMM yyyy HH:mm";
        public const string TimeFormat_MMYYYY = "MM/yyyy";

        public static string ToDateTimeString(DateTime? time, string format)
        {
            if (!time.HasValue)
            {
                return null;
            }

            return time.Value.ToString(format, DateTimeFormatInfo.InvariantInfo);
        }

        public static DateTime? ToDateTime(DateTime? time, string format)
        {
            string newTimeStr = ToDateTimeString(time, format);

            return ToDateTime(newTimeStr);
        }

        public static DateTime? ToDateTime(string time)
        {
            if (string.IsNullOrEmpty(time) ||
                string.IsNullOrWhiteSpace(time))
            {
                return null;
            }

            DateTime resultDateTime;

            bool parsed = DateTime.TryParse(time.ToString(),
                                            timeFormat,
                                            DateTimeStyles.None,
                                            out resultDateTime);

            if (!parsed)
            {
                return null;
            }

            return ToDateTime(resultDateTime);
        }

        public static DateTime? ToDateTime(object time)
        {
            if (time == null ||
                time == DBNull.Value)
            {
                return null;
            }

            DateTime resultDateTime;

            bool parsed = DateTime.TryParse(time.ToString(), 
                                            DateTimeFormatInfo.InvariantInfo, 
                                            DateTimeStyles.None, 
                                            out resultDateTime);

            if (!parsed)
            {
                return null;
            }

            return ToDateTime(resultDateTime);
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
