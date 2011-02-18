using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Net;

namespace ECollegeAPI.Model
{
    public class Announcement
    {
        public long ID { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Submitter { get; set; }
        public DateTime StartDisplayDate { get; set; }
        public DateTime EndDisplayDate { get; set; }
        public long CourseID { get; set; }
        public string CourseTitle { get; set; }
    }
}
