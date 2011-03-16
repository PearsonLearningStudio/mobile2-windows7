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

namespace ECollegeAPI.Model
{
    public class ContainerInfo
    {
        public long ContentItemID { get; set; }
        public string ContentItemTitle { get; set; }
        public long ContentItemOrderNumber { get; set; }
        public string UnitTitle { get; set; }
        public long UnitNumber { get; set; }
        public string UnitHeader { get; set; }
        public long CourseID { get; set; }
        public string CourseTitle { get; set; }
    }
}
