using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCollegeWP7.ECollegeAPI.Model
{
    public class ActivityStreamObject
    {
        public long CourseId { get; set; }
        public object ReferenceId { get; set; }
        public object ID { get; set; }
        public string Summary { get; set; }
        public string ObjectType { get; set; }
        public string LetterGrade { get; set; } //on grades
        public decimal PointsAchieved { get; set; } //on grades
    }
}
