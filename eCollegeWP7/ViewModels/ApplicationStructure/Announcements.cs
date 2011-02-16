using System;

namespace eCollegeWP7.ViewModels.Application
{
    public class Announcement
    {
        public string ID { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Submitter { get; set; }
        public DateTime StartDisplayDate { get; set; }
        public DateTime EndDisplayDate { get; set; }
    }
}
