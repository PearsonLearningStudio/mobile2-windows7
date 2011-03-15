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
    public class DiscussionsViewModel : ViewModelBase
    {

        private List<UserDiscussionTopic> _Topics;
        public List<UserDiscussionTopic> Topics
        {
            get { return _Topics; }
            set { _Topics = value; this.OnPropertyChanged(() => this.Topics); }
        }

        private Course _DiscussionCourseFilter;
        public Course DiscussionCourseFilter
        {
            get { return _DiscussionCourseFilter; }
            set { _DiscussionCourseFilter = value; this.OnPropertyChanged(() => this.DiscussionCourseFilter); }
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
            if (_Topics != null)
            {
                if (callback != null) callback(true);
                return;
            }

            var courseIds = (from c in AppViewModel.Courses.MyCourses select (long)c.ID).ToList<long>();
            AppViewModel.Client.FetchMyDiscussionTopics(courseIds, (tresult) =>
            {
                this.Topics = tresult;
                if (callback != null) callback(true);
            });
        }

    }
}