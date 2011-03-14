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
using eCollegeWP7.Util;
using ECollegeAPI.Model;
using ECollegeAPI;
using System.Linq;

namespace eCollegeWP7
{
    public class MainViewModel : ViewModelBase
    {

        private ObservableCollection<Course> _MyCourses;
        public ObservableCollection<Course> MyCourses
        {
            get { return _MyCourses; }
            set {
                _MyCourses = value;
                this.OnPropertyChanged(() => this.MyCourses);
                this.OnPropertyChanged(() => this.MyCoursesPlusAll); 
            }
        }

        private List<DiscussionTopicHeader> _Topics;
        public List<DiscussionTopicHeader> Topics
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

        private Course _PeopleCourseFilter;
        public Course PeopleCourseFilter
        {
            get { return _PeopleCourseFilter; }
            set {
                // Stalled until the enrolledusers api stops returning "Access to that resource is denied"
                //_PeopleCourseFilter = value;
                //if (_PeopleCourseFilter != null)
                //{
                //    if (_peopleForCourse.ContainsKey(_PeopleCourseFilter.ID))
                //    {
                //        FilteredPeople = _peopleForCourse[_PeopleCourseFilter.ID];
                //    }
                //    else
                //    {
                //        AppViewModel.Client.FetchEnrolledUsers(_PeopleCourseFilter.ID, (result) =>
                //        {
                //            var col = result.ToObservableCollection();
                //            _peopleForCourse[_PeopleCourseFilter.ID] = col;
                //            FilteredPeople = col;
                //        });
                //    }
                //}
                //else
                //{
                //    FilteredPeople = null;
                //}
                //this.OnPropertyChanged(() => this.PeopleCourseFilter); 
            }
        }

        private ObservableCollection<EnrolledUser> _FilteredPeople;
        public ObservableCollection<EnrolledUser> FilteredPeople
        {
            get { return _FilteredPeople; }
            set { _FilteredPeople = value; this.OnPropertyChanged(() => this.FilteredPeople); }
        }

        private Dictionary<long, ObservableCollection<EnrolledUser>> _peopleForCourse = new Dictionary<long,ObservableCollection<EnrolledUser>>();

        public ObservableCollection<Course> MyCoursesPlusAll
        {
            get
            {
                var res = new ObservableCollection<Course>();
                res.Add(new Course() { ID = -1, Title = "All Courses" });
                if (MyCourses != null) { foreach (var c in MyCourses) res.Add(c); }
                return res;
            }
        }

        public MainViewModel()
        {
        }

        public void LoadData()
        {
            AppViewModel.Client.FetchMyCurrentCourses(result =>
            {
                var oc = new ObservableCollection<Course>();
                foreach (var c in result) { oc.Add(c); }
                this.MyCourses = oc;

                var courseIds = (from c in oc select (long)c.ID).ToList<long>();
                AppViewModel.Client.FetchMyDiscussionTopics(courseIds, (tresult) =>
                {
                    this.Topics = tresult;
                });

            });
        }
    }
}