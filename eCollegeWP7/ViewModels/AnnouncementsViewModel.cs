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
    public class AnnouncementsViewModel : ViewModelBase
    {

        private long _CourseID;
        public long CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; this.OnPropertyChanged(() => this.CourseID); }
        }
        

        private Course _Course;
        public Course Course
        {
            get { return _Course; }
            set { _Course = value; this.OnPropertyChanged(() => this.Course); }
        }

        private ObservableCollection<Announcement> _Announcements;
        public ObservableCollection<Announcement> Announcements
        {
            get { return _Announcements; }
            set { _Announcements = value; this.OnPropertyChanged(() => this.Announcements); }
        }

        public AnnouncementsViewModel(long courseId)
        {
            this.CourseID = courseId;
            this.Course = App.Model.Courses.CourseIdMap[courseId];

            App.BuildService(new FetchAnnouncementsService(courseId)).Execute((service) =>
            {
                this.Announcements = service.Result.ToObservableCollection();
            });
        }

    }
}