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
using eCollegeWP7.Util.MultiBinding;
using System.Windows.Data;
using ECollegeAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace eCollegeWP7.Util.Converters
{
    public class FilterTopicByCourseConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var topics = values[0] as List<UserDiscussionTopic>;
            var course = values[1] as Course;

            if (course == null || course.ID == -1 || topics == null) //c.ID == -1 means all topics
            {
                return topics;
            }
            else
            {
                return (from t in topics where t.Topic.ContainerInfo.CourseID.Equals(course.ID) select t).ToList<UserDiscussionTopic>();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
