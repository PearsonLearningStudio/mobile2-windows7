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
            var topicsByCourse = values[0] as List<Group<UserDiscussionTopic>>;
            var course = values[1] as Course;

            if (course == null || course.ID == -1 || topicsByCourse == null) //c.ID == -1 means all topics
            {
                return topicsByCourse;
            }
            else
            {
                //var res = from t in service.Result
                //                      group t by t.Topic.ContainerInfo.CourseID.ToString()
                //                          into r
                //                          orderby r.Key
                //                          select new Group<UserDiscussionTopic>(r.Key, r);

                var res = from t in topicsByCourse
                          where t.Items[0].Topic.ContainerInfo.CourseID == course.ID
                          select new Group<UserDiscussionTopic>(t.GroupId,t.Items);


                return res;

                //foreach (var topicGroup in topicsByCourse)
                //{
                //    if (topicGroup.Items != null && topicGroup.Items.Count > 0)
                //    {
                //        if (topicGroup.Items[0].Topic.ContainerInfo.CourseID == course.ID)
                //        {
                //            var res2 = new List<Group<UserDiscussionTopic>>();
                //            res2.Add(topicGroup);
                //            return res2;
                //        }
                //    }
                //}

                //return new List<Group<UserDiscussionTopic>>();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
