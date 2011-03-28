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
using ECollegeAPI.Services.Dropbox;

namespace eCollegeWP7.ViewModels
{

    public class DropboxMessageViewModel : ViewModelBase
    {
        private long _CourseID;
        public long CourseID
        {
            get { return _CourseID; }
            set
            {
                _CourseID = value;
                this.OnPropertyChanged(() => this.CourseID); this.OnPropertyChanged(() => this.Course);
            }
        }

        private Course _Course;
        public Course Course
        {
            get { return AppViewModel.Courses.CourseIdMap[CourseID]; }
        }

        private DropboxMessage _Message;
        public DropboxMessage Message
        {
            get { return _Message; }
            set { _Message = value; this.OnPropertyChanged(() => this.Message); }
        }

        public DropboxMessageViewModel(long courseId, long basketId, long messageId)
        {
            this.CourseID = courseId;
            App.BuildService(new FetchDropboxMessageService(courseId, basketId, messageId)).Execute(service =>
            {
                this.Message = service.Result;
            });
        }

    }
}