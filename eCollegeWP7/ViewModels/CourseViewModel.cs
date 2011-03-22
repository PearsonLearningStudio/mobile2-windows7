using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using ECollegeAPI.Services.Discussions;
using eCollegeWP7.Util;
using ECollegeAPI.Services.Announcements;

namespace eCollegeWP7.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {

        private long _CourseID;
        public long CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; this.OnPropertyChanged(() => this.CourseID); }
        }

        private ObservableCollection<Announcement> _Announcements;
        public ObservableCollection<Announcement> Announcements
        {
            get { return _Announcements; }
            set { _Announcements = value; this.OnPropertyChanged(() => this.Announcements); }
        }

        private ObservableCollection<DiscussionViewModel> _CourseTopics;
        public ObservableCollection<DiscussionViewModel> CourseTopics
        {
            get { return _CourseTopics; }
            set { _CourseTopics = value; this.OnPropertyChanged(() => this.CourseTopics); }
        }

        private ActivitiesViewModel _Activities;
        public ActivitiesViewModel Activities
        {
            get { return _Activities; }
            set { _Activities = value; this.OnPropertyChanged(() => this.Activities); }
        }
        

        public CourseViewModel(int courseId)
        {
            this.CourseID = courseId;
            App.BuildService(new FetchAnnouncementsService(courseId)).Execute((service) =>
            {
                this.Announcements = service.Result.ToObservableCollection();
            });
            var task = App.BuildService(new FetchMyDiscussionTopicsService(new List<long>() {courseId}));
            task.Execute((service) =>
            {
                this.CourseTopics = (from t in service.Result select new DiscussionViewModel(t)).ToList().ToObservableCollection();
            });
            this.Activities = new ActivitiesViewModel();
            this.Activities.CourseID = courseId;
            this.Activities.Load(false);
        }

    }
}