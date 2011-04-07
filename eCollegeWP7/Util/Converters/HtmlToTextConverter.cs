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
using HtmlAgilityPack;

namespace eCollegeWP7.Util.Converters
{
    public class HtmlToTextConverter : IValueConverter
    {

        public static string StripHtml(string html)
        {
            if (html == null) return null;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var output = "";

            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                output += node.InnerText;
            }

            return HttpUtility.HtmlDecode(output).Trim();
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return StripHtml(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
