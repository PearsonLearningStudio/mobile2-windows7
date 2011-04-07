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
    public class UserGradebookItem
    {
        public string ID { get; set; }
        public GradebookItem GradebookItem { get; set; }
        public List<GradeLink> Links { get; set; }

        public Grade Grade
        {
            get
            {
                if (Links != null && Links.Count > 0)
                {
                    return Links[0].Grade;
                }
                return null;
            }
        }

        public string DisplayedGrade
        {
            get
            {
                Grade g = this.Grade;

                if (g == null) return null;
                if (GradebookItem.PointsPossible.HasValue && g.Points.HasValue)
                {
                    return string.Format("{0:0.##}", g.Points) + "/" +
                           string.Format("{0:0.##}", GradebookItem.PointsPossible);
                }
                if (g.LetterGrade != null)
                {
                    return g.LetterGrade;
                }
                return "";
            }
        }
    }
}
