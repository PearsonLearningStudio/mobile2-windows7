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
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI.Model
{
    public class DiscussionResponseHeader
    {
        public string ID { get; set; }
        public bool MarkedAsRead { get; set; }
        public DiscussionResponse Response { get; set; }
        public ResponseCount ChildResponseCounts { get; set; }
        public ParentUserTopicLinkContainer ParentUserTopic { get; set; }
    }
}
