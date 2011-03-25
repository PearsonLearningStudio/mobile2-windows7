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
using ECollegeAPI.Services.Announcements;
using eCollegeWP7.Util;
using System.Linq;
using ECollegeAPI.Services.Activities;

namespace eCollegeWP7.ViewModels
{
    public class GradesViewModel : ViewModelBase
    {

        private long? _CourseID;
        public long? CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value;
                this.OnPropertyChanged(() => this.CourseID); this.OnPropertyChanged(() => this.Course); }
        }

        private Course _Course;
        public Course Course
        {
            get { return CourseID.HasValue ? AppViewModel.Courses.CourseIdMap[CourseID.Value] : null; }
        }

        public GradesViewModel(long courseId)
        {
            this.CourseID = courseId;

            //App.BuildService(new FetchAnnouncementsService(courseId)).Execute((service) =>
            //{
            //    this.Announcements = service.Result.ToObservableCollection();
            //});
        }

    }
}