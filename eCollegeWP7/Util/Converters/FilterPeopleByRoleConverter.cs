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
using eCollegeWP7.ViewModels;

namespace eCollegeWP7.Util.Converters
{
    public class FilterPeopleByRoleConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var people = values[0] as List<RosterUser>;

            if (people == null) return null;

            var role = values[1] as string;
            string friendlyRole = null;

            if ("instructors".Equals(role))
            {
                friendlyRole = "Instructor";
            } else if ("students".Equals(role))
            {
                friendlyRole = "Student";
            }

            if (friendlyRole == null)
            {
                return (from u in people
                            orderby u.LastName
                            group u by u.LastNameFirstChar
                                into g
                                orderby g.Key
                                select new Group<RosterUser>(g.Key, g)).ToList();
            }

            return (from u in people
                    orderby u.LastName where u.FriendlyRole.Equals(friendlyRole)
                    group u by u.LastNameFirstChar
                    into g
                    orderby g.Key
                    select new Group<RosterUser>(g.Key, g)).ToList();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
