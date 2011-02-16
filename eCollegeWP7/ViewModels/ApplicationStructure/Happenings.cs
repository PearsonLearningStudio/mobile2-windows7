using System;

namespace eCollegeWP7.ViewModels.Application
{
    public class HappeningsItem
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public string FormatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Course Course { get; set; }
        public Object Type { get; set; }
    }
}
