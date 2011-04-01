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
using ECollegeAPI.Services.Discussions;
using eCollegeWP7.Util;
using System.Linq;
using ECollegeAPI.Services.Activities;

namespace eCollegeWP7.ViewModels
{
    public class CourseDiscussionsViewModel : ViewModelBase
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

        private List<Group<DiscussionViewModel>> _TopicsByUnit;
        public List<Group<DiscussionViewModel>> TopicsByUnit
        {
            get { return _TopicsByUnit; }
            set { _TopicsByUnit = value; this.OnPropertyChanged(() => this.TopicsByUnit); }
        }

        private ObservableCollection<DiscussionViewModel> _CourseTopics;
        public ObservableCollection<DiscussionViewModel> CourseTopics
        {
            get { return _CourseTopics; }
            set { _CourseTopics = value; this.OnPropertyChanged(() => this.CourseTopics); }
        }

        public CourseDiscussionsViewModel(long courseId)
        {
            this.CourseID = courseId;
            this.Course = App.Model.Courses.CourseIdMap[courseId];

            var task = App.BuildService(new FetchMyDiscussionTopicsService(new List<long>() { courseId }));
            task.Execute((service) =>
            {
                this.CourseTopics = (from t in service.Result select new DiscussionViewModel(t)).ToList().ToObservableCollection();

                this.TopicsByUnit = (from t in service.Result
                                       group new DiscussionViewModel(t) by GetUnitTitle(t)
                                           into r
                                           select new Group<DiscussionViewModel>(r.Key, r)).ToList();

            });
        }

        protected string GetUnitTitle(UserDiscussionTopic udt)
        {
            var ci = udt.Topic.ContainerInfo;
            return string.Format("{0} {1}: {2}", ci.UnitHeader, ci.UnitNumber, ci.UnitTitle);
        }

    }
}