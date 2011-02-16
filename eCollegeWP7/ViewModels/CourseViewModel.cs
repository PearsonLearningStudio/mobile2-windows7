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

namespace eCollegeWP7
{
    public class CourseViewModel : ViewModelBase
    {

        private int _CourseID;
        public int CourseID
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
        

        public CourseViewModel(int courseId)
        {
            this.CourseID = courseId;
            App.ViewModel.API.FetchAnnouncements(courseId, (result) =>
            {
                var formattedResult = new ObservableCollection<Announcement>();
                foreach (var ann in result)
                {
                    formattedResult.Add(ann);
                }
                this.Announcements = formattedResult;
            });
        }

    }
}