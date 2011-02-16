
using System.Collections.Generic;
namespace eCollegeWP7.ViewModels.Application
{
    public class Course
    {
        public Course()
        {
            ThreadedDiscussions = new List<ThreadedDiscussion>();
            Announcements = new List<Announcement>();
            DropboxBaskets = new List<DropboxBasket>();
            GradebookItems = new List<GradebookItem>();
            Instructors = new List<TeachingInstructor>();
        }

        public string ID { get; set; }
        public string Href { get; set; }
        public string MeHref { get; set; }
        public string Rel { get; set; }
        public string DisplayCourseCode { get; set; }
        public string Title { get; set; }
        public object[] CallNumbers { get; set; }

        public List<ThreadedDiscussion> ThreadedDiscussions { get; set; }
        public List<Announcement> Announcements { get; set; }
        public List<DropboxBasket> DropboxBaskets { get; set; }
        public List<GradebookItem> GradebookItems { get; set; }
        public List<TeachingInstructor> Instructors { get; set; }
    }

    public class TeachingInstructor
    {
        public string InstructorHref { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string eMailAdress { get; set; }
        public string UserHref { get; set; }
    }
}
