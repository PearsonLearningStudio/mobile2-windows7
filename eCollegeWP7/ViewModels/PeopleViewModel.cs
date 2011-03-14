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
    public class PeopleViewModel : ViewModelBase
    {

        private Course _PeopleCourseFilter;
        public Course PeopleCourseFilter
        {
            get { return _PeopleCourseFilter; }
            set
            {
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

        private Dictionary<long, ObservableCollection<EnrolledUser>> _peopleForCourse = new Dictionary<long, ObservableCollection<EnrolledUser>>();
    }
}