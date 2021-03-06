﻿using System;
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
    public class CourseIdToCourseNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is long)
            {
                var v = (long)value;
                Course c;
                if (App.Model.Courses.CourseIdMap.TryGetValue(v,out c))
                {
                    if ("ToUpper".Equals(parameter)) 
                    {
                        return c.Title.ToUpper();
                    }
                    if ("ToLower".Equals(parameter)) {
                        return c.Title.ToLower();
                    }
                    return c.Title;
                }
            }
            return "Unknown Course";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
