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
    public class UserDiscussionTopic
    {
        public string ID { get; set; }
        public DiscussionTopic Topic { get; set; }
        public ResponseCount ChildResponseCounts { get; set; }
    }
}
