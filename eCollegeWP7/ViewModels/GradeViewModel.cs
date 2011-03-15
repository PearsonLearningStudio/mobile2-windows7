using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using ECollegeAPI.Model;
using eCollegeWP7.Util;
using System.Linq;

namespace eCollegeWP7.ViewModels
{

    public class GradeViewModel : ViewModelBase
    {
        private Grade _Grade;
        public Grade Grade
        {
            get { return _Grade; }
            set { _Grade = value; this.OnPropertyChanged(() => this.Grade); }
        }

        private GradebookItem _GradebookItem;
        public GradebookItem GradebookItem
        {
            get { return _GradebookItem; }
            set { _GradebookItem = value; this.OnPropertyChanged(() => this.GradebookItem); }
        }

        public long CourseID { get; set; }
        public string GradebookItemGuid { get; set; }

        public GradeViewModel(long courseId, string gradebookItemGuid)
        {
            this.CourseID = courseId;
            this.GradebookItemGuid = gradebookItemGuid;

            App.Model.Client.FetchGradebookItemByGuid(CourseID, GradebookItemGuid, res =>
            {
                this.GradebookItem = res;
            });

            App.Model.Client.FetchMyGradebookItemGrade(CourseID, GradebookItemGuid, res =>
            {
                this.Grade = res;
            });
        }

    }
}