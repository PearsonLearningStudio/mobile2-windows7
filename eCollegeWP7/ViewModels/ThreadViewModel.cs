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

        private string _ThreadTitle;
        public string ThreadTitle
        {
            get { return _ThreadTitle; }
            set { _ThreadTitle = value; this.OnPropertyChanged(() => this.ThreadTitle); }
        }

        private ObservableCollection<GroupedObservableCollection<DiscussionViewModel>> _ThreadTopics;
        public ObservableCollection<GroupedObservableCollection<DiscussionViewModel>> ThreadTopics
        {
            get { return _ThreadTopics; }
            set { _ThreadTopics = value; this.OnPropertyChanged(() => this.ThreadTopics); }
        }

        private string _ScheduleInfo;
        public string ScheduleInfo
        {
            get { return _ScheduleInfo; }
            set { _ScheduleInfo = value; this.OnPropertyChanged(() => this.ScheduleInfo); }
        }

        public ThreadViewModel(long courseId, long threadId, string scheduleInfo)
        {
            this.CourseID = courseId;
            this.ScheduleInfo = scheduleInfo;

            App.Model.BuildService(new FetchDiscussionTopicsByThreadIdService(courseId,threadId)).Execute((service) =>
            {
                foreach (var t in service.Result)
                {
                    if (t.Topic.ContainerInfo.ContentItemTitle != null)
                    {
                        ThreadTitle = t.Topic.ContainerInfo.ContentItemTitle;
                        break;
                    }
                }

                ThreadTopics = (from t in service.Result select new DiscussionViewModel(t)).ToList().ToSingleGroupedObservableCollection();
            });
            //App.Model.BuildService(new FetchDiscussionThreadByIdService(courseId, threadId)).Execute(
            //    (service) => Thread = service.Result);
        }

    }
}