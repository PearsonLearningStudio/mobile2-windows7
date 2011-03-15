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
    }
}
