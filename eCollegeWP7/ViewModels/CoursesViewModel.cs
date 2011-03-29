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
using ECollegeAPI.Services.Courses;

namespace eCollegeWP7.ViewModels
{
    public class CoursesViewModel : ViewModelBase
    {
        public Dictionary<long, Course> CourseIdMap { get; set; }

        public CoursesViewModel()
        {
            CourseIdMap = new Dictionary<long, Course>();
        }

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
                res.Add(_AllCoursesPlaceholder);
                if (MyCourses != null) { foreach (var c in MyCourses) res.Add(c); }
                return res;
            }
        }

        private static Course _AllCoursesPlaceholder = new Course() {ID = -1, Title = "All Courses"};
        public static Course AllCoursesPlaceholder { get { return _AllCoursesPlaceholder; } }
        
        public void Load() {Load(null);}

        public void Load(Action successCallback)
        {
            App.BuildService(new FetchMyCurrentCoursesService()).Execute(service =>
            {
                var oc = new ObservableCollection<Course>();
                foreach (var c in service.Result)
                { 
                    oc.Add(c);
                    CourseIdMap[c.ID] = c;
                }
                this.MyCourses = oc;
                successCallback();
            });
        }

    }
}