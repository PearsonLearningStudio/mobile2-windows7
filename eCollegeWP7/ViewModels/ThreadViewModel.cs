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
using ECollegeAPI.Services.Discussions;
using eCollegeWP7.Util;
using System.Linq;
using ECollegeAPI.Services.Users;

namespace eCollegeWP7.ViewModels
{
    public class ThreadViewModel : ViewModelBase
    {

        private long _CourseID;
        public long CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value;
                this.OnPropertyChanged(() => this.CourseID); this.OnPropertyChanged(() => this.Course); }
        }

        private Course _Course;
        public Course Course
        {
            get { return AppViewModel.Courses.CourseIdMap[CourseID]; }
        }

        private DiscussionThread _Thread;
        public DiscussionThread Thread
        {
            get { return _Thread; }
            set { _Thread = value; this.OnPropertyChanged(() => this.Thread); }
        }

        private List<DiscussionThreadTopic> _ThreadTopics;
        public List<DiscussionThreadTopic> ThreadTopics
        {
            get { return _ThreadTopics; }
            set { _ThreadTopics = value; this.OnPropertyChanged(() => this.ThreadTopics); }
        }
        

        public ThreadViewModel(long courseId, long threadId)
        {
            this.CourseID = courseId;
            App.Model.BuildService(new FetchDiscussionTopicsByThreadIdService(courseId,threadId)).Execute((service) =>
            {
                ThreadTopics = service.Result;
            });
            App.Model.BuildService(new FetchDiscussionThreadByIdService(courseId, threadId)).Execute(
                (service) => Thread = service.Result);
        }

    }
}