using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace ECollegeAPI.Model
{

    public class CoursesResultList
    {
        public List<CourseLinkContainer> Courses { get; set; }
    }

    public class CourseLinkContainer
    {
        public List<CourseLink> Links { get; set; }
    }

    public class CourseLink
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public Course Course { get; set; }
        public string Title { get; set; }
    }

    public class Course
    {
        public int ID { get; set; }
        public string DisplayCourseCode { get; set; }
        public string Title { get; set; }
        public List<string> CallNumbers { get; set; }
        public LinkContainer<InstructorLink> Instructors { get; set; }
        public LinkContainer<TeacherAssistantLink> TeacherAssistants { get; set; }
        public LinkContainer<StudentLink> Students { get; set; }
        public List<TermLink> Links { get; set; }
    }

    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
    }

    public class LinkContainer<T>
    {
        public List<T> Links { get; set; }
    }

    public class InstructorLink
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
    }

    public class TeacherAssistantLink
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
    }

    public class StudentLink
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
    }

    public class TermLink
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
    }
}
