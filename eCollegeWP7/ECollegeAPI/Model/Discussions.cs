using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Net;

namespace ECollegeAPI.Model
{

    public class ThreadedDiscussionResultList
    {
        public List<ThreadedDiscussion> ThreadedDiscussions { get; set; }
    }

    public class ThreadedDiscussion
    {
        public Int64 ID { get; set; }
        public string IntroductoryText { get; set; }
        public List<TopicLink> Links { get; set; }
    }

    public class TopicLink
    {
        public string Href { get; set; }
        public string Rel { get; set; }
    }
}
