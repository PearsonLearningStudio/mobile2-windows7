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

namespace eCollegeWP7.ViewModels
{
    public class CoursesViewModel : ViewModelBase
    {
        private ObservableCollection<Course> _MyCourses;
        public ObservableCollection<Course> MyCourses
        {
            get { return _MyCourses; }
            set
            {
                _MyCourses = value;
                this.OnPropertyChanged(() => this.MyCourses);
                this.OnPropertyChanged(() => this.MyCoursesPlusAll);
            }
        }

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

        public void Load(Action<bool> callback)
        {
            AppViewModel.Client.FetchMyCurrentCourses(result =>
            {
                var oc = new ObservableCollection<Course>();
                foreach (var c in result) { oc.Add(c); }
                this.MyCourses = oc;
                callback(true);
            });
        }

    }
}