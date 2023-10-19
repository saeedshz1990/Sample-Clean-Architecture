using System;

namespace Sample_Clean_Architecture.Web.Utilities
{
    public static class Pub
    {
        public static string ToCustomFormat(this DateTime yourTime, string customFormat)
        {
            // Get the following var out of the database
            // String format = "yyyy/MM/dd hh:mm:sszzz";

            // String format = "yyyy/MM/dd";
            string result = "";
            try
            {
                string format = customFormat;

                // Converts the local DateTime to a string 
                // using the custom format string and display.
                result = yourTime == Convert.ToDateTime("2079-01-01") ? "" : yourTime.ToString(format);
            }
            catch
            {
                result = Convert.ToDateTime("2079-01-01").ToShortDateString();
            }
            return result;
        }
        public static string ToSystemFormat(this string yourTime, string customFormat)
        {
            string result = "";
            string systemFormat = "yyyy/MM/dd";
            try
            {
                string currectTime = yourTime.Substring(customFormat.IndexOf("yyyy"), 4) + "/" + yourTime.Substring(customFormat.IndexOf("mm"), 2) + "/" + yourTime.Substring(customFormat.IndexOf("dd"), 2);
                result = Convert.ToDateTime(currectTime).ToString(systemFormat);
            }
            catch
            {
                result = Convert.ToDateTime("2079-01-01").ToString(systemFormat);
            }
            return result;
        }
        public static DateTime ToSystemFormatV2(this string yourTime, string customFormat)
        {
            DateTime result = DateTime.MinValue;

            try
            {
                string currectTime = yourTime.Substring(customFormat.IndexOf("yyyy"), 4) + "/" + yourTime.Substring(customFormat.IndexOf("mm"), 2) + "/" + yourTime.Substring(customFormat.IndexOf("dd"), 2);
                result = Convert.ToDateTime(currectTime);
            }
            catch
            {
                result = Convert.ToDateTime("2079-01-01");
            }
            return result;
        }
    }
}
