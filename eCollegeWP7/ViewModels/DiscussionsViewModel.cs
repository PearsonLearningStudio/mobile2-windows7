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
using ECollegeAPI.Services.Discussions;

namespace eCollegeWP7.ViewModels
{
    public class DiscussionsViewModel : ViewModelBase
    {
        private bool _loadStarted = false;
        public bool LoadStarted { get { return _loadStarted; } }

        private Course _DiscussionCourseFilter = CoursesViewModel.AllCoursesPlaceholder;
        public Course DiscussionCourseFilter
        {
            get { return _DiscussionCourseFilter; }
            set { _DiscussionCourseFilter = value; this.OnPropertyChanged(() => this.DiscussionCourseFilter); }
        }

        private List<Group<DiscussionViewModel>> _TopicsByCourse;
        public List<Group<DiscussionViewModel>> TopicsByCourse
        {
            get { return _TopicsByCourse; }
            set { _TopicsByCourse = value; this.OnPropertyChanged(() => this.TopicsByCourse); }
        }

        public void ForceUpdate()
        {
            this.OnPropertyChanged(() => this.DiscussionCourseFilter);
            this.OnPropertyChanged(() => this.TopicsByCourse);
        }

        public DiscussionsViewModel()
        {
        }

        public void Load()
        {
            Load(null);
        }

        public void Load(Action<bool> callback)
        {
            if (_loadStarted)
            {
                if (callback != null) callback(true);
                return;
            }

            _loadStarted = true;

            var courseIds = (from c in AppViewModel.Courses.MyCourses select (long)c.ID).ToList<long>();
            App.Model.BuildService(new FetchMyDiscussionTopicsService(courseIds)).Execute(service =>
            {
                this.TopicsByCourse = (from t in service.Result
                                       group new DiscussionViewModel(t) by t.Topic.ContainerInfo.CourseID
                                           into r
                                           orderby GetCourseTitle(r.Key)
                                           select new Group<DiscussionViewModel>(r.Key, r.Where(dvm => dvm.UserTopic.IsActive))).ToList();
                if (callback != null) callback(true);
            });
        }

        private string GetCourseTitle(long courseId)
        {
            Course c = App.Model.Courses.CourseIdMap[courseId];
            if (c != null)
            {
                return c.Title;
            }
            return null;
        }

    }
}