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
using ECollegeAPI.Services.Users;

namespace eCollegeWP7.ViewModels
{
    public class PeopleViewModel : ViewModelBase
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

        private List<Group<RosterUser>> _PeopleByLastNameFirstChar;
        public List<Group<RosterUser>> PeopleByLastNameFirstChar
        {
            get { return _PeopleByLastNameFirstChar; }
            set { _PeopleByLastNameFirstChar = value; this.OnPropertyChanged(() => this.PeopleByLastNameFirstChar); }
        }

        public PeopleViewModel(long courseId)
        {
            this.CourseID = courseId;
            App.BuildService(new FetchRosterService(courseId)).Execute((service) =>
            {
                //var sortedUsers = (from u in service.Result orderby u.LastName select u);

                PeopleByLastNameFirstChar = (from u in service.Result orderby u.LastName
                                               group u by u.LastNameFirstChar
                                                   into g
                                                   orderby g.Key
                                                              select new Group<RosterUser>(g.Key, g)).ToList();
            });

        }

    }
}