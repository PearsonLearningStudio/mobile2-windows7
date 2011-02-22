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

        private ObservableCollection<DiscussionTopicHeader> _UserTopics;
        public ObservableCollection<DiscussionTopicHeader> UserTopics
        {
            get { return _UserTopics; }
            set { _UserTopics = value; this.OnPropertyChanged(() => this.UserTopics); }
        }

        //private ObservableCollection<ThreadedDiscussion> _ThreadedDiscussions;
        //public ObservableCollection<ThreadedDiscussion> ThreadedDiscussions
        //{
        //    get { return _ThreadedDiscussions; }
        //    set { _ThreadedDiscussions = value; this.OnPropertyChanged(() => this.ThreadedDiscussions); }
        //}
        

        public CourseViewModel(int courseId)
        {
            this.CourseID = courseId;
            AppViewModel.API.FetchAnnouncements(courseId, (result) =>
            {
                var formattedResult = new ObservableCollection<Announcement>();
                foreach (var ann in result)
                {
                    formattedResult.Add(ann);
                }
                this.Announcements = formattedResult;
            });
            AppViewModel.API.FetchMyDiscussionTopics(new List<long>(){courseId}, (result) =>
            {
                var formattedResult = new ObservableCollection<DiscussionTopicHeader>();
                foreach (var r in result) formattedResult.Add(r);
                this.UserTopics = formattedResult;
            });
        }

    }
}