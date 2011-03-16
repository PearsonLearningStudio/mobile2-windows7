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

namespace ECollegeAPI.Model
{
    public class ResponseCount
    {
        public long TotalResponseCount { get; set; }
        public long UnreadResponseCount { get; set; }
        public long PersonalResponseCount { get; set; }
        public long Last24HourResponseCount { get; set; }
    }
}
