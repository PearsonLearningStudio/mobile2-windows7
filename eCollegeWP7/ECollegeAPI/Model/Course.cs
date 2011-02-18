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
using System.Collections.Generic;
using ECollegeAPI.Model.Boilerplate;

namespace ECollegeAPI.Model
{
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
}
