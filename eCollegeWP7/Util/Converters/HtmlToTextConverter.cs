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

        private static HtmlNode FindBodyNode(HtmlNode node)
        {
            if (node.Name != null && node.Name.ToLower().Equals("body"))
            {
                return node;
            }
            foreach (var childNode in node.ChildNodes)
            {
                var res = FindBodyNode(childNode);
                if (res != null) return res;
            }
            return null;
        }

        public static string StripHtmlBody(string html)
        {
            if (html == null) return null;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var bodyNode = FindBodyNode(doc.DocumentNode);

            var output = "";

            if (bodyNode != null)
            {
                foreach (var node in bodyNode.ChildNodes)
                {
                    output += node.InnerText;
                }
            }

            return HttpUtility.HtmlDecode(output).Trim();
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            if ("body".Equals(parameter)) return StripHtmlBody(value.ToString());
            return StripHtml(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
