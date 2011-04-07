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
using System.Windows.Data;
using System.Globalization;
using ECollegeAPI.Model;

namespace eCollegeWP7.Util.Converters
{
    public class CourseIdToAllTopicsLinkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is long)
            {
                var v = (long)value;
                Course c;
                if (App.Model.Courses.CourseIdMap.TryGetValue(v,out c))
                {
                    return string.Format("SEE ALL TOPICS FOR {0}",c.Title.ToUpper());
                }
            }
            return "SEE ALL TOPICS FOR ?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
