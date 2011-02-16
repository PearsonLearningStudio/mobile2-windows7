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

namespace eCollegeWP7.ViewModels.Application
{
    public class GradebookItem
    {
        public string Type { get; set; }
        public string ID { get; set; }
        public string Title { get; set; }
        public double PointsPossible { get; set; }
        public string Href { get; set; }
        public string GradebookHref { get; set; }
        public string Rel { get; set; }
        public Grade Grade { get; set; }
    }

    public class Grade
    {
        public Grade()
        {
            GradedStudent = new GradedStudent();
        }

        public string ID { get; set; }
        public double? Points { get; set; }
        public string LetterGrade { get; set; }
        public string Comments { get; set; }
        public DateTime UpdatedDate { get; set; }
        public GradedStudent GradedStudent { get; set; }
    }

    public class GradedStudent
    {
        public string ID { get; set; }
        public string Href { get; set; }
        public string Rel { get; set; }
    }
}
