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
using ECollegeAPI.Services.Grades;

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

        private long _CourseID;
        public long CourseID
        {
            get { return _CourseID; }
            set
            {
                _CourseID = value;
                this.OnPropertyChanged(() => this.CourseID); this.OnPropertyChanged(() => this.Course);
            }
        }

        private Course _Course;
        public Course Course
        {
            get { return AppViewModel.Courses.CourseIdMap[CourseID]; }
        }

        public string GradebookItemGuid { get; set; }

        public GradeViewModel(long courseId, string gradebookItemGuid)
        {
            this.CourseID = courseId;
            this.GradebookItemGuid = gradebookItemGuid;

            App.BuildService(new FetchGradebookItemByGuidService(CourseID, GradebookItemGuid)).Execute(service =>
            {
                this.GradebookItem = service.Result;
            });

            App.BuildService(new FetchMyGradebookItemGradeService(CourseID, GradebookItemGuid)).Execute(service =>
            {
                this.Grade = service.Result;
            });
        }

    }
}