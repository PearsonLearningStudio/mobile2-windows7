using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace eCollegeWP7.Util
{
    public class DateTimeUtil
    {

        public static string FriendlyDate(DateTime dt)
        {
            var dtStr = dt.ToString("MMM d yyyy");

            if (DateTime.Today.ToString("MMM d yyyy") == dtStr)
            {
                return "Today";
            }
            if (DateTime.Today.AddDays(-1.0).ToString("MMM d yyyy") == dtStr)
            {
                return "Yesterday";
            }
            if (dt.Year != DateTime.Today.Year)
            {
                return dtStr;
            }
            else
            {
                return dt.ToString("MMM d");
            }
        }

        /*  Today 12:34 PM
            Yesterday 12:34 PM
            February 13 12:34 PM
            December 13, 2010 12:34 PM
            */

        public static string LongFriendlyDate(DateTime dt)
        {
            var dtStr = dt.ToString("MMM d yyyy");

            if (DateTime.Today.ToString("MMM d yyyy") == dtStr)
            {
                return "Today " + dt.ToString("h:mm tt");
            }
            if (DateTime.Today.AddDays(-1.0).ToString("MMM d yyyy") == dtStr)
            {
                return "Yesterday " + dt.ToString("h:mm tt");
            }
            if (dt.Year != DateTime.Today.Year)
            {
                return dt.ToString("MMMM d, yyyy h:mm tt");
            }
            else
            {
                return dt.ToString("MMMM d h:mm tt");
            }
        }
    }
}
